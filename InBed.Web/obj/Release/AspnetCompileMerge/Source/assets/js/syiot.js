
function timeStamp(second_time) {
    var time = parseInt(second_time) + "秒";
    if (parseInt(second_time) > 60) {
        var second = parseInt(second_time) % 60;
        var min = parseInt(second_time / 60);
        time = min + "分" + second + "秒";
        if (min > 60) {
            min = parseInt(second_time / 60) % 60;
            var hour = parseInt(parseInt(second_time / 60) / 60);
            time = hour + "小时" + min + "分" + second + "秒";
            if (hour > 24) {
                hour = parseInt(parseInt(second_time / 60) / 60) % 24;
                var day = parseInt(parseInt(parseInt(second_time / 60) / 60) / 24);
                time = day + "天" + hour + "小时" + min + "分" + second + "秒";
            }
        }
    }
    return time;
}

function timeStampMin(min_time) {

    return (Math.floor(min_time / 60) + "小时" + (min_time % 60) + "分");
}

function timeStampSec(sec_time) {

    return (Math.floor(sec_time / 60 / 60) + "小时" + Math.floor(sec_time / 60 % 60) + "分");
}

function dateStamp(time) {
    var str = time;
    var arr = str.split(' ');
    var lastStr = arr[arr.length - 1];
    var lastIndex = str.lastIndexOf(' ');
    var lastStr = str.substring(lastIndex + 1);
    return lastStr;
}

function dateStart(time) {
    var str = time;
    var arr = str.split(' ');
    var firstStr = arr[0];
    return firstStr;
}

function dateEnd(time) {
    var str = time;
    var arr = str.split(' ');
    var lastStr = arr[arr.length - 1];
    var lastIndex = str.lastIndexOf(' ');
    var lastStr = str.substring(lastIndex + 1);
    return lastStr;
}

function toJSDate(jsondate) {
    var date = new Date(parseInt(jsondate.replace("/Date(", "").replace(")/", ""), 10));
    return date;
}

function getDateTime(date) {
    var hh = date.getHours();
    var mm = date.getMinutes();
    var ss = date.getSeconds();
    return hh + ":" + mm + ":" + ss;
}

function toHourMinute(time) {
    var str = time;
    var arr = str.split(':');
    var hour = arr[0];
    hour = hour * 60 * 60;
    var min = arr[1];
    min = min * 60;
    var sec = arr[2];
    sec = sec * 1;
    return hour + min + sec;
}

function timeTamp(second_time) {
    var time = parseInt(second_time);
    if (parseInt(second_time) > 60) {
        var second = parseInt(second_time) % 60;
        if (second < 10) {
            second = "0" + second;
        }
        var min = parseInt(second_time / 60);
        time = min + ":" + second;
        if (min > 60) {
            min = parseInt(second_time / 60) % 60;
            if (min < 10) {
                min = "0" + min;
            }
            var hour = parseInt(parseInt(second_time / 60) / 60);
            time = hour + ":" + min + ":" + second;
        }
    }
    return time;
}

function GetDateStr(AddDayCount,value) {
    var dd = new Date(value);
    dd.setDate(dd.getDate() + AddDayCount);
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1;
    var d = dd.getDate();
    return y + "/" + m + "/" + d;
}


Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

Array.prototype.unique = function () {
    var res = [];
    var json = {};
    for (var i = 0; i < this.length; i++) {
        if (!json[this[i]]) {
            res.push(this[i]);
            json[this[i]] = 1;
        }
    }
    return res;
}

 /***********告警框消息************/
function chatformat_msg(msg, type, name, time) {
    var d = new Date();
    var h = d.getHours();
    var m = d.getMinutes();
    return "<div class='chatmsg msg_" + type + "'><span class='name'>" + name + "</span><span class='text'>" + msg + "</span><span class='ts'>" + time + "</span></div>";
}