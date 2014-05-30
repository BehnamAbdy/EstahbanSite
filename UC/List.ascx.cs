using System;
using System.Linq;
using System.Xml.Linq;
using System.Text;

public partial class UC_List : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XDocument doc = doc = XDocument.Load(Server.MapPath("~/App_Data/List.xml"));
            StringBuilder html = new StringBuilder("<ul id='loans'>");
            var list = doc.Element("List").Elements("Item").Select(an => new { Id = an.Attribute("id").Value, Title = an.Element("Title").Value });
            foreach (var item in list)
            {
                html.AppendFormat("<li><a href='{0}?id={1}'>{2}</a></li>", ResolveUrl("~/Info.aspx"), item.Id, item.Title);
            }
            this.lstTests.InnerHtml = html.Append("</ul>").ToString();
        }
    }
}