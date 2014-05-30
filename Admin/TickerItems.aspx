<%@ Page Title="ویرایش متن متحرک" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TickerItems.aspx.cs" Inherits="Admin_TickerItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        ویرایش متن متحرک
    </div>

    <div ng-controller="myCtrl" style="margin-right: 40px;">
        <table>
            <tr>
                <td>
                    <input type="text" class="textbox" maxlength="65" style="width: 350px;" ng-model="model.title" />
                </td>
                <td>
                    <input type="button" class="button" ng-click="save()" value="ثبت" />
                </td>
            </tr>
        </table>
        <ul id="ticker-items">
            <li ng-repeat="item in items">
                <span>{{item.title}}</span>
                <img src="../App_Themes/Default/images/edit.png" title="ویرایش" ng-click="edit(item)" />
                <img src="../App_Themes/Default/images/delete.png" title="حذف" ng-click="delete(item)" />

            </li>
        </ul>
    </div>
    <script type="text/javascript" src="../Scripts/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/angular.min.js"></script>

    <script type="text/javascript">
        var module = angular.module('siteApp', []);
        module.controller('myCtrl', ['$scope', '$http', function ($scope, $http) {
            $scope.model = { id: 0, title: '' };

            $http.get('../Admin/TickerItems.aspx?mode=0')
                .success(function (model) {
                    $scope.items = model;
                })
                .error(function () {

                });

            $scope.save = function () {
                if ($scope.model.title != '') {
                    $.ajax({
                        type: 'POST',
                        data: ({ mode: 1, id: $scope.model.id, title: $scope.model.title }),
                        url: '../Admin/TickerItems.aspx',
                        dataType: 'json',
                        cache: false,
                        success: function (result) {

                            $scope.$apply(function () {
                                if ($scope.model.id == 0) {
                                    $scope.items.push({ id: result, title: $scope.model.title });
                                }
                                $scope.model = { id: 0, title: '' };
                            });
                        }
                    });
                }
            };

            $scope.edit = function (model) {
                $scope.model = model;
            };

            $scope.delete = function (model) {
                if (confirm('آیتم مورد نظر حذف گردد؟')) {
                    $http.get('../Admin/TickerItems.aspx?mode=2&id=' + model.id)
                        .success(function () {
                            $scope.items.splice($scope.items.indexOf(model));
                            $scope.id = 0;
                            $scope.title = null;
                        })
                        .error(function () {

                        });
                }
            };

        }]);

    </script>

</asp:Content>

