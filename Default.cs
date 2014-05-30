using System;
using System.IO;
using System.Net;

public partial class Default : System.Web.UI.Page
{
    protected int[] cuttentDate = Persia.Calendar.ConvertToPersian(DateTime.Now).ArrayType;

    private void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int[] date = Persia.Calendar.ConvertToPersian(DateTime.Now.AddDays(-5)).ArrayType;
            WebRequest webRequest = WebRequest.Create(string.Concat("http://www.tabnak.ir/fa/prayer/time/17/17_117/", date[0], date[1], date[2]));
            webRequest.Method = "GET";
            WebResponse response = webRequest.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            this.time.InnerHtml = responseFromServer;
        }
    }
}