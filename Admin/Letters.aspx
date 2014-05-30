<%@ Page Title="ویرایش متن بخشنامه های سازمان" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" ValidateRequest="false" CodeFile="Letters.aspx.cs" Inherits="Site_Letters" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <link href="../Styles/date.css" rel="stylesheet" type="text/css" />
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="30" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div class="page-title">
        ویرایش متن بخشنامه های سازمان
    </div>
    <table style="margin: 10px 90px 10px 0;">
        <tr>
            <td class="fieldName"></td>
            <td>
                <asp:DropDownList ID="drpLetters" runat="server" CssClass="dropdown" DataTextField="Title" Width="245px"
                    DataValueField="Id" OnSelectedIndexChanged="drpLetters_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/App_Themes/Default/images/delete.png" ToolTip="حذف" OnClientClick="javascript:return preDeleteItem()" OnClick="btnDelete_Click" />
            </td>
        </tr>
        <tr>
            <td class="fieldName">عنوان :
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox" Width="240px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" ErrorMessage="عنوان را وارد نمایید" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">URL :
            </td>
            <td>
                <asp:TextBox ID="txtUrl" runat="server" CssClass="textbox" Width="240px" Style="direction: ltr;" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="فرمت نادرست میباشد" ControlToValidate="txtUrl" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="fieldName">تاریخ :
            </td>
            <td>
                <pdc:PersianDateTextBox ID="txtDate" runat="server" CssClass="textbox" IconUrl="~/App_Themes/Default/images/calendar.gif"
                    Width="100px">
                </pdc:PersianDateTextBox>
            </td>
        </tr>
    </table>
    <center>
        <textarea id="txtEditor" class="txtEditor" name="txtEditor" runat="server" cols="60"
            rows="10" style="height: 250px; width: 600px;"></textarea>
    </center>
    <div class="pane-left" style="clear: both; margin-top: 15px;">
        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="ذخیره" OnClick="btnSave_Click" />
        <asp:Label ID="lblMessage" runat="server" CssClass="lbl-msg" EnableViewState="false"></asp:Label>
    </div>
    <%--<script src="../Scripts/javascript.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.cleditor.min.js" type="text/javascript"></script>
    <link href="../Styles/jquery.cleditor.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">

        //$(document).ready(function () {
        //    $('.txtEditor').cleditor();
        //});

        function preDeleteItem() {
            //if ($get('<%=drpLetters.ClientID %>').selectedIndex > 0) {
            return confirm('آیا خبر موردنظر حذف گردد؟');
            //}
        }

    </script>
</asp:Content>
