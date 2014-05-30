<%@ Page Title="درخواست وام ازدواج" Language="C#" MasterPageFile="~/Site.master" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        درخواست وام ازدواج
    </div>
    <br />
    <br />
    <br />
    <table class="account-info" style="width: 350px;">
        <tr>
            <th colspan="2">درخواست وام ازدواج شما ثبت گردید
            </th>
        </tr>
        <tr>
            <td>کد رهگیری :
            </td>
            <td>
                <label id="follow-code"></label>
            </td>
        </tr>
        <tr>
            <td>تاریخ مراجعه شما به صندوق :
            </td>
            <td>
                <label id="refDate"></label>
            </td>
        </tr>
    </table>
    <div class="alarm">
        خطا در ثبت درخواست وام ازدواج !
    </div>

    <script src="../Scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/javascript.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            if (getQueryString(window.location.search, 'code') != null && getQueryString(window.location.search, 'date') != null) {
                $('.alarm').hide();
                $('#follow-code').text(getQueryString(window.location.search, 'code'));
                var date = getQueryString(window.location.search, 'date');
                $('#refDate').text(date.substring(0, 4) + '/' + date.substring(4, 6) + '/' + date.substring(6, 8));
            } else {
                $('.account-info').hide();
            }
        });

    </script>
</asp:Content>

