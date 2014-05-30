using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public partial class Admin_EditSlogan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Text.xml"));
            var panel = doc.Element("Panels").Elements("Panel").Select(pnl => new
            {
                Marquee = pnl.Element("Marquee").Value
                ,
                Slogan = pnl.Element("Slogan").Value
            });

            foreach (var item in panel)
            {
                this.txtMarquee.Text = item.Marquee;
                this.txtSlogan.Text = item.Slogan;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Text.xml"));
        IEnumerable<XElement> element = doc.Element("Panels").Elements("Panel");

        foreach (XElement item in element)
        {
            item.Element("Marquee").Value = this.txtMarquee.Text.Trim();
            item.Element("Slogan").Value = this.txtSlogan.Text.Trim();
            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
        }
        this.lblMessage.Text = "ویرایش پنل انجام گردید";
    }
}