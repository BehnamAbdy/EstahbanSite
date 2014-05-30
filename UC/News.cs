using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

public partial class UC_News : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Letters.xml"));
            var letters = doc.Element("Letters").Elements("Letter").OrderByDescending(let => let.Element("Date").Value).Select(let => new
            {
                Title = let.Element("Title").Value,
                Url = let.Element("Url").Value,
                Date = let.Element("Date").Value,
                Text = string.IsNullOrEmpty(let.Element("Text").Value) ? null : (let.Element("Text").Value.Length < 80 ? let.Element("Text").Value : let.Element("Text").Value.Substring(0, 80)),
                Id = let.Attribute("id").Value
            });

            StringBuilder html = new StringBuilder("<ul id='letters'>");
            foreach (var item in letters)
            {
                html.AppendFormat(@"<li>
                                <img src='./App_Themes/Default/images/title-logo.png' />
                                <div>&nbsp;
                                    <a target='_blank' href='{0}'>{1}</a>
                                    <p class='lett-date'>{2}</p>
                                    <p>{3} ...</p>
                                </div></li>", string.IsNullOrEmpty(item.Url) ? string.Concat(ResolveUrl("~/Info.aspx"), "?news=", item.Id) : item.Url, item.Title, item.Date, item.Text);
            }

            html.Append("</ul><div class='clear'></div>");
            this.dvList.InnerHtml = null;
            this.dvList.InnerHtml = html.ToString();
        }
    }
}