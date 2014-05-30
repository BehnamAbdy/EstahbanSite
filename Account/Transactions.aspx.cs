using System;

public partial class Account_Transactions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtAccountNo.Focus();
            this.txtDateTo.Text = Public.ToPersianDate(DateTime.Now);
        }
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            string captcha = Session["Captcha2"] as string;
            if (captcha != null && this.txtAnswer.Text.ToLower() == captcha.ToLower())
            {
                string name = null;
                string dateFrom = string.IsNullOrEmpty(this.txtDateFrom.Text) ? null : this.txtDateFrom.Text.Replace("/", "");
                string dateTo = string.IsNullOrEmpty(this.txtDateTo.Text) ? null : this.txtDateTo.Text.Replace("/", "");
                string hType = null;
                string amount1 = null;
                string amount2 = null;
                byte outputState = 10;
                System.Data.DataTable dtObj = Account.Transactions(this.txtAccountNo.Text.Trim(), this.txtPassword.Text, dateFrom, dateTo, ref name, ref hType, ref  amount1, ref  amount2, ref outputState);

                switch (outputState)
                {
                    case 0:
                        this.btnCheck.Enabled = false;
                        this.tblResult.Visible = true;
                        this.lblMessage.Text = null;
                        this.lblAccountOwner.Text = name;
                        this.lblAccountType.Text = hType;
                        this.lblBalance1.Text = amount1;
                        this.lblBalance2.Text = amount2;
                        this.lstTransactions.DataSource = dtObj;
                        this.lstTransactions.DataBind();
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script type='text/javascript' > pageTimer.Timer.stop();</script>", false);
                        return;

                    case 1:
                        this.lblMessage.Text = "حساب وجود ندارد";
                        break;

                    case 2:
                        this.lblMessage.Text = "حساب دارای رمز نمی باشد";
                        break;

                    case 3:
                        this.lblMessage.Text = "حساب پس انداز , جاری یا وام نمی باشد";
                        break;

                    case 4:
                        this.lblMessage.Text = "گذرواژه حساب نادرست میباشد";
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