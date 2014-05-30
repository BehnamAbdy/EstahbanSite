<%@ Page Language="C#" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">    
    public void Page_load(object sender, EventArgs e)
    {
        Public.Log("request just arrived");
        if (Request.QueryString["from"] != null &&
            Request.QueryString["to"] != null &&
            Request.QueryString["text"] != null)
        {
            Public.Log("request with parameters");
            Public.Log(Request.QueryString["from"]);
            Public.Log(Request.QueryString["to"]);
            Public.Log(Request.QueryString["text"]);
            Account.ReceiveSms(Request.QueryString["from"].StartsWith(" 98") ? Request.QueryString["from"].Replace(" 98", "0").Trim() : Request.QueryString["from"].Trim()
                                       , Public.GetDate(DateTime.Now)
                                       , Public.GetTime(DateTime.Now)
                                       , Request.QueryString["text"]);
        }
    }    
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
</body>
</html>
