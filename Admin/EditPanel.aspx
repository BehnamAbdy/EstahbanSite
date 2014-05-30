<%@ Page Title="ویرایش پنل صفحه نخست سایت" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="EditPanel.aspx.cs" Inherits="Admin_EditPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        ویرایش پنل صفحه نخست سایت
    </div>
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" style="margin: 10px 50px;">
                <tr>
                    <td valign="middle">
                        عنوان
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        متن
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="textbox" TextMode="MultiLine"
                            Height="140px" Width="440px" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        آیتمهای لیست
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtList" runat="server" CssClass="textbox" TextMode="MultiLine"
                            Height="120px" Width="440px" />
                    </td>
                </tr>
                <tr>
                    <td style="color: #f9770d; font-size: 14px;">
                        آیتم های لیست را با <b style="color: red; font-size: 17px;">$</b> جدا کنید
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="ذخیره" OnClick="btnSave_Click" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="lbl-msg" EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
