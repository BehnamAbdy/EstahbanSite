<%@ Page Title="ویرایش متن بانکداری" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" ValidateRequest="false" CodeFile="EditText.aspx.cs" Inherits="Admin_EditText" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
    </div>
    <table style="margin: 25px 5% 0 0;">
        <tr>
            <td>
                <textarea id="txtEditor" name="txtEditor" cols="60" rows="10" style="height: 400px; width: 600px;"></textarea>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnSave" onclick="javascript: commit()" class="button" value="ثبت" />
                &nbsp;
                <label class="lbl-msg">
                </label>
            </td>
        </tr>
    </table>
    <script src="../Scripts/javascript.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.cleditor.min.js" type="text/javascript"></script>
    <link href="../Styles/jquery.cleditor.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {
            switch (getQueryString(window.location.search, 'm')) {
                case '1': // Internet Bank
                    $('.page-title').text('ویرایش متن بانکداری اینترنتی');
                    break;

                case '2': // Telephone Bank
                    $('.page-title').text('ویرایش متن سامانه تلفن بانک');
                    break;

                case '3': // Sms Bank
                    $('.page-title').text('ویرایش متن سامانه پیام کوتاه');
                    break;

                case '4': // Marriage Loan
                    $('.page-title').text('ویرایش متن شرایط وام ازدواج');
                    break;

                case '5':
                    $('.page-title').text('ویرایش متن تاريخچه');
                    break;

                case '6':
                    $('.page-title').text('ویرایش متن اساسنامه');
                    break;

                case '7':
                    $('.page-title').text('ویرایش متن ساختار سازماني');
                    break;

                case '8':
                    $('.page-title').text('ویرایش متن منشور اخلاقي');
                    break;

                case '9':
                    $('.page-title').text('ویرایش متن هیات مدیره و مدیرعامل');
                    break;

                case '15':
                    $('.page-title').text('ویرایش متن آمار و عملکرد صندوق');
                    break;
            }

            $('#txtEditor').cleditor();
            $.ajax({
                type: 'POST',
                data: '{ mode:"' + getQueryString(window.location.search, 'm') + '"}',
                url: '../Utility.asmx/BankingInfo',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    var txt = eval(response.d);
                    document.getElementById('txtEditor').value = txt[0].Text;
                    $('#txtEditor').cleditor()[0].updateFrame();
                }
            });
        });

        function commit() {
            var txt = document.getElementById('txtEditor').value;
            if (txt == '') {
                $('.lbl-msg').text('متن آیتم نوشته نشده');
                return;
            }

            $.ajax({
                type: 'POST',
                data: ({ mode: getQueryString(window.location.search, 'm'), txt: txt }),
                url: '../Admin/EditText.aspx',
                dataType: 'json',
                cache: false,
                success: function (result) {
                    if (result == '1') {
                        document.getElementById('btnSave').value = 'ثبت';
                        $('.lbl-msg').text('ویرابش آیتم انجام گردید');
                    }
                }
            });
        }

    </script>
</asp:Content>
