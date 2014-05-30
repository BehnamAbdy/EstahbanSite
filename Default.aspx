<%@ Page Title="صندوق قرض الحسنه مهدیه استهبان" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.cs" Inherits="Default" %>

<%@ Register Src="UC/News.ascx" TagName="News" TagPrefix="uc2" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cph">
    <script type="text/javascript" src="Scripts/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="./Scripts/angular.min.js"></script>
    <script type="text/javascript" src="Scripts/calendar.js"></script>
    <link href="Styles/nivo-slider.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery.nivo.slider.js"></script>
    <script type="text/javascript" src="Scripts/jquery.ticker.js"></script>
    <link href="Styles/ticker-style.css" rel="stylesheet" type="text/css" />
    <link href="Styles/prayer-time.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        $(document).ready(function () {
            $('#slider').nivoSlider();
            $('#georgian').html(convertDate('<%=cuttentDate[0]%>', '<%=cuttentDate[1]%>', '<%=cuttentDate[2]%>', 'solar'));
        });

        setInterval(function () {
            $('#letters li:first').slideUp(function () {
                $(this).appendTo($('#letters')).slideDown();
            }, 100);
        }, 3000);

        var module = angular.module('siteApp', []);
        module.controller('myCtrl', ['$scope', '$http', function ($scope, $http) {

            $http.get('./Admin/TickerItems.aspx?mode=0')
                .success(function (model) {
                    $scope.tickers = model;
                    setTimeout(function () {
                        $('#ticker-bar').ticker();
                    }, 0);
                })
                .error(function () {

                });
        }]);

    </script>
    <div ng-controller="myCtrl">
        <table cellpadding="0" cellspacing="0" style="margin: 10px 0 0; width: 100%;">
            <tr>
                <td style="width: 220px;" valign="top">
                    <div id="current-date">
                        <div id="solar">
                            <%=Persia.Calendar.ConvertToPersian(DateTime.Now).Weekday %>
                        </div>
                        <hr style="margin: 4px auto 6px auto;" />
                        <div id="moon">
                            <%=Persia.Calendar.ConvertToIslamic(DateTime.Now).Formal %>
                        </div>
                        <hr style="margin: 4px auto 6px auto;" />
                        <div id="georgian"></div>
                    </div>
                    <div id="time" runat="server"></div>
                </td>
                <td>
                    <div id="slider-wrapper">
                        <div id="slider" class="nivoSlider">
                            <a href="Info.aspx?mode=10">
                                <img src="App_Themes/Default/images/slider/1.jpg" /></a>
                            <a href="Info.aspx?mode=11">
                                <img src="App_Themes/Default/images/slider/2.jpg" /></a>
                            <a href="Info.aspx?mode=12">
                                <img src="App_Themes/Default/images/slider/3.jpg" /></a>
                            <a href="Info.aspx?mode=13">
                                <img src="App_Themes/Default/images/slider/4.jpg" /></a>
                            <a href="Info.aspx?mode=14">
                                <img src="App_Themes/Default/images/slider/5.jpg" /></a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div class="section-divider"></div>

        <ul id="ticker-bar">
            <li ng-repeat="tick in tickers">{{tick.title}}</li>
        </ul>
        <div id="middle-bar"></div>
        <table id="panel">
            <tr>
                <td align="center">
                    <a href="Info.aspx?mode=1">
                        <img src="App_Themes/Default/images/internet-bank.gif" /></a>
                </td>
                <td align="center">
                    <a href="Info.aspx?mode=3">
                        <img src="App_Themes/Default/images/sms-bank.gif" /></a>
                </td>
                <td align="center">
                    <a href="Info.aspx?mode=2">
                        <img src="App_Themes/Default/images/phone-bank.gif" /></a>
                </td>
            </tr>
        </table>

        <table style="width: 630px; margin: 0;">
            <tr>
                <td></td>
                <td>
                    <div class="navbar">
                        <div class="navbar-right"></div>
                        <div class="navbar-left"></div>
                        <div class="navbar-center"></div>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="bank" valign="top">
                    <br />
                    <div id="marriage-loan">
                        <a href="Info.aspx?mode=4">
                            <img src="App_Themes/Default/images/home/marriage-loan.jpg" style="position: absolute; top: 2px; right: 10px;" />
                        </a>
                    </div>
                    <div class="shetab">
                        <img src="App_Themes/Default/images/home/chart.png" style="position: absolute; top: 5px; right: 12px;" />
                        <a href="Info.aspx?mode=15">
                            <img src="App_Themes/Default/images/home/statistics.png" style="position: absolute; top: 9px; left: 9px;" />
                        </a>
                    </div>
                    <div class="shetab">
                        <img src="App_Themes/Default/images/home/bmi.png" style="position: absolute; top: 10px; right: 13px;" />
                        <a href="BMI/TransCash.aspx">
                            <img src="App_Themes/Default/images/home/transfer-shetab.png" style="position: absolute; top: 6px; left: 35px;" />
                        </a>
                    </div>
                    <div class="shetab">
                        <img src="App_Themes/Default/images/home/bmi.png" style="position: absolute; top: 10px; right: 13px;" />
                        <a href="BMI/Loan.aspx">
                            <img src="App_Themes/Default/images/home/pay-installment.png" style="position: absolute; top: 6px; left: 55px;" />
                        </a>
                    </div>
                </td>
                <td style="background-color: #fefefc; border: solid 1px #e2e2e2; border-radius: 4px; width: 330px;">
                    <uc2:News ID="news" runat="server" />
                </td>
            </tr>
        </table>

        <div class="section-divider"></div>
        <table id="utilities">
            <tr>
                <td>
                    <a href="Info.aspx?mode=5">
                        <img src="App_Themes/Default/images/home/history.png" />
                        <p>تاريخچه</p>
                    </a>

                </td>
                <td>
                    <a href="Info.aspx?mode=6">
                        <img src="App_Themes/Default/images/home/book.png" />
                        <p>اساسنامه</p>
                    </a>
                </td>
                <td>
                    <a href="Info.aspx?mode=7">
                        <img src="App_Themes/Default/images/home/network.png" />
                        <p>ساختار سازماني</p>
                    </a>
                </td>

                <td>
                    <a href="Info.aspx?mode=8">
                        <img src="App_Themes/Default/images/home/partnership.png" />
                        <p>منشور اخلاقي</p>
                    </a>
                </td>
                <td>
                    <a>
                        <img src="App_Themes/Default/images/home/mail.png" />
                        <p>تماس مستقیم با مدير عامل</p>
                    </a>
                </td>
                <td>
                    <a href="Info.aspx?mode=9">
                        <img src="App_Themes/Default/images/home/session.jpg" />
                        <p>هيات مديره و مدير عامل</p>
                    </a>
                </td>
            </tr>
        </table>
    </div>
    <%--<p id="slogan">
    </p>--%>
    <%--<uc2:SiteLinks runat="server" />--%>



    <%--<div class="navbar" style="margin-right: 60px;">
        <div class="navbar-left"></div>
        <div class="navbar-center">
        </div>
        <div class="navbar-right"></div>
    </div>
    <div class="navbar-box">
    </div>--%>
</asp:Content>


