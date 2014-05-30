<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        تماس با ما
    </div>
    <div id="component-contact">
        <table style="margin-right: 60px; width: 400px;">
            <tbody>
                <tr>
                    <td colspan="2" style="width: 100%">
                        <div class="contact_email">
                            <label for="contact_name">
                                نام خود را وارد کنید:
                            </label>
                            <br />
                            <asp:TextBox ID="txtName" runat="server" CssClass="textbox" name="name" ValidationGroup="message"
                                Width="150px" />
                            (اختیاری)<br />
                            <label for="contact_email" id="contact_emailmsg">
                                ایمیل خود را وارد کنید:
                            </label>
                            <br />
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" name="email" ValidationGroup="message"
                                Width="150px" />
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="لطفا ایمیل خود را وارد کنید." ValidationGroup="message">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="ایمیل وارد شده معتبر نمی باشد." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="message">*</asp:RegularExpressionValidator>
                            <br />
                            <label for="contact_subject">
                                موضوع پیام:
                            </label>
                            <br />
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="textbox" name="subject" ValidationGroup="message"
                                Width="150px" />
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                                ErrorMessage="لطفا موضوع پیام خود را وارد کنید." ValidationGroup="message">*</asp:RequiredFieldValidator>
                            <br />
                            <label for="contact_text" id="contact_textmsg">
                                پیام خود را وارد کنید:
                            </label>
                            <br />
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="textbox"
                                Width="350px" Height="100px"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMessage"
                                ErrorMessage="لطفا پیام خود را وارد کنید." ValidationGroup="message">*</asp:RequiredFieldValidator>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSend" runat="server" CssClass="button" OnClick="btnSend_Click" Text="ارسال"
                            ValidationGroup="message" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
