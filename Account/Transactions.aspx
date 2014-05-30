<%@ Page Title="دریافت صورتحساب" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Transactions.aspx.cs" Inherits="Account_Transactions" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <link href="../Styles/date.css" rel="stylesheet" type="text/css" />
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="30" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <div class="page-title">
                    دریافت صورتحساب
                </div>
            </td>
            <td>
                <div id="date">
                    <%=Persia.Calendar.ConvertToPersian(DateTime.Now).Weekday%>
                </div>
            </td>
        </tr>
    </table>
    <div id="countdown">
        04:00:00
    </div>
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnCheck">
                <table style="margin: 10px 70px 0 0;">
                    <tr>
                        <td class="fieldName">از تاریخ :
                        </td>
                        <td class="date">
                            <pdc:PersianDateTextBox ID="txtDateFrom" runat="server" CssClass="textbox" IconUrl="~/App_Themes/Default/images/calendar.gif"
                                Width="100px">
                            </pdc:PersianDateTextBox>
                        </td>
                        <td style="width: 250px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="تاریخ را وارد نمایید"
                                ControlToValidate="txtDateFrom" CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">تا تاریخ :
                        </td>
                        <td>
                            <pdc:PersianDateTextBox ID="txtDateTo" runat="server" CssClass="textbox" IconUrl="~/App_Themes/Default/images/calendar.gif"
                                Width="100px">
                            </pdc:PersianDateTextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="تاریخ را وارد نمایید"
                                ControlToValidate="txtDateTo" CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">شماره حساب :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccountNo" runat="server" CssClass="textbox" MaxLength="15" Style="direction: ltr;"
                                onkeypress="javascript:return isNumberKey(event)" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="شماره حساب را وارد نمایید"
                                ControlToValidate="txtAccountNo" CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">گذرواژه :
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" MaxLength="10" TextMode="Password"
                                Style="float: right;" />
                            <img src="../App_Themes/Default/images/keyboard.png" onclick="javascript:drawKeyPad(event,this)"
                                class="button-keyboard" alt="نمایش کیبورد" title="نمایش کیبورد" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="گذرواژه حساب را وارد نمایید"
                                ControlToValidate="txtPassword" CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <img id="imgcaptcha" src="../Captcha.ashx?mode=2" alt="کد امنیتی" />
                            <input type="button" id="btnRecaptcha" onclick="javascript: loadCaptcha()" class="button-captcha"
                                title="کد امنیتی جدید" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="font-size: 11px;">کد امنیتی بالا را وارد نمایید
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnswer" runat="server" CssClass="textbox" MaxLength="8"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="کد امنیتی را وارد نمایید"
                                ControlToValidate="txtAnswer" CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnCheck" runat="server" CssClass="button" Text="تایید" OnClick="btnCheck_Click" />
                        </td>
                        <td>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnl">
                                <ProgressTemplate>
                                    <img src="../App_Themes/Default/images/indicator.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div class="button-box">
                <asp:Label ID="lblMessage" runat="server" CssClass="lbl-msg" EnableViewState="false" />
            </div>
            <div id="result">
                <table id="tblResult" runat="server" visible="false" class="account-info">
                    <tr>
                        <td class="fieldName">نوع حساب :
                        </td>
                        <td>
                            <asp:Label ID="lblAccountType" runat="server" EnableViewState="false" ForeColor="#d5665d" />
                        </td>
                        <td rowspan="4" valign="bottom">
                            <input id="btnPrint" class="button-print" onclick="javascript: showPrint()" title="جاپ"
                                type="button" value="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">دارنده حساب :
                        </td>
                        <td>
                            <asp:Label ID="lblAccountOwner" runat="server" EnableViewState="false" ForeColor="#d5665d" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">کل مبلغ موجودی (ریال) :
                        </td>
                        <td>
                            <asp:Label ID="lblBalance2" runat="server" EnableViewState="false" ForeColor="#d5665d" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="width: 120px;">مبلغ قابل برداشت (ریال) :
                        </td>
                        <td>
                            <asp:Label ID="lblBalance1" runat="server" EnableViewState="false" ForeColor="#d5665d" />
                        </td>
                    </tr>
                </table>
                <div style="margin: 10px 7px 5px 0px;">
                    <asp:ListView ID="lstTransactions" runat="server" ItemPlaceholderID="itemPlaceHolder">
                        <LayoutTemplate>
                            <table id="list-header">
                                <tr>
                                    <th style="width: 30px;"></th>
                                    <th style="width: 80px;">تاريخ
                                    </th>
                                    <th style="width: 90px;">صدور
                                    </th>
                                    <th style="width: 170px;">شرح
                                    </th>
                                    <th>عمليات
                                    </th>
                                    <th>مانده
                                    </th>
                                </tr>
                            </table>
                            <div id="itemPlaceHolder" runat="server">
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <table class="list-item-even">
                                <tr>
                                    <td style="width: 30px;">
                                        <%#Container.DataItemIndex + 1%>
                                    </td>
                                    <td style="width: 80px;">
                                        <%#Eval("B")%>
                                    </td>
                                    <td style="width: 90px;">
                                        <%#Eval("C")%>
                                    </td>
                                    <td style="width: 170px;">
                                        <%#Eval("D")%>
                                    </td>
                                    <td>
                                        <%#Eval("E").ToString().Split('.')[0]%>
                                    </td>
                                    <td>
                                        <%#Eval("F").ToString().Split('.')[0]%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table class="list-item-odd">
                                <tr>
                                    <td style="width: 30px;">
                                        <%#Container.DataItemIndex + 1%>
                                    </td>
                                    <td style="width: 80px;">
                                        <%#Eval("B")%>
                                    </td>
                                    <td style="width: 90px;">
                                        <%#Eval("C")%>
                                    </td>
                                    <td style="width: 170px;">
                                        <%#Eval("D")%>
                                    </td>
                                    <td>
                                        <%#Eval("E").ToString().Split('.')[0]%>
                                    </td>
                                    <td>
                                        <%#Eval("F").ToString().Split('.')[0]%>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <h1>آیتمی یافت نشد</h1>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="keyPad-dialog">
    </div>
    <script src="../Scripts/javascript.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.timer.js" type="text/javascript"></script>
    <script type="text/javascript">

        var reqIndex = 0;

        $(window).unload(function () {
            $('#imgcaptcha').attr('src', '../Captcha.ashx?cls=2');
        });

        function loadCaptcha() {
            $get('<%=txtAnswer.ClientID %>').value = '';
            $('#imgcaptcha').attr('src', '../Captcha.ashx?mode=2&ci=' + reqIndex++);
        }

        var pageTimer = new (function () {
            var $countdown,
        $form,
        incrementTime = 70,
        currentTime = 30000,
        updateTimer = function () {
            $countdown.html(formatTime(currentTime));
            if (currentTime == 0) {
                pageTimer.Timer.stop();
                timerComplete();
                return;
            }
            currentTime -= incrementTime / 10;
            if (currentTime < 0) currentTime = 0;
        },
        timerComplete = function () {
            window.location = '../Default.aspx';
        },
        init = function () {
            $countdown = $('#countdown');
            pageTimer.Timer = $.timer(updateTimer, incrementTime, true);
            $form = $('#pageTimerform');
            $form.bind('submit', function () {
                return false;
            });
        };
            $(init);
        });

        $('body').click(function () {
            if ($('#keyPad-dialog').css('display') == 'block') {
                hideKeyPad();
            }
        });

        function drawKeyPad(event, btnPad) {
            event.cancelBubble = true;
            var sArray = new Array();
            var storeList = new Array();

            for (var i = 1; i <= 10; i++) {
                sArray.push(i);
            }

            for (var k = 1; k <= 10; k++) {
                var randomPos = Math.floor(Math.random() * sArray.length);
                var valueFromArray = sArray.splice(randomPos, 1);
                var numberRand = parseInt(valueFromArray);
                storeList.push(numberRand - 1);
            }

            var keyPad = '<table id="keyPad"><tr><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[0] + '</td><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[1] + '</td><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[2] + '</td></tr><tr><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[3] + '</td><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[4] + '</td><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[5] + '</td></tr><tr><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[6] + '</td><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[7] + '</td><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[8] + '</td></tr><tr><td id="close" onclick="javascript:hideKeyPad()" title="بستن">×</td><td id="modify" onclick="javascript:modify(event)" title="تصحیح">Back</td><td class="num" onclick="javascript:hitNum(event,this)">' + storeList[9] + '</td></tr></table>';
            $('#keyPad-dialog').html(keyPad);
            var aryPosition = objectPosition(btnPad);
            $('#keyPad-dialog').css({ 'left': aryPosition[0] - 82, 'top': aryPosition[1] });
            $('#keyPad-dialog').show(300);
        }

        function modify(event) {
            event.cancelBubble = true;
            var txtPass = $get('<%=txtPassword.ClientID %>');
            if (txtPass.value != '') {
                txtPass.value = txtPass.value.slice(0, txtPass.value.length - 1);
            }
        }

        function hitNum(event, keyItem) {
            event.cancelBubble = true;
            var txtPass = $get('<%=txtPassword.ClientID %>');
            if (txtPass.value.length < 10) {
                txtPass.value += keyItem.innerHTML;
            }
        }

        function hideKeyPad() {
            $('#keyPad-dialog').hide(300);
        }

        function showPrint() {
            var url = 'Print.htm?mode=1&f=' + $get('<%=txtDateFrom.ClientID %>').value + '&t=' + $get('<%=txtDateTo.ClientID %>').value;
            window.open(url, 'prnt', 'location=0,status=1,scrollbars=1,resizable=no,width=650,height=600,menubar=no,toolbar=no')
        }

    </script>
</asp:Content>
