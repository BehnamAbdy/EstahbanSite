using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web.Script.Serialization;

public partial class Admin_EditList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XDocument doc = null;
            string result = null;
            if (Request.QueryString["mode"] != null)
            {
                JavaScriptSerializer serializer = null;
                doc = XDocument.Load(Server.MapPath("~/App_Data/List.xml"));
                switch (Request.QueryString["mode"])
                {
                    case "1": // Load list
                        serializer = new JavaScriptSerializer();
                        var list = doc.Element("List").Elements("Item").Select(an => new { Id = an.Attribute("id").Value, Title = an.Element("Title").Value });
                        result = serializer.Serialize(list);
                        break;

                    case "2": // Load a test
                        serializer = new JavaScriptSerializer();
                        var test = doc.Element("List").Elements("Item").Where(an => an.Attribute("id").Value == Request.QueryString["id"]).Select(tst => new { Title = tst.Element("Title").Value, Description = tst.Element("Description").Value });
                        result = serializer.Serialize(test);
                        break;

                    case "3": // Delete a test
                        IEnumerable<XElement> elements = doc.Element("List").Elements("Item").Where(an => an.Attribute("id").Value == Request.QueryString["id"]);
                        foreach (XElement element in elements.ToList<XElement>())
                        {
                            element.Remove();
                        }
                        doc.Save(Server.MapPath("~/App_Data/List.xml"));
                        result = "1";
                        break;
                }

                Response.Clear();
                Response.Write(result);
                Response.End();
            }
            else if (Request.HttpMethod == "POST")
            {
                doc = XDocument.Load(Server.MapPath("~/App_Data/List.xml"));
                if (Request.Params["id"] == "0") // Add mode
                {
                    string maxId = doc.Element("List").Elements("Item").Max(tst => tst.Attribute("id").Value); ;

                    doc.Element("List").Add(new XElement("Item", new XAttribute("id", maxId == null ? "1" : (short.Parse(maxId) + 1).ToString()),
                                                       new XElement("Title", Request.Params["tle"]),
                                                       new XElement("Description", Request.Params["bdy"])));
                    doc.Save(Server.MapPath("~/App_Data/List.xml"));
                    result = "1";
                }
                else // Edit mode
                {
                    IEnumerable<XElement> element = doc.Element("List").Elements("Item").Where(an => an.Attribute("id").Value == Request.Params["id"]);
                    foreach (XElement item in element)
                    {
                        item.Element("Title").Value = Request.Params["tle"];
                        item.Element("Description").Value = Request.Params["bdy"];
                        doc.Save(Server.MapPath("~/App_Data/List.xml"));
                        result = "1";
                    }
                }

                Response.Clear();
                Response.Write(result);
                Response.End();
            }
        }
    }
}