using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Linq;

public partial class Admin_TickerItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            short id = 0;
            if (Request.QueryString["mode"] != null)
            {
                XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Ticker.xml"));
                switch (Request.QueryString["mode"])
                {
                    case "0": // load all
                        var letters = doc.Element("Items").Elements("Item").Select(let => new
                        {
                            id = let.Attribute("id").Value,
                            title = let.Value
                        });
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        Response.Clear();
                        Response.Write(serializer.Serialize(letters));
                        Response.End();
                        break;

                    case "2": // delete
                        if (short.TryParse(Request.QueryString["id"], out id) && id > 0)
                        {
                            IEnumerable<XElement> elements = doc.Element("Items").Elements("Item").Where(an => an.Attribute("id").Value == Request.QueryString["id"]);
                            foreach (XElement item in elements)
                            {
                                item.Remove();
                            }
                            doc.Save(Server.MapPath("~/App_Data/Ticker.xml"));
                            Response.Clear();
                            Response.Write("0");
                            Response.End();

                        }
                        break;
                }
            }
            else if (Request.RequestType == "POST") // edit
            {
                if (short.TryParse(Request.Params["id"], out id))
                {
                    XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Ticker.xml"));
                    if (id > 0)
                    {
                        IEnumerable<XElement> elements = doc.Element("Items").Elements("Item").Where(an => an.Attribute("id").Value == Request.Params["id"]);
                        foreach (XElement item in elements)
                        {
                            item.Value = Request.Params["title"];
                        }
                        doc.Save(Server.MapPath("~/App_Data/Ticker.xml"));
                        Response.Clear();
                        Response.Write("0");
                        Response.End();
                    }
                    else
                    {
                        int nextId = 1;
                        IEnumerable<XElement> lastItem = doc.Element("Items").Elements("Item").Reverse().Take(1);
                        foreach (XElement item in lastItem)
                        {
                            nextId = Public.ToShort(item.Attribute("id").Value) + 1;
                        }
                        doc.Element("Items").Add(new XElement("Item", new XAttribute("id", nextId), Request.Params["title"]));
                        doc.Save(Server.MapPath("~/App_Data/Ticker.xml"));
                        Response.Clear();
                        Response.Write(nextId.ToString());
                        Response.End();
                    }
                }
            }
        }
    }
}