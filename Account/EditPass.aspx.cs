using System;

public partial class Account_EditPass : System.Web.UI.Page
{
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            string captcha = Session["Captcha6"] as string;
            if (captcha != null && this.txtAnswer.Text.ToLower() == captcha.ToLower())
            {
                string name = null;
                string accountType = null;
                string mobile = null;
                byte outputState = 10;
                Account.PreEditPassword(this.txtAccountNo.Text.Trim(), this.txtPassword.Text, ref name, ref accountType, ref mobile, ref outputState);

                switch (outputState)
                {
                    case 0:
                        this.txtAccountNo.ReadOnly = true;
                        this.txtPassword.ReadOnly = true;
                        this.txtAnswer.ReadOnly = true;
                        this.btnCheck.Enabled = false;
                        this.pnlConfirm.Visible = true;
                        this.lblMessage.Text = null;
                        this.lblAccountOwner.Text = name;
                        this.lblAccountType.Text = accountType;
                        this.ViewState["Pass"] = this.txtPassword.Text;
                        return;

                    case 1:
                        this.lblMessage.Text = "حساب موجود نمي باشد";
                        break;

                    case 2:
                        this.lblMessage.Text = "حساب دارای گذرواژه نمیباشد";
                        break;

                    case 3:
                        this.lblMessage.Text = "گذرواژه نادرست میباشد";
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

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (this.pnlConfirm.Visible && this.ViewState["Pass"] != null)
        {
            byte outputState = 0;
            Account.EditPassword(this.txtAccountNo.Text.Trim(), this.ViewState["Pass"].ToString(), ref outputState);

            switch (outputState)
            {
                case 0:
                    this.lblMessage.Text = "ویرایش گذرواژه موفقیت انجام گردید";
                    return;

                case 1:
                    this.lblMessage.Text = "حساب موجود نمي باشد";
                    break;

                case 10:
                    this.lblMessage.Text = "اشکال در ویرایش گذرواژه";
                    break;
            }

            this.btnConfirm.Enabled = false;
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script type='text/javascript' > pageTimer.Timer.stop(); </script>", false);
        }
    }
}
