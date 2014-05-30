using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public partial class Admin_EditPanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Wellcome.xml"));
            var panel = doc.Element("Panels").Elements("Panel").Select(pnl => new
            {
                Title = pnl.Element("Title").Value
                ,
                Description = pnl.Element("Description").Value
                ,
                List = pnl.Element("List").Descendants().Select(lst => new { Item = lst.Value })
            });

            foreach (var item in panel)
            {
                this.txtTitle.Text = item.Title;
                this.txtDesc.Text = item.Description;
                foreach (var itm in item.List)
                {
                    this.txtList.Text += string.Concat(itm.Item, "$");
                }

                if (!string.IsNullOrEmpty(this.txtList.Text))
                {
                    this.txtList.Text.Remove(this.txtList.Text.Length - 1, 1);
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Wellcome.xml"));
        IEnumerable<XElement> panels = doc.Element("Panels").Elements("Panel");
        foreach (XElement item in panels)
        {
            item.Remove();
        }

        string[] list = this.txtList.Text.Split('$');
        XElement xList = new XElement("List");
        foreach (string item in list)
        {
            if (!string.IsNullOrEmpty(item))
            {
                xList.Add(new XElement("Item", item));
            }
        }
        doc.Element("Panels").Add(new XElement("Panel", new XElement("Title", this.txtTitle.Text.Trim()),
                                                                           new XElement("Description", this.txtDesc.Text.Trim()),
                                                                           xList));

        doc.Save(Server.MapPath("~/App_Data/Wellcome.xml"));
        this.lblMessage.Text = "ویرایش پنل انجام گردید";
    }
}