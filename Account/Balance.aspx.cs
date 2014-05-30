using System;
using System.Text;

public partial class Account_Balance : System.Web.UI.Page
{
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            string captcha = Session["Captcha1"] as string;
            if (captcha != null && this.txtAnswer.Text.ToLower() == captcha.ToLower())
            {
                string name = null;
                string rem1 = null;
                string rem2 = null;
                string hType = null;
                byte outputState = 10;
                Account.Balance(this.txtAccountNo.Text.Trim(), this.txtPassword.Text, ref name, ref rem1, ref rem2, ref hType, ref outputState);

                switch (outputState)
                {
                    case 0:
                        this.btnCheck.Enabled = false;
                        this.tblResult.Visible = true;
                        this.lblMessage.Text = null;
                        this.lblAccountOwner.Text = name;
                        this.lblAccountType.Text = hType;
                        this.lblBalance1.Text = rem1;
                        this.lblBalance2.Text = rem2;
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script type='text/javascript' > pageTimer.Timer.stop(); </script>", false);
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
