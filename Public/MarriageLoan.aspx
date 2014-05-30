<%@ Page Title="درخواست وام ازدواج" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MarriageLoan.aspx.cs" Inherits="Public_MarriageLoan" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <link href="../Styles/date.css" rel="stylesheet" type="text/css" />
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="30" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div class="page-title">
        درخواست وام ازدواج
    </div>
    <table style="margin-right: 40px;">
        <tr>
            <td colspan="2">
                <h2>مشخصات متقاضی
                </h2>
            </td>
        </tr>
        <tr>
            <td class="fieldName">کد ملی :
            </td>
            <td>
                <asp:TextBox ID="txtNationalCode" runat="server" CssClass="textbox-medium" MaxLength="10"
                    onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNationalCode"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="کد ملی را وارد کنید" CssClass="validator"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="کد ملی باید 10 رقم باشد"
                    ControlToValidate="txtNationalCode" ClientValidationFunction="nationalCodeValidate"
                    CssClass="validator"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">نام :
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox-medium" MaxLength="20"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFirstName"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="نام را وارد کنید" CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">نام خانوادگی :
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox-medium" MaxLength="70"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLastName"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="نام خانوادگی را وارد کنید"
                    CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">نام پدر :
            </td>
            <td>
                <asp:TextBox ID="txtFather" runat="server" CssClass="textbox-medium" MaxLength="20"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFather"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="نام پدر را وارد کنید"
                    CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">شماره شناسنامه :
            </td>
            <td>
                <asp:TextBox ID="txtBirthCertificateNo" runat="server" CssClass="textbox-medium" MaxLength="15"
                    onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtBirthCertificateNo"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="شماره شناسنامه را وارد کنید"
                    CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
           <tr>
            <td class="fieldName">تاریخ تولد :
            </td>
            <td>
                <pdc:PersianDateTextBox ID="txtBirthDate" runat="server" CssClass="textbox-medium" IconUrl="~/App_Themes/Default/images/calendar.gif"
                    Width="100px">
                </pdc:PersianDateTextBox>
            </td>
        </tr>
        <tr>
            <td class="fieldName">محل تولد :
            </td>
            <td>
                <asp:DropDownList ID="drpBirthPlace" runat="server" CssClass="dropdown-medium" DataValueField="City_Iden" DataTextField="City_Name">
                </asp:DropDownList>
                <span class="star">*</span>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="محل تولد را انتخاب کنید"
                    CssClass="validator" ControlToValidate="drpBirthPlace" ValueToCompare="0" Operator="GreaterThan"
                    Type="Integer"></asp:CompareValidator>
            </td>
        </tr>

        <tr>
            <td class="fieldName">جنسیت :
            </td>
            <td>
                <asp:DropDownList ID="drpGender" runat="server" CssClass="dropdown-medium">
                    <asp:ListItem Value="0" Text="مرد"></asp:ListItem>
                    <asp:ListItem Value="1" Text="زن"></asp:ListItem>
                </asp:DropDownList>
                <span class="star">*</span>
            </td>
        </tr>


        <tr>
            <td class="fieldName">تلفن منزل :
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" MaxLength="14" CssClass="textbox-medium" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
                <span class="star">*</span>
            </td>
        </tr>
        <tr>
            <td class="fieldName">تلفن محل کار :
            </td>
            <td>
                <asp:TextBox ID="txtPhoneWork" runat="server" MaxLength="14" CssClass="textbox-medium" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td class="fieldName">تلفن همراه :
            </td>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" MaxLength="11" CssClass="textbox-medium" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMobile"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="تلفن همراه را وارد کنید"
                    CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">تاریخ عقد :
            </td>
            <td>
                <pdc:PersianDateTextBox ID="txtMarriageDate" runat="server" CssClass="textbox-medium" IconUrl="~/App_Themes/Default/images/calendar.gif"
                    Width="100px">
                </pdc:PersianDateTextBox>
            </td>
        </tr>
        <tr>
            <td class="fieldName">شهرستان محل عقد :
            </td>
            <td>
                <asp:DropDownList ID="drpMarriageCity" runat="server" CssClass="dropdown-medium" DataValueField="City_Iden" DataTextField="City_Name">
                </asp:DropDownList>
                <span class="star">*</span>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="محل عقد را انتخاب کنید"
                    CssClass="validator" ControlToValidate="drpMarriageCity" ValueToCompare="0" Operator="GreaterThan"
                    Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">شماره سریال سند عقد :
            </td>
            <td>
                <asp:TextBox ID="txtMarrageNo" runat="server" CssClass="textbox-medium" MaxLength="20"></asp:TextBox>
                <span class="star">*</span>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMarrageNo"
                            SetFocusOnError="true" Display="Dynamic" ErrorMessage="شماره سریال سند عقد را وارد کنید" CssClass="validator"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="fieldName">شماره دفترخانه :
            </td>
            <td>
                <asp:TextBox ID="txtMarriageOfficeNo" runat="server" CssClass="textbox-medium" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fieldName">کد پستی :
            </td>
            <td>
                <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="10" CssClass="textbox-medium"
                    onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="validator" ControlToValidate="txtPostalCode"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="کد پستی را وارد کنید"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator4" runat="server" CssClass="validator" ErrorMessage="کد پستی باید 10 رقم باشد"
                    ControlToValidate="txtPostalCode" ClientValidationFunction="nationalCodeValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">آدرس محل سکونت :
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" Height="40px" Width="350px" TextMode="MultiLine"
                    MaxLength="100" CssClass="textbox-medium"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="validator" ErrorMessage="آدرس را وارد کنید"
                    ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">آدرس محل کار :
            </td>
            <td>
                <asp:TextBox ID="txtAddressWork" runat="server" Height="40px" Width="350px" TextMode="MultiLine"
                    MaxLength="100" CssClass="textbox-medium"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <h2>مشخصات همسر
                </h2>
            </td>
        </tr>
        <tr>
            <td class="fieldName">کد ملی :
            </td>
            <td>
                <asp:TextBox ID="txtSposeNationalCode" runat="server" CssClass="textbox-medium" MaxLength="10"
                    onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSposeNationalCode"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="کد ملی را وارد کنید" CssClass="validator"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="کد ملی باید 10 رقم باشد"
                    ControlToValidate="txtSposeNationalCode" ClientValidationFunction="nationalCodeValidate"
                    CssClass="validator"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">نام :
            </td>
            <td>
                <asp:TextBox ID="txtSposeFirstName" runat="server" CssClass="textbox-medium" MaxLength="20"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSposeFirstName"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="نام را وارد کنید" CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">نام خانوادگی :
            </td>
            <td>
                <asp:TextBox ID="txtSposeLastName" runat="server" CssClass="textbox-medium" MaxLength="70"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSposeLastName"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="نام خانوادگی را وارد کنید"
                    CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">نام پدر :
            </td>
            <td>
                <asp:TextBox ID="txtSposeFather" runat="server" CssClass="textbox-medium" MaxLength="20"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtSposeFather"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="نام پدر را وارد کنید"
                    CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">شماره شناسنامه :
            </td>
            <td>
                <asp:TextBox ID="txtSposeBirthCertificateNo" runat="server" CssClass="textbox-medium" MaxLength="15"
                    onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
                <span class="star">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtSposeBirthCertificateNo"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="شماره شناسنامه را وارد کنید"
                    CssClass="validator"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table style="margin-right: 30px;">
        <tr>
            <td colspan="2">
                <h2>اسکن مدارک
                </h2>
            </td>
        </tr>
        <tr>
            <td></td>
            <td class="star">رزولوشن (150) و با فرمت (JPG) و حداکثر اندازه 500 کیلوبایت و سایز 800 پیکسل (پهنا) در 600 پیکسل (درازا)
            </td>
        </tr>
        <tr>
            <td class="fieldName" style="width: 110px;">شناسنامه صفحه اول :
            </td>
            <td>
                <asp:FileUpload ID="fluBirthCertification1" runat="server" Width="300px" />
            </td>
        </tr>
        <tr style="height: 10px;">
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td class="star">رزولوشن (150) و با فرمت (JPG) و حداکثر اندازه 500 کیلوبایت و سایز 800 پیکسل (پهنا) در 600 پیکسل (درازا)
            </td>
        </tr>
        <tr>
            <td class="fieldName">شناسنامه صفحه دوم :
            </td>
            <td>
                <asp:FileUpload ID="fluBirthCertification2" runat="server" Width="300px" />
            </td>
        </tr>
        <tr style="height: 10px;">
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td class="star">رزولوشن (150) و با فرمت (JPG) و حداکثر اندازه 500 کیلوبایت و سایز 600 پیکسل (پهنا) در 400 پیکسل (درازا)
            </td>
        </tr>
        <tr>
            <td class="fieldName">کارت ملی :
            </td>
            <td>
                <asp:FileUpload ID="fluNationalCard" runat="server" Width="300px" />
            </td>
        </tr>
        <tr style="height: 10px;">
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td class="star">رزولوشن (150) و با فرمت (JPG) و حداکثر اندازه 1.5 مگابایت و سایز 800 پیکسل (پهنا) در 600 پیکسل (درازا)
            </td>
        </tr>
        <tr>
            <td class="fieldName">سند ازدواج (صفحه اول) :
            </td>
            <td>
                <asp:FileUpload ID="fluMarriageDoc" runat="server" Width="300px" />
            </td>
        </tr>
        <tr style="height: 10px;">
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td class="star">رزولوشن (150) وبا فرمت (JPG) و حداکثر اندازه 500 کیلوبایت و سایز 800 پیکسل (پهنا) در 600 پیکسل (درازا)
            </td>
        </tr>
        <tr>
            <td class="fieldName">عکس رنگی شخص :
            </td>
            <td>
                <asp:FileUpload ID="fluPicture" runat="server" Width="300px" />
            </td>
        </tr>
    </table>
    <div style="margin: 5px 157px 0; width: 600px;">
        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="ثبت" OnClick="btnSave_Click" />
        <asp:Label ID="lblMessage" runat="server" CssClass="lbl-msg" EnableViewState="false" />
    </div>
    <script src="../Scripts/javascript.js"></script>
</asp:Content>

