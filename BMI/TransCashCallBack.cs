using System;

public partial class BMI_TransCashCallBack : System.Web.UI.Page
{
    private void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["RefId"] != null &&
                Request.Params["ResCode"] != null &&
                Request.Params["SaleOrderId"] != null &&
                Request.Params["SaleReferenceId"] != null)
            {
                TrnsactionInfo trnsactionInfo = this.Session["TransCashInfo"] as TrnsactionInfo;
                this.Session["TransCashInfo"] = null;

                if (trnsactionInfo != null)
                {
                    long orderId = Public.ToLong(Request.Params["SaleOrderId"]);
                    long saleReferenceId = Public.ToLong(Request.Params["SaleReferenceId"]);
                    BMI.SaveOreder(orderId, Request.Params["RefId"], Request.Params["ResCode"], saleReferenceId);
                    BMIService.PaymentGatewayImplService bmiService = new BMIService.PaymentGatewayImplService();

                    switch (Request.Params["ResCode"])
                    {
                        case "0":
                            string result = bmiService.bpVerifyRequest(long.Parse(Public.TerminalId)
                                , Public.UserName
                                , Public.UserPassword
                                , orderId
                                , orderId
                                , saleReferenceId);

                            if (result == "0")
                            {
                                byte status = 0;
                                BMI.TransferCash(trnsactionInfo.AccountNo, trnsactionInfo.Amount, saleReferenceId, ref status);
                                Public.Log(string.Format("Res Saiid = {0}", status));
                                if (status == 0)
                                {
                                    //result = bmiService.bpInquiryRequest(long.Parse(Public.TerminalId)
                                    //  , Public.UserName
                                    //  , Public.UserPassword
                                    //  , orderId
                                    //  , orderId
                                    //  , saleReferenceId);
                                    //Public.Log(string.Format("Result of bpInquiryRequest = {0}", result));

                                    result = bmiService.bpSettleRequest(long.Parse(Public.TerminalId)
                                        , Public.UserName
                                        , Public.UserPassword
                                        , orderId
                                        , orderId
                                        , saleReferenceId);

                                    if (result == "0")
                                    {
                                        this.lblMessage.Text = string.Format("تراکنش با موفقیت انجام گردید، کدرهگیری تراکنش {0} میباشد", saleReferenceId);
                                    }
                                    else
                                    {
                                        result = bmiService.bpReversalRequest(long.Parse(Public.TerminalId)
                                        , Public.UserName
                                        , Public.UserPassword
                                        , orderId
                                        , orderId
                                        , saleReferenceId);
                                        this.lblMessage.Text = "اشکال در درج سند";
                                    }
                                }
                                else
                                {
                                    result = bmiService.bpReversalRequest(long.Parse(Public.TerminalId)
                                        , Public.UserName
                                        , Public.UserPassword
                                        , orderId
                                        , orderId
                                        , saleReferenceId);
                                }
                            }
                            break;

                        case "17":
                            this.lblMessage.Text = "مشتری گرامی خطا در انجام عملیات بانکی انجام پرداخت با انصراف روبرو شده است.";
                            break;

                        default:
                            this.lblMessage.Text = "مشتری گرامی خطا در انجام عملیات بانکی.";
                            Public.Log(string.Format("ResCode = {0}", Request.Params["ResCode"]));
                            break;
                    }
                }
            }
        }
    }
}