<%@ Page Title="ویرایش شعار صفحه نخست سایت" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="EditSlogan.aspx.cs" Inherits="Admin_EditSlogan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        ویرایش شعار صفحه نخست سایت
    </div>
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" style="margin: 10px 50px;">
                <tr>
                    <td valign="middle">
                        متن متحرک
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMarquee" runat="server" CssClass="textbox" Style="background-color: #d72f33;
                            color: #fefefe; font-weight: bold; padding: 0 5px; width: 350px" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        متن شعار
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSlogan" runat="server" CssClass="textbox" Style="background-color: #f7f7f7;
                            color: #0066cc; font-weight: bold; padding: 0 5px; width: 350px" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px;">
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
