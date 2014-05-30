﻿<%@ Page Title="آخرین وضعیت اقساط وام" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="LoanRecords.aspx.cs" Inherits="Account_LoanRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <div class="page-title">
                    آخرین وضعیت اقساط وام
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
        04:00:00</div>
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnCheck">
                <table style="margin: 10px 70px 0 0;">
                    <tr>
                        <td class="fieldName">
                            شماره حساب (وام) :
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
                        <td class="fieldName">
                            گذرواژه :
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
                        <td>
                        </td>
                        <td>
                            <img id="imgcaptcha" src="../Captcha.ashx?mode=5" alt="کد امنیتی" />
                            <input type="button" id="btnRecaptcha" onclick="javascript:loadCaptcha()" class="button-captcha"
                                title="کد امنیتی جدید" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="font-size: 11px;">
                            کد امنیتی بالا را وارد نمایید
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
                        <td>
                        </td>
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
                        <td class="fieldName">
                            گیرنده وام :
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" EnableViewState="false" ForeColor="#d5665d" />
                        </td>
                        <td rowspan="3" valign="bottom">
                            <input id="btnPrint" class="button-print" onclick="javascript:showPrint()" title="جاپ"
                                type="button" value="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">
                            تاریخ وام :
                        </td>
                        <td>
                            <asp:Label ID="lblDate" runat="server" EnableViewState="false" ForeColor="#d5665d" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">
                            مبلغ وام :
                        </td>
                        <td>
                            <asp:Label ID="lblAmount" runat="server" EnableViewState="false" ForeColor="#d5665d" />
                        </td>
                    </tr>
                </table>
                <br />
                <div style="margin: 0 7px 5px 0px;">
                    <asp:ListView ID="lstTransactions" runat="server" ItemPlaceholderID="itemPlaceHolder">
                        <LayoutTemplate>
                            <table id="list-header">
                                <tr>
                                    <th style="width: 30px;">
                                    </th>
                                    <th style="width: 80px;">
                                        شماره سند
                                    </th>
                                    <th style="width: 80px;">
                                        تاريخ سررسید
                                    </th>
                                    <th>
                                        مبلغ قسط
                                    </th>
                                    <th style="width: 70px;">
                                        دیرکرد (روز)
                                    </th>
                                    <th style="width: 80px;">
                                        تاريخ پرداخت
                                    </th>
                                    <th style="width: 150px;">
                                        شرح
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
                                        <%#Eval("Serial")%>
                                    </td>
                                    <td style="width: 80px;">
                                        <%#Eval("DueDate")%>
                                    </td>
                                    <td>
                                        <%#Eval("InsuAmount")%>
                                    </td>
                                    <td style="width: 70px;">
                                        <%#Eval("DelayPay")%>
                                    </td>
                                    <td style="width: 80px;">
                                        <%#Public.ToSeparatedPersianDate(Eval("DocsDate"))%>
                                    </td>
                                    <td style="width: 150px;">
                                        <%#Eval("DocsDesc")%>
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
                                        <%#Eval("Serial")%>
                                    </td>
                                    <td style="width: 80px;">
                                        <%#Eval("DueDate")%>
                                    </td>
                                    <td>
                                        <%#Eval("InsuAmount")%>
                                    </td>
                                    <td style="width: 70px;">
                                        <%#Eval("DelayPay")%>
                                    </td>
                                    <td style="width: 80px;">
                                        <%#Public.ToSeparatedPersianDate(Eval("DocsDate"))%>
                                    </td>
                                    <td style="width: 150px;">
                                        <%#Eval("DocsDesc")%>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <h1>
                                آیتمی یافت نشد</h1>
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
            $('#imgcaptcha').attr('src', '../Captcha.ashx?cls=5');
        });

        function loadCaptcha() {
            $get('<%=txtAnswer.ClientID %>').value = '';
            $('#imgcaptcha').attr('src', '../Captcha.ashx?mode=5&ci=' + reqIndex++);
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
            var url = 'Print.htm?mode=2';
            window.open(url, 'prnt', 'location=0,status=1,scrollbars=1,resizable=no,width=650,height=600,menubar=no,toolbar=no')
        }

    </script>
</asp:Content>
