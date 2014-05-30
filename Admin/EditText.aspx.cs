using System;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class Admin_EditText : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["mode"] != null)
            {
                string result = null;
                IEnumerable<XElement> element = null;
                XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Text.xml"));
                switch (Request.Params["mode"])
                {
                    case "1": // Internet Bank
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("InternetBank").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "2": // Telephone Bank
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("TelephoneBank").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "3": // Sms Bank
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("SmsBank").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "4": // Marriage Loan
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("MarriageLoan").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "5": // History
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Histoty").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "6": // Priciple
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Principle").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "7": // OrgChart
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("OrgChart").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "8": // Courtesy
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Courtesy").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "9": // Council
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Council").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "10": // Slide1
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Slide1").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "11": // Slide2
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Slide2").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "12": // Slide3
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Slide3").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "13": // Slide4
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Slide4").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "14": // Slide5
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Slide5").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;

                    case "15": // Statistics
                        element = doc.Element("Panels").Elements("Panel");
                        foreach (XElement item in element)
                        {
                            item.Element("Statistics").Value = Request.Params["txt"];
                            doc.Save(Server.MapPath("~/App_Data/Text.xml"));
                            result = "1";
                        }
                        break;
                }

                Response.Clear();
                Response.Write(result);
                Response.End();
            }
        }
    }
}