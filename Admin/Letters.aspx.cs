using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public partial class Site_Letters : System.Web.UI.Page
{
    XDocument doc = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            doc = XDocument.Load(Server.MapPath("~/App_Data/Letters.xml"));
            this.drpLetters.DataSource = doc.Element("Letters").Elements("Letter").Select(an => new { Id = an.Attribute("id").Value, Title = an.Element("Title").Value });
            this.drpLetters.DataBind();
            this.drpLetters.Items.Insert(0, "- جدید -");
            this.txtDate.Text = Public.ToPersianDate(DateTime.Now);
        }
    }

    protected void drpLetters_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.drpLetters.SelectedIndex > 0)
        {
            doc = XDocument.Load(Server.MapPath("~/App_Data/Letters.xml"));
            var text = doc.Element("Letters").Elements("Letter").Where(let => let.Attribute("id").Value == this.drpLetters.SelectedValue).Select(let =>
                new
                {
                    Title = let.Element("Title").Value,
                    Text = let.Element("Text").Value,
                    Url = let.Element("Url").Value,
                    Date = let.Element("Date").Value
                });
            foreach (var item in text)
            {
                this.txtTitle.Text = item.Title;
                this.txtUrl.Text = item.Url;
                this.txtDate.Text = item.Date;
                this.txtEditor.Value = item.Text;
            }
        }
        else
        {
            this.txtTitle.Text = null;
            this.txtUrl.Text = null;
            this.txtEditor.Value = null;
            this.txtDate.Text = Public.ToPersianDate(DateTime.Now);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        doc = XDocument.Load(Server.MapPath("~/App_Data/Letters.xml"));
        if (this.drpLetters.SelectedIndex > 0) // Edit mode
        {
            string fileName = string.Empty;

            IEnumerable<XElement> elements = doc.Element("Letters").Elements("Letter").Where(an => an.Attribute("id").Value == this.drpLetters.SelectedValue);
            foreach (XElement item in elements)
            {
                item.Element("Date").Value = this.txtDate.Text;
                item.Element("Title").Value = this.txtTitle.Text;
                item.Element("Url").Value = this.txtUrl.Text;
                item.Element("Text").Value = this.txtEditor.Value;
            }
            doc.Save(Server.MapPath("~/App_Data/Letters.xml"));
            this.lblMessage.Text = "ویرایش انجام گردید";
        }
        else // Add mode
        {
            int nextId = 1;
            IEnumerable<XElement> lastItem = doc.Element("Letters").Elements("Letter").Reverse().Take(1);
            foreach (XElement item in lastItem)
            {
                nextId = Public.ToShort(item.Attribute("id").Value) + 1;
            }

            doc.Element("Letters").Add(new XElement("Letter", new XAttribute("id", nextId),
                                                              new XElement("Date", this.txtDate.Text.Trim()),
                                                              new XElement("Title", this.txtTitle.Text.Trim()),
                                                              new XElement("Url", this.txtUrl.Text.Trim()),
                                                              new XElement("Text", this.txtEditor.Value.Trim())));
            doc.Save(Server.MapPath("~/App_Data/Letters.xml"));

            this.lblMessage.Text = "ثبت انجام گردید";
            this.drpLetters.DataSource = doc.Element("Letters").Elements("Letter").Select(an => new { Id = an.Attribute("id").Value, Title = an.Element("Title").Value });
            this.drpLetters.DataBind();
            this.drpLetters.Items.Insert(0, "- جدید -");
            this.drpLetters.SelectedIndex = 0;
        }

        this.txtTitle.Text = null;
        this.txtUrl.Text = null;
        this.txtEditor.Value = null;
        this.txtDate.Text = Public.ToPersianDate(DateTime.Now);
    }

    protected void btnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (this.drpLetters.SelectedIndex > 0)
        {
            doc = XDocument.Load(Server.MapPath("~/App_Data/Letters.xml"));
            IEnumerable<XElement> elements = doc.Element("Letters").Elements("Letter").Where(an => an.Attribute("id").Value == this.drpLetters.SelectedValue);
            foreach (XElement item in elements)
            {
                item.Remove();
                this.txtTitle.Text = null;
                this.txtUrl.Text = null;
                this.txtEditor.Value = null;
                this.txtDate.Text = Public.ToPersianDate(DateTime.Now);
            }
            doc.Save(Server.MapPath("~/App_Data/Letters.xml"));
            this.drpLetters.DataSource = doc.Element("Letters").Elements("Letter").Select(an => new { Id = an.Attribute("id").Value, Title = an.Element("Title").Value });
            this.drpLetters.DataBind();
            this.drpLetters.Items.Insert(0, "- جدید -");
        }
    }
}