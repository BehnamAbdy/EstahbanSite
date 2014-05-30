using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Public_MarriageLoan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtObj = MarriageLoan.GetCityList();
            this.drpBirthPlace.DataSource = dtObj;
            this.drpMarriageCity.DataSource = dtObj;
            this.drpBirthPlace.DataBind();
            this.drpMarriageCity.DataBind();
            this.drpBirthPlace.Items.Insert(0, new ListItem("- انتخاب کنید -", "0"));
            this.drpMarriageCity.Items.Insert(0, new ListItem("- انتخاب کنید -", "0"));
            this.Form.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) return false;");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            #region Uploads

            System.Drawing.Image image = null;
            if (this.fluBirthCertification1.HasFile)
            {
                if (!(this.fluBirthCertification1.PostedFile.ContentType.Equals("image/pjpeg") ||
                      this.fluBirthCertification1.PostedFile.ContentType.Equals("image/jpeg")))
                {
                    this.lblMessage.Text = "فرمت اسکن صفحه اول شناسنامه jpg نمیباشد";
                    return;
                }

                if (this.fluBirthCertification1.PostedFile.ContentLength > 512000)
                {
                    this.lblMessage.Text = "!سایز اسکن صفحه اول شناسنامه بیش از 500 کیلو بایت میباشد";
                    return;
                }

                image = System.Drawing.Image.FromStream(this.fluBirthCertification1.PostedFile.InputStream);
                if (image.Height > 600 || image.Width > 800)
                {
                    this.lblMessage.Text = "!سایز اسکن صفحه اول شناسنامه 800 در 600 پیکسل نمیباشد";
                    return;
                }

                //if (image.VerticalResolution == 100)
                //{
                //    this.lblMessage.Text = "!رزولوشن اسکن صفحه اول شناسنامه 150 نمیباشد";
                //    return;
                //}
            }
            else
            {
                this.lblMessage.Text = "!اسکن صفحه اول شناسنامه را بارگزاری نمایید";
                return;
            }

            if (this.fluBirthCertification2.HasFile)
            {
                if (!(this.fluBirthCertification2.PostedFile.ContentType.Equals("image/pjpeg") ||
                      this.fluBirthCertification2.PostedFile.ContentType.Equals("image/jpeg")))
                {
                    this.lblMessage.Text = "فرمت اسکن صفحه دوم شناسنامه jpg نمیباشد";
                    return;
                }

                if (this.fluBirthCertification2.PostedFile.ContentLength > 512000)
                {
                    this.lblMessage.Text = "!سایز اسکن صفحه دوم شناسنامه بیش از 500 کیلو بایت میباشد";
                    return;
                }

                image = System.Drawing.Image.FromStream(this.fluBirthCertification2.PostedFile.InputStream);
                if (image.Height > 600 || image.Width > 800)
                {
                    this.lblMessage.Text = "!سایز اسکن صفحه دوم شناسنامه 800 در 600 پیکسل نمیباشد";
                    return;
                }

                //if (image.VerticalResolution == 100)
                //{
                //    this.lblMessage.Text = "!رزولوشن اسکن صفحه اول شناسنامه 150 نمیباشد";
                //    return;
                //}
            }
            else
            {
                this.lblMessage.Text = "!اسکن صفحه دوم شناسنامه را بارگزاری نمایید";
                return;
            }

            if (this.fluNationalCard.HasFile)
            {
                if (!(this.fluNationalCard.PostedFile.ContentType.Equals("image/pjpeg") ||
                      this.fluNationalCard.PostedFile.ContentType.Equals("image/jpeg")))
                {
                    this.lblMessage.Text = "فرمت اسکن کارت ملی jpg نمیباشد";
                    return;
                }

                if (this.fluNationalCard.PostedFile.ContentLength > 512000)
                {
                    this.lblMessage.Text = "!سایز اسکن کارت ملی بیش از 500 کیلو بایت میباشد";
                    return;
                }

                image = System.Drawing.Image.FromStream(this.fluNationalCard.PostedFile.InputStream);
                if (image.Height > 400 || image.Width > 600)
                {
                    this.lblMessage.Text = "!سایز اسکن کارت ملی 800 در 400 پیکسل نمیباشد";
                    return;
                }

                //if (image.VerticalResolution == 100)
                //{
                //    this.lblMessage.Text = "!رزولوشن اسکن صفحه اول شناسنامه 150 نمیباشد";
                //    return;
                //}
            }
            else
            {
                this.lblMessage.Text = "!اسکن کارت ملی را بارگزاری نمایید";
                return;
            }

            if (this.fluMarriageDoc.HasFile)
            {
                if (!(this.fluMarriageDoc.PostedFile.ContentType.Equals("image/pjpeg") ||
                      this.fluMarriageDoc.PostedFile.ContentType.Equals("image/jpeg")))
                {
                    this.lblMessage.Text = "فرمت اسکن سند ازدواج (صفحه اول) jpg نمیباشد";
                    return;
                }

                if (this.fluMarriageDoc.PostedFile.ContentLength > 512000)
                {
                    this.lblMessage.Text = "!سایز اسکن سند ازدواج (صفحه اول) بیش از 500 کیلو بایت میباشد";
                    return;
                }

                image = System.Drawing.Image.FromStream(this.fluMarriageDoc.PostedFile.InputStream);
                if (image.Height > 600 || image.Width > 800)
                {
                    this.lblMessage.Text = "!سایز اسکن سند ازدواج (صفحه اول) 800 در 600 پیکسل نمیباشد";
                    return;
                }

                //if (image.VerticalResolution == 100)
                //{
                //    this.lblMessage.Text = "!رزولوشن اسکن صفحه اول شناسنامه 150 نمیباشد";
                //    return;
                //}
            }
            else
            {
                this.lblMessage.Text = "!اسکن سند ازدواج (صفحه اول) را بارگزاری نمایید";
                return;
            }

            if (this.fluPicture.HasFile)
            {
                if (!(this.fluPicture.PostedFile.ContentType.Equals("image/pjpeg") ||
                      this.fluPicture.PostedFile.ContentType.Equals("image/jpeg")))
                {
                    this.lblMessage.Text = "فرمت اسکن عکس رنگی شخص jpg نمیباشد";
                    return;
                }

                if (this.fluPicture.PostedFile.ContentLength > 512000)
                {
                    this.lblMessage.Text = "!سایز اسکن سند عکس رنگی شخص بیش از 500 کیلو بایت میباشد";
                    return;
                }

                image = System.Drawing.Image.FromStream(this.fluPicture.PostedFile.InputStream);
                if (image.Height > 600 || image.Width > 800)
                {
                    this.lblMessage.Text = "!سایز اسکن عکس رنگی شخص 800 در 600 پیکسل نمیباشد";
                    return;
                }

                //if (image.VerticalResolution == 100)
                //{
                //    this.lblMessage.Text = "!رزولوشن اسکن صفحه اول شناسنامه 150 نمیباشد";
                //    return;
                //}
            }
            else
            {
                this.lblMessage.Text = "!اسکن سند عکس رنگی شخص را بارگزاری نمایید";
                return;
            }

            #endregion

            byte result = 11;
            int counter = 0;
            string refDate = null;
            MarriageLoan.SaveReq(this.txtFirstName.Text.Trim(), this.txtLastName.Text.Trim()
                , this.txtNationalCode.Text.Trim()
                , this.txtMarriageOfficeNo.Text.Trim(), this.txtFather.Text.Trim(), this.txtBirthCertificateNo.Text.Trim()
                , this.txtAddress.Text.Trim(), this.txtAddressWork.Text.Trim()
                , this.txtPhone.Text.Trim(), this.txtPhoneWork.Text.Trim(), this.txtMobile.Text.Trim()
                , this.drpGender.SelectedIndex == 0 ? false : true
                , this.txtPostalCode.Text.Trim()
                , string.IsNullOrEmpty(this.txtBirthDate.Text) ? null : this.txtBirthDate.Text.Replace("/", "")
                , this.drpMarriageCity.SelectedItem.Text, this.txtSposeFirstName.Text.Trim()
                , this.txtSposeLastName.Text.Trim(), this.txtSposeNationalCode.Text.Trim()
                , this.txtSposeBirthCertificateNo.Text.Trim(), string.Empty, this.txtSposeFather.Text.Trim(), 0, 0
                , string.IsNullOrEmpty(this.txtMarriageDate.Text) ? null : this.txtMarriageDate.Text.Replace("/", "")
                , this.txtMarrageNo.Text.Trim(), "", ref result, ref counter, ref refDate);

            if (result == 0)
            {
                System.IO.Directory.CreateDirectory(string.Format(Server.MapPath("~/App_Data/MarriageLoanImages/{0}"), counter));
                this.fluBirthCertification1.PostedFile.SaveAs(string.Format(Server.MapPath("~/App_Data/MarriageLoanImages/{0}/shenasname1.jpg"), counter));
                this.fluBirthCertification2.PostedFile.SaveAs(string.Format(Server.MapPath("~/App_Data/MarriageLoanImages/{0}/shenasname2.jpg"), counter));
                this.fluNationalCard.PostedFile.SaveAs(string.Format(Server.MapPath("~/App_Data/MarriageLoanImages/{0}/cartMelli.jpg"), counter));
                this.fluMarriageDoc.PostedFile.SaveAs(string.Format(Server.MapPath("~/App_Data/MarriageLoanImages/{0}/sanadEzdevaj.jpg"), counter));
                this.fluPicture.PostedFile.SaveAs(string.Format(Server.MapPath("~/App_Data/MarriageLoanImages/{0}/moteghazi.jpg"), counter));
                Response.Redirect(string.Format("~/Public/LoanMessage.aspx?code={0}&date={1}", counter, refDate));
            }
            else
            {
                Response.Redirect("~/Public/LoanMessage.aspx");
            }
        }
    }
}