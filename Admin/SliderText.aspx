<%@ Page Title="ویرایش متن گالری عکس ها" Language="C#" MasterPageFile="~/Site.master" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        ویرایش متن گالری عکس ها
    </div>

    <table style="margin: 0 auto;">
        <tr>
            <td>
                <img src="../App_Themes/Default/images/slider/1.jpg" style="cursor: pointer; height: 63px; width: 110px;" onclick="load(10)" />

            </td>
            <td>
                <img src="../App_Themes/Default/images/slider/2.jpg" style="cursor: pointer; height: 63px; width: 110px;" onclick="load(11)" />

            </td>
            <td>
                <img src="../App_Themes/Default/images/slider/3.jpg" style="cursor: pointer; height: 63px; width: 110px;" onclick="load(12)" />

            </td>
            <td>
                <img src="../App_Themes/Default/images/slider/4.jpg" style="cursor: pointer; height: 63px; width: 110px;" onclick="load(13)" />

            </td>
            <td>
                <img src="../App_Themes/Default/images/slider/5.jpg" style="cursor: pointer; height: 63px; width: 110px;" onclick="load(14)" />

            </td>
        </tr>
    </table>
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

        $('#txtEditor').cleditor();
        var selectedItem = null;

        function load(item) {
            $('.lbl-msg').empty();
            selectedItem = item;
            $.ajax({
                type: 'POST',
                data: '{ mode:"' + item + '"}',
                url: '../Utility.asmx/BankingInfo',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    var txt = eval(response.d);
                    document.getElementById('txtEditor').value = txt[0].Text;
                    $('#txtEditor').cleditor()[0].updateFrame();
                }
            });
        }

        function commit() {
            if (selectedItem) {
                var txt = document.getElementById('txtEditor').value;
                if (txt == '') {
                    $('.lbl-msg').text('متن آیتم نوشته نشده');
                    return;
                }

                $.ajax({
                    type: 'POST',
                    data: ({ mode: selectedItem, txt: txt }),
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
        }

    </script>
</asp:Content>
