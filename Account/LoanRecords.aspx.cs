using System;

public partial class Account_LoanRecords : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtAccountNo.Focus();
        }
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            string captcha = Session["Captcha5"] as string;
            if (captcha != null && this.txtAnswer.Text.ToLower() == captcha.ToLower())
            {
                string name = null;
                string payDate = null;
                string amount = null;
                byte outputState = 10;
                System.Data.DataTable dtObj = Account.LoanRecords(this.txtAccountNo.Text.Trim(), this.txtPassword.Text, ref amount, ref payDate, ref name, ref outputState);

                switch (outputState)
                {
                    case 0:
                        this.btnCheck.Enabled = false;
                        this.tblResult.Visible = true;
                        this.lblMessage.Text = null;
                        this.lblName.Text = name;
                        this.lblDate.Text = Public.ToSeparatedPersianDate(payDate);
                        this.lblAmount.Text = amount;
                        this.lstTransactions.DataSource = dtObj;
                        this.lstTransactions.DataBind();
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script type='text/javascript' > pageTimer.Timer.stop(); </script>", false);
                        return;

                    case 1:
                        this.lblMessage.Text = "حساب وجود ندارد";
                        break;

                    case 2:
                        this.lblMessage.Text = "حساب دارای رمز نمی باشد";
                        break;

                    case 10:
                        this.lblMessage.Text = "خطا در انجام عملیات";
                        break;
                }
            }
            else
            {
                this.lblMessage.Text = "کد امنیتی نادرست میباشد";
            }
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script type='text/javascript' > loadCaptcha(); </script>", false);
        }
    }
}