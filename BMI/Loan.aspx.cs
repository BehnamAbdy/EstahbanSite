using System;
using System.Web.UI;

public partial class BMI_Loan : Page
{
    protected readonly string PgwSite = Public.PgwSite;

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            string captcha = Session["Captcha7"] as string;
            if (captcha != null && this.txtAnswer.Text.ToLower() == captcha.ToLower())
            {
                string payDate = null;
                string name = null;
                int amount = 0;
                byte outputState = 1;
                BMI.PreLoan(this.txtAccountNo.Text.Trim(), ref amount, ref payDate, ref name, ref outputState);

                switch (outputState)
                {
                    case 0:
                        this.txtAccountNo.ReadOnly = true;
                        this.txtAnswer.ReadOnly = true;
                        this.btnCheck.Enabled = false;
                        this.pnlConfirm.Visible = true;
                        this.lblMessage.Text = null;
                        this.lblAccountOwner.Text = name;
                        this.lblAmount.Text = amount.ToString("#,##0");
                        this.lblDate.Text = Public.ToSeparatedPersianDate(payDate);
                        this.Session["LoanInfo"] = new TrnsactionInfo { AccountNo = this.txtAccountNo.Text.Trim(), Amount = amount.ToString() };
                        return;

                    case 1:
                        this.lblMessage.Text = "حساب گیرنده وام موجود نمي باشد";
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
        TrnsactionInfo trnsactionInfo = this.Session["LoanInfo"] as TrnsactionInfo;
        if (this.pnlConfirm.Visible && trnsactionInfo != null)
        {
            try
            {
                BMIService.PaymentGatewayImplService bmiService = new BMIService.PaymentGatewayImplService();
                string result = bmiService.bpPayRequest(long.Parse(Public.TerminalId)
                                                      , Public.UserName
                                                      , Public.UserPassword
                                                      , BMI.GetOrderID()
                                                      , Convert.ToInt64(trnsactionInfo.Amount)
                                                      , Public.CurrentDateForBMI
                                                      , Public.CurrentTimeForBMI
                                                      , this.txtAccountNo.Text
                                                      , "http://www.sghMahdiyeh.ir/BMI/LoanCallBack.aspx"
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
