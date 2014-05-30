<%@ Page Title="ویرایش لیست" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="EditList.aspx.cs" Inherits="Admin_EditList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        ویرایش لیست
    </div>
    <table style="margin: 15px 5% 0 0;">
        <tr>
            <td valign="middle">
                <select id="drpItems" name="drpItems" class="dropdown" style="width: 200px;" onchange="javascript:setMode()">
                </select>
                <img src="../App_Themes/Default/images/delete.png" style="cursor: pointer; margin-right: 5px;"
                    onclick="javascript:deleteItem()" title="حذف" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px;">
            </td>
        </tr>
        <tr>
            <td class="fieldName">
                عنوان :
            </td>
        </tr>
        <tr>
            <td>
                <input type="text" id="txtTitle" name="txtTitle" class="textbox" style="width: 196px;" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px;">
            </td>
        </tr>
        <tr>
            <td valign="middle">
                متن
            </td>
        </tr>
        <tr>
            <td>
                <textarea id="txtEditor" name="txtEditor" cols="60" rows="10" style="height: 400px;
                    width: 600px;"></textarea>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnSave" onclick="javascript:commit()" class="button" value="ثبت" />
                &nbsp;
                <label class="lbl-msg">
                </label>
            </td>
        </tr>
    </table>
    <script src="../Scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.cleditor.min.js" type="text/javascript"></script>
    <link href="../Styles/jquery.cleditor.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {
            $('#txtEditor').cleditor();
            loadTests();
        });

        function loadTests() {
            var drp = document.getElementById('drpItems');
            for (var i = drp.options.length - 1; i >= 0; i--) {
                drp.remove(i);
            }

            $.ajax({
                type: 'GET',
                data: ({ mode: 1 }),
                url: '../Admin/EditList.aspx',
                dataType: 'json',
                cache: false,
                success: function (resultdata) {
                    drp.options[0] = new Option('-- جدید --', '0');
                    for (index in resultdata) {
                        drp.options[drp.options.length] = new Option(resultdata[index].Title, resultdata[index].Id);
                    }
                }
            });
        }

        function setMode() {
            $('.lbl-msg').text('');
            var drp = document.getElementById('drpItems');
            if (drp.selectedIndex > 0) {
                $.ajax({
                    type: 'GET',
                    data: ({ mode: 2, id: drp.value }),
                    url: '../Admin/EditList.aspx',
                    dataType: 'json',
                    cache: false,
                    success: function (result) {
                        for (index in result) {
                            document.getElementById('txtTitle').value = result[index].Title;
                            document.getElementById('txtEditor').value = result[index].Description;
                            $('#txtEditor').cleditor()[0].updateFrame();
                            document.getElementById('btnSave').value = 'ویرایش';
                        }
                    }
                });
            }
            else {
                document.getElementById('txtTitle').value = '';
                document.getElementById('txtEditor').value = '';
                document.getElementById('btnSave').value = 'ثبت';
            }
        }

        function commit() {
            var title = document.getElementById('txtTitle').value;
            if (title == '') {
                $('.lbl-msg').text('عنوان آیتم نوشته نشده');
                return;
            }

            var body = document.getElementById('txtEditor').value;
            if (body == '') {
                $('.lbl-msg').text('متن آیتم نوشته نشده');
                return;
            }

            var id = document.getElementById('drpItems').value;
            $.ajax({
                type: 'POST',
                data: ({ id: id, tle: title, bdy: body }),
                url: '../Admin/EditList.aspx',
                dataType: 'json',
                cache: false,
                success: function (result) {
                    if (result == '1') {
                        document.getElementById('txtTitle').value = '';
                        document.getElementById('txtEditor').value = '';
                        document.getElementById('btnSave').value = 'ثبت';
                        loadTests();
                        $('.lbl-msg').text(id == '0' ? 'ثبت آیتم جدید انجام گردید' : 'ویرابش آیتم انجام گردید');
                    }
                }
            });
        }

        function deleteItem() {
            var drp = document.getElementById('drpItems');
            if (drp.selectedIndex > 0) {
                if (confirm('آیا آیتم مورد نظر حذف گردد؟')) {
                    $.ajax({
                        type: 'GET',
                        data: ({ mode: 3, id: drp.value }),
                        url: '../Admin/EditList.aspx',
                        dataType: 'json',
                        cache: false,
                        success: function (result) {
                            if (result == '1') {
                                drp.remove(drp.selectedIndex);
                                document.getElementById('txtTitle').value = '';
                                document.getElementById('txtEditor').value = '';
                                document.getElementById('btnSave').value = 'ثبت';
                            }
                        }
                    });
                }
            }
        }
    </script>
</asp:Content>
