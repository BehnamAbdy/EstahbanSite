<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
    </div>
    <div id="text">
    </div>
    <div id="loan" style="text-align: left;">
        <hr />
        <input type="button" class="button" onclick="javascript: window.location = './Public/MarriageLoan.aspx'" value="ادامه" style="margin-left: 50px;" />
    </div>
    <script src="Scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="Scripts/javascript.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#loan').hide();
            if (getQueryString(window.location.search, 'id') != null && getQueryString(window.location.search, 'mode') == null) {
                $.ajax({
                    type: 'POST',
                    data: '{ id:' + getQueryString(window.location.search, 'id') + '}',
                    url: './Utility.asmx/LoanInfo',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        var loan = eval(response.d);
                        $('.page-title').text(loan[0].Title);
                        $('#text').html(loan[0].Description);
                    }
                });
            }
            else if (getQueryString(window.location.search, 'id') == null && getQueryString(window.location.search, 'mode') != null) {
                $.ajax({
                    type: 'POST',
                    data: '{ mode:"' + getQueryString(window.location.search, 'mode') + '"}',
                    url: './Utility.asmx/BankingInfo',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        var banking = eval(response.d);
                        $('#text').html(banking[0].Text);
                        switch (getQueryString(window.location.search, 'mode')) {
                            case '1': // Internet Bank
                                $('.page-title').text('آشنایی با سامانه اینترنت بانک');
                                break;

                            case '2': // Telephone Bank
                                $('.page-title').text('آشنایی با سامانه تلفن بانک');
                                break;

                            case '3': // Sms Bank
                                $('.page-title').text('آشنایی با سامانه اس ام اس بانك');
                                break;

                            case '4': // Marriage Loan
                                $('.page-title').text('آشنایی با شرایط وام ازدواج');
                                $('#loan').show();
                                break;

                            case '5':
                                $('.page-title').text('آشنایی با تاريخچه صندوق');
                                break;

                            case '6':
                                $('.page-title').text('آشنایی با اساسنامه صندوق');
                                break;

                            case '7':
                                $('.page-title').text('آشنایی با ساختار سازماني');
                                break;

                            case '8':
                                $('.page-title').text('آشنایی با منشور اخلاقي ما');
                                break;

                            case '8':
                                $('.page-title').text('آشنایی با منشور اخلاقي ما');
                                break;

                            case '15':
                                $('.page-title').text('آمار و عملکرد صندوق قرض الحسنه مهدیه استهبان');
                                break;

                            default:
                                $('.page-title').text('صندوق قرض الحسنه مهدیه استهبان');
                                break;
                        }
                    }
                });
            } else if (getQueryString(window.location.search, 'news') != null) {
                $.ajax({
                    type: 'POST',
                    data: '{ id:' + getQueryString(window.location.search, 'news') + '}',
                    url: './Utility.asmx/News',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        var loan = eval(response.d);
                        $('.page-title').text(loan[0].Title);
                        $('#text').html(loan[0].Text);
                    }
                });
            }
        });

    </script>
</asp:Content>
