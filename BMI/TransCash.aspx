<%@ Page Title="انتقال وجه" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TransCash.cs" Inherits="BMI_TransCash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <div class="page-title">
                    انتقال وجه
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
                <table style="margin: 5px 70px 0 0;">
                    <tr>
                        <td class="fieldName">شماره حساب واریزی :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccountNo" runat="server" CssClass="textbox" MaxLength="15"
                                Style="direction: ltr;" onkeypress="javascript:return isNumberKey(event)" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="شماره حساب را وارد نمایید"
                                ControlToValidate="txtAccountNo" CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <img id="imgcaptcha" src="../Captcha.ashx?mode=8" alt="کد امنیتی" />
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
            <asp:Panel ID="pnlConfirm" runat="server" Visible="False" DefaultButton="btnConfirm">
                <table class="account-info" style="margin-right: 70px; width: 340px;">
                    <tr>
                        <td colspan="3" align="center">
                            <label style="color: #0072c6; font: bold 15px Arial;">
                                حساب واریزی</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">دارنده حساب :
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblAccountOwner" runat="server" ForeColor="#d5665d" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">نوع حساب :
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblAccountType" runat="server" ForeColor="#d5665d" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName">مبلغ واریزی (ریال) :
                        </td>
                        <td colspan="2">
                            <input type="number" id="txtAmount" name="txtAmount" class="textbox" maxlength="15" style="direction: ltr;" /><label class="star">*</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnConfirm" runat="server" CssClass="button" Text="پرداخت"
                                OnClick="btnConfirm_Click" OnClientClick="javascript:return prePay();" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div class="button-box">
                <asp:Label ID="lblMessage" runat="server" CssClass="lbl-msg" EnableViewState="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <input type="hidden" id="hdnAmount" name="hdnAmount" />

    <script src="../Scripts/javascript.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.timer.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.maskMoney.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#txtAmount').maskMoney({ precision: 0 });
        });

        function prePay() {
            if ($('#txtAmount').val() == '' || $('#txtAmount').val() == '0') {
                $('.star').text('مبلغ را وارد نمایید');
                return false;
            }
            var splitted = $('#txtAmount').val().split(',');
            var amount = '';
            for (var i = 0; i < splitted.length; i++) {
                amount += splitted[i];
            }
            $('#hdnAmount').val(amount.trim());
        }

        var reqIndex = 0;

        $(window).unload(function () {
            $('#imgcaptcha').attr('src', '../Captcha.ashx?cls=8');
        });

        function loadCaptcha() {
            $get('<%=txtAnswer.ClientID %>').value = '';
            $('#imgcaptcha').attr('src', '../Captcha.ashx?mode=8&ci=' + reqIndex++);
        }

        var pageTimer = new (function () {
            var $countdown,
        $form,
        incrementTime = 70,
        currentTime = 21000,
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

        function postRefId(refIdValue) {
            var form = document.createElement("form");
            form.setAttribute("method", "POST");
            form.setAttribute("action", "<%= PgwSite %>");
            form.setAttribute("target", "_self");
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "RefId");
            hiddenField.setAttribute("value", refIdValue);
            form.appendChild(hiddenField);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }

    </script>
</asp:Content>
