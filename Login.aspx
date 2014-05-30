<%@ Page Title="ورود به سامانه صندوق قرض الحسنه مهدیه استهبان" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        ورود به مدیریت سایت
    </div>
    <div id="login-box">
        <div id="login-header">
        </div>
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
            <table style="margin: 40px 30px;">
                <tr>
                    <td style="color: #99aac4; font-size: 14px; text-align: left; font-weight: bold;">
                        نام کاربری :
                    </td>
                    <td style="width: 10px;">
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox-login" />
                        <asp:RequiredFieldValidator ID="valRequireUserName" runat="server" ControlToValidate="txtUserName"
                            SetFocusOnError="True" Text="*" ValidationGroup="Login" />
                    </td>
                </tr>
                <tr>
                    <td height="8px">
                    </td>
                    <td height="8px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="color: #99aac4; font-size: 14px; text-align: left; font-weight: bold;">
                        گذرواژه :
                    </td>
                    <td style="width: 10px;">
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox-login" />
                        <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" ControlToValidate="txtPassword"
                            SetFocusOnError="True" Text="*" ValidationGroup="Login" />
                    </td>
                </tr>
                <tr>
                    <td height="8px">
                    </td>
                    <td height="8px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="width: 10px;">
                    </td>
                    <td>
                        <asp:Button ID="btnLogin" runat="server" CssClass="button" ValidationGroup="Login"
                            OnClick="btnLogin_Click" Text="ورود" Width="55px" />
                        <asp:Label ID="lblMessage" runat="server" ForeColor="#fd6f42" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
