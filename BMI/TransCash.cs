using System;
using System.Web.UI;

public partial class BMI_TransCash : Page
{
    protected readonly string PgwSite = Public.PgwSite;

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            string captcha = Session["Captcha8"] as string;
            if (captcha != null && this.txtAnswer.Text.ToLower() == captcha.ToLower())
            {
                string hType = null;
                string mobile = null;
                string name = null;
                byte outputState = 1;
                BMI.PreTransaction(this.txtAccountNo.Text.Trim(), ref name, ref mobile, ref hType, ref outputState);

                switch (outputState)
                {
                    case 0:
                        this.txtAccountNo.ReadOnly = true;
                        this.txtAnswer.ReadOnly = true;
                        this.btnCheck.Enabled = false;
                        this.pnlConfirm.Visible = true;
                        this.lblMessage.Text = null;
                        this.lblAccountOwner.Text = name;
                        this.lblAccountType.Text = hType;
                        this.Session["TransCashInfo"] = new TrnsactionInfo { AccountNo = this.txtAccountNo.Text.Trim() };
                        return;

                    case 1:
                        this.lblMessage.Text = "حساب واریزی موجود نمي باشد";
                        break;

                    case 2:
                        this.lblMessage.Text = "حساب برداشتی حواله يا پس انداز نمي باشد";
                        break;
                }
            }
            else
            {
                this.lblMessage.Text = "کد امنیتی نادرست میباشد";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script type='text/javascript' > loadCaptcha(); </script>", false);
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        TrnsactionInfo trnsactionInfo = this.Session["TransCashInfo"] as TrnsactionInfo;
        if (this.pnlConfirm.Visible && trnsactionInfo != null)
        {
            try
            {
                trnsactionInfo.Amount = Request["hdnAmount"];
                if (int.Parse(trnsactionInfo.Amount) <= 0)
                {
                    this.lblMessage.Text = "مبلغ را وارد نمایید";
                    return;
                }
                this.Session["TransCashInfo"] = trnsactionInfo;
                BMIService.PaymentGatewayImplService bmiService = new BMIService.PaymentGatewayImplService();
                string result = bmiService.bpPayRequest(long.Parse(Public.TerminalId)
                                                      , Public.UserName
                                                      , Public.UserPassword
                                                      , BMI.GetOrderID()
                                                      , Convert.ToInt64(trnsactionInfo.Amount)
                                                      , Public.CurrentDateForBMI
                                                      , Public.CurrentTimeForBMI
                                                      , this.txtAccountNo.Text
                                                      , "http://www.sghMahdiyeh.ir/BMI/TransCashCallBack.aspx"
                                                      , 0);

                string[] resultArray = result.Split(',');
                if (resultArray[0] == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                }
            }
            catch (Exception ex)
            {
                Public.Log(ex);
            }
        }
    }
}
