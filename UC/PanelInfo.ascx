<%@ Control Language="C#" %>
<%@ OutputCache Duration="300" VaryByParam="none" %>
<div id="wellcome">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div class="title">
                </div>
                <div class="desc">
                </div>
            </td>
            <td>
                <img src="./App_Themes/Default/images/flag.jpg">
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                </ol>
            </td>
            <td>
                <img src="./App_Themes/Default/images/chart.jpg">
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">

    $.ajax({
        type: 'POST',
        url: './Utility.asmx/WellcomePanel',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            var panel = eval(response.d);
            for (var i in panel) {
                $('.title').text(panel[i].Title);
                $('.desc').text(panel[i].Description);
                for (var j in panel[i].List) {
                    $('<li>' + panel[i].List[j].Item + '</li>').appendTo('#wellcome ol');
                }
            }
        }
    });

</script>
