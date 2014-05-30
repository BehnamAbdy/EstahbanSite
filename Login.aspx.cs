using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void btnLogin_Click(object sender, System.EventArgs e)
    {
        if (Page.IsValid)
        {
            if (FormsAuthentication.Authenticate(this.txtUserName.Text.Trim(), this.txtPassword.Text)) // Credentials are valid
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(2, this.txtUserName.Text, DateTime.Now, DateTime.Now.AddMinutes(30), false, "Admin");
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket)));
                FormsAuthentication.GetRedirectUrl(this.txtUserName.Text, false);
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                this.lblMessage.Text = "نام کاربری یا گذرواژه نادرست میباشد";
            }
        }
    }
}