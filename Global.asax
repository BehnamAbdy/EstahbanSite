<%@ Application Language="C#" %>
<script RunAt="server">

    private void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

        if (authCookie == null)
        {
            return;
        }

        FormsAuthenticationTicket authTicket = null;
        try
        {
            authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        }
        catch
        {
            return;
        }

        if (authTicket == null)
        {
            return;
        }

        System.Security.Principal.GenericIdentity identity = new System.Security.Principal.GenericIdentity(authTicket.Name, "Forms");
        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, new string[] { "Admin" });
    }
    
</script>
