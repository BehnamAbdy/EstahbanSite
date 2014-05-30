using System;

public partial class Account_Loan : System.Web.UI.Page
{
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            string captcha = Session["Captcha4"] as string;
            if (captcha != null && this.txtAnswer.Text.ToLower() == captcha.ToLower())
            {
                string fromName = null;
                string fromHName = null;
                string toName = null;
                string toHName = null;
                string amount = null;
                byte outputState = 10;
                Account.PreLoan(this.txtFromAccountNo.Text.Trim(), this.txtToAccountNo.Text.Trim(), this.txtPassword.Text
                                      , ref fromName, ref fromHName, ref toName, ref toHName, ref amount, ref outputState);

                switch (outputState)
                {
                    case 0:
                        this.txtFromAccountNo.ReadOnly = true;
                        this.txtToAccountNo.ReadOnly = true;
                        this.txtPassword.ReadOnly = true;
                        this.txtAnswer.ReadOnly = true;
                        this.btnCheck.Enabled = false;
                        this.pnlConfirm.Visible = true;
                        this.lblMessage.Text = null;
                        this.lblFromAccountOwner.Text = fromName;
                        this.lblFromAccountType.Text = fromHName;
                        this.lblAmount.Text = amount;
                        this.lblToAccountOwner.Text = toName;
                        this.lblToAccountType.Text = toHName;
                        this.ViewState["Pass"] = this.txtPassword.Text;
                        return;

                    case 1:
                        this.lblMessage.Text = "حساب برداشتی موجود نمي باشد";
                        break;

                    case 2:
                        this.lblMessage.Text = "گذرواژه حساب برداشتی تعريف نشده يا نادرست میباشد";
                        break;

                    case 3:
                        this.lblMessage.Text = "حساب گیرنده وام موجود نمي باشد";
                        break;

                    case 4:
                        this.lblMessage.Text = "حساب برداشتی حواله يا پس انداز نمي باشد";
                        break;

                    case 5:
                        this.lblMessage.Text = "حساب دوم وام نمي باشد";
                        break;

                    case 6:
                        this.lblMessage.Text = "وام معرفي شده مستهلک گرديده است";
                        break;

                    case 7:
                        this.lblMessage.Text = "حساب برداشتی داراي حواله فرم خورده ميباشد";
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
            int followNo = 0;
            byte outputState = 0;
            Account.PayLoan(this.txtFromAccountNo.Text.Trim(), this.txtToAccountNo.Text.Trim(), this.ViewState["Pass"].ToString(), Convert.ToDecimal(this.lblAmount.Text), ref followNo, ref outputState);

            switch (outputState)
            {
                case 0:
                    this.lblMessage.Text = string.Format("تراکنش با موفقیت انجام گردید، کدرهگیری تراکنش {0} میباشد", followNo);
                    return;

                case 1:
                    this.lblMessage.Text = "حساب برداشتی موجود نمي باشد";
                    break;

                case 2:
                    this.lblMessage.Text = "حساب گیرنده موجود نمي باشد";
                    break;

                case 3:
                    this.lblMessage.Text = "حساب برداشتی حواله يا پس انداز نمي باشد";
                    break;

                case 4:
                    this.lblMessage.Text = "حساب گیرنده حواله ، پس انداز يا وام نمي باشد";
                    break;

                case 5:
                    this.lblMessage.Text = "گذرواژه حساب برداشتی تعريف نشده يا نادرست میباشد";
                    break;

                case 6:
                    this.lblMessage.Text = "مبلغ وارده از مبلغ قابل برداشت بيشتر ميباشد";
                    break;

                case 7:
                    this.lblMessage.Text = "حساب برداشتی داراي حواله فرم خورده ميباشد";
                    break;

                case 8:
                    this.lblMessage.Text = "مبلغ وارده با مبلغ قسط مغايرت دارد";
                    break;

                case 9:
                    this.lblMessage.Text = "وام معرفي شده مستهلک گرديده است";
                    break;

                case 10:
                    this.lblMessage.Text = "اشکال در درج سند";
                    break;
            }

            this.btnConfirm.Enabled = false;
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script type='text/javascript' > pageTimer.Timer.stop(); </script>", false);
        }
    }
}
