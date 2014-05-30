function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}
function isDateKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode > 31 && (charCode < 47 || charCode > 57)))
        return false;
    return true;
}
function objectPosition(obj) {
    var curleft = 0; var curtop = 0;
    if (obj.offsetParent) {
        do { curleft += obj.offsetLeft; curtop += obj.offsetTop; }
        while (obj = obj.offsetParent);
    } return [curleft, curtop];
}
function getQueryString(param, key) {
    var values = new Array(), keys = new Array();
    param = param.substring(1, param.length)
    param = param.split('&');
    for (i = 0; i < param.length; i++) {
        keys[i] = param[i].split('=')[0];
        values[i] = param[i].split('=')[1];
    }
    for (i = 0; i < keys.length; i++) {
        if (keys[i] == key)
            return values[i];
    }
    return null;
}
function nationalCodeValidate(source, arguments) {
    if (arguments.Value.length == 10) {
        arguments.IsValid = true;
    }
    else {
        arguments.IsValid = false;
    }
}