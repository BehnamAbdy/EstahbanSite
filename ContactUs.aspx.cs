using System;
using System.Net.Mail;
using System.Configuration;

public partial class ContactUs : System.Web.UI.Page
{
    public void btnSend_Click(object sender, EventArgs e)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.To.Add(new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["fromEmailAddress"]));
        mailMessage.Subject = this.txtSubject.Text.Trim();
        System.Text.StringBuilder html = new System.Text.StringBuilder("<html><head><title></title></head><body><table><tr><td>");
        html.AppendFormat("From : {0}</td></tr><tr><td>Email : {1}</td></tr>", this.txtName.Text, this.txtEmail.Text);
        //html.AppendFormat("<tr><td>{0}</td></tr></table><p>{1}</p></body></html>", Public.ToPersianDateTime(DateTime.Now), this.txtMessage.Text);
        mailMessage.Body = html.ToString();
        //mailMessage.Body = string.Format("<html><head></head><body><h1 style='color: red;'>Test HTML Email</h1></br>rr</body>");
        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        mailMessage.IsBodyHtml = true;
        SmtpClient smtpClient = new SmtpClient();

        try
        {
            smtpClient.Send(mailMessage);
            Response.Redirect("~/Default.aspx");
        }
        catch (SmtpFailedRecipientsException recExc)
        {
            for (int recipient = 0; recipient < recExc.InnerExceptions.Length - 1; recipient++)
            {
                System.Net.Mail.SmtpStatusCode statusCode;
                statusCode = recExc.InnerExceptions[recipient].StatusCode;

                if ((statusCode == System.Net.Mail.SmtpStatusCode.MailboxBusy) || (statusCode == System.Net.Mail.SmtpStatusCode.MailboxUnavailable))
                {
                    smtpClient.Send(mailMessage);
                }
                else
                {
                    Server.Transfer("~/Error.aspx");
                }
            }
        }
    }
}
