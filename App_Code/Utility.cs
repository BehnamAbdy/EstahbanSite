using System;
using System.Web;
using System.Linq;
using System.Xml.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

[WebService(Namespace = "http://www.SghMahdiyeh.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Utility : System.Web.Services.WebService
{
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string MarqueeSlogan()
    {
        string marquee = HttpContext.Current.Cache["Marquee"] as string;
        string slogan = HttpContext.Current.Cache["Slogan"] as string;
        if (string.IsNullOrEmpty(slogan) || string.IsNullOrEmpty(marquee))
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
                marquee = item.Marquee;
                slogan = item.Slogan;
                HttpContext.Current.Cache.Insert("Marquee", marquee, null, DateTime.MaxValue, TimeSpan.FromMinutes(5));
                HttpContext.Current.Cache.Insert("Slogan", slogan, null, DateTime.MaxValue, TimeSpan.FromMinutes(5));
            }
        }

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(new { Marquee = marquee, Slogan = slogan });
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string BankingInfo(string mode)
    {
        string result = null;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Text.xml"));

        switch (mode)
        {
            case "1": // Internet Bank
                var ibank = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("InternetBank").Value
                });
                result = serializer.Serialize(ibank);
                break;

            case "2": // Telephone Bank
                var telbank = doc.Element("Panels").Elements("Panel").Select(pnl => new
                 {
                     Text = pnl.Element("TelephoneBank").Value
                 });
                result = serializer.Serialize(telbank);
                break;

            case "4":
                var marriage = doc.Element("Panels").Elements("Panel").Select(pnl => new
                 {
                     Text = pnl.Element("MarriageLoan").Value
                 });
                result = serializer.Serialize(marriage);
                break;

            case "5":
                var history = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("History").Value
                });
                result = serializer.Serialize(history);
                break;

            case "6":
                var principle = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Principle").Value
                });
                result = serializer.Serialize(principle);
                break;

            case "7":
                var orgChart = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("OrgChart").Value
                });
                result = serializer.Serialize(orgChart);
                break;

            case "8":
                var courtesy = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Courtesy").Value
                });
                result = serializer.Serialize(courtesy);
                break;

            case "9":
                var council = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Council").Value
                });
                result = serializer.Serialize(council);
                break;

            case "10":
                var slide1 = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Slide1").Value
                });
                result = serializer.Serialize(slide1);
                break;

            case "11":
                var slide2 = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Slide2").Value
                });
                result = serializer.Serialize(slide2);
                break;

            case "12":
                var slide3 = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Slide3").Value
                });
                result = serializer.Serialize(slide3);
                break;

            case "13":
                var slide4 = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Slide4").Value
                });
                result = serializer.Serialize(slide4);
                break;

            case "14":
                var slide5 = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Slide5").Value
                });
                result = serializer.Serialize(slide5);
                break;

            case "15":
                var statistics = doc.Element("Panels").Elements("Panel").Select(pnl => new
                {
                    Text = pnl.Element("Statistics").Value
                });
                result = serializer.Serialize(statistics);
                break;
        }
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string WellcomePanel()
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
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(panel);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string LoanInfo(string id)
    {
        XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/List.xml"));
        var loan = doc.Element("List").Elements("Item").Where(an => an.Attribute("id").Value == id).Select(ln => new { Title = ln.Element("Title").Value, Description = ln.Element("Description").Value });
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(loan);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string News(string id)
    {
        XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/Letters.xml"));
        var text = doc.Element("Letters").Elements("Letter").Where(let => let.Attribute("id").Value == id).Select(let =>
               new
               {
                   Title = let.Element("Title").Value,
                   Text = let.Element("Text").Value,
                   Url = let.Element("Url").Value,
                   Date = let.Element("Date").Value
               });
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(text);
    }
}
