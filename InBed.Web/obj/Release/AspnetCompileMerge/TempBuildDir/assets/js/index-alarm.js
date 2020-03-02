
function diff(arr1, arr2) {
    var newArr = [];
    var arr3 = [];
    for (var i = 0; i < arr1.length; i++) {
        if (arr2.indexOf(arr1[i]) === -1)
            arr3.push(arr1[i]);
    }
    var arr4 = [];
    for (var j = 0; j < arr2.length; j++) {
        if (arr1.indexOf(arr2[j]) === -1)
            arr4.push(arr2[j]);
    }
    newArr = arr3.concat(arr4);
    return newArr;
}

$(document).on('click', ".alarm-windows .user-window .controlbar .closeit,.alarm-windows .user-window .controlbar .closeit", function (e) {
    var id = $(this).attr("data-user-id");
    $(".alarm-windows #user-window" + id).hide();
    $(".page-chatapi .user-row#chat_user_" + id).removeClass("active");
});

$(document).on('click', ".alarm-windows .user-window .controlbar img, .alarm-windows .user-window .controlbar .minimizeit", function (e) {
    var id = $(this).attr("data-user-id");

    if (!$(".alarm-windows #user-window" + id).hasClass("minimizeit")) {
        $(".alarm-windows #user-window" + id).addClass("minimizeit");
        ULTRA_SETTINGS.tooltipsPopovers();
    } else {
        $(".alarm-windows #user-window" + id).removeClass("minimizeit");
    }
});

$(document).on('keypress', ".alarm-windows .user-window .typearea input", function (e) {
    if (e.keyCode == 13) {
        var _this = $(this);
        var id = _this.attr("data-user-id");
        var msg1 = _this.val();
        var msg = "處理結果："+msg1;
        //console.log(msg);
        msg = chatformat_msg(msg, 'sent', '操作人：caichuan', getDateTime(new Date()));
        $(".alarm-windows #user-window" + id + " .chatarea").append(msg);
        $.ajax({
            type: "post",
            url: "/Adm/Control/HandleAlarm",
            dataType: "json",
            data:{
                alarmID : id,
                mag : msg1
            },
            success: function (result) {
                _this.attr("disabled", "disabled");
                _this.val("處理成功！");
		        _this.css('color','#e4393c');
                $("#" + id + "").remove();
                var num = $('.alarmCount').html();
                num = num - 1;
                //console.log(num);
                $('.alarmCount').html(num);
            }
        })
        
    }
    $(".alarm-windows #user-window" + id + " .chatarea").perfectScrollbar({
        suppressScrollX: true
    });
});

//var alarmCount;
var oDomCount=$('.alarm-list').children().length;
var alarmArray = [];
var newAlarmArray = [];
var audio = document.getElementById("bgMusic");
var alarmListCount;
function getAlarmList() {
    var strVar = '';
    $.ajax({
        type: "get",
        url: "/Adm/Control/GetAlarm",
        dataType: "json",
        success: function (result) {
            var alarmCount = result.data.length;
            lastAlarmCount = result.data.length;
            $('.alarmCount').html(alarmCount);
            if (alarmCount > alarmListCount) {
                audio.play();
                $('.alarm-bell').addClass('.twinkle-bell');
            };
            for (var i = 0, l = result.data.length; i < l ; i++) {
                alarmArray .push(result.data[i].ID);
                var id = result.data[i].ID;
                var elderID = result.data[i].ElderID;
                var elderName = result.data[i].ElderName;
                var alarmTypeName = result.data[i].AlarmTypeName;
                var alarmTime = result.data[i].AlarmTime;
                strVar += "<li class=\"unread busy alarmList\" id=" + id + " onclick='clickFun(this)'>\n";
                strVar += "<a href=\"javascript:;\">\n";
                strVar += "<div class=\"notice-icon\">\n";
                strVar += "<i class=\"fa fa-exclamation-circle\"><\/i>\n";
                strVar += "<\/div>\n";
                strVar += "<div>\n";
                strVar += "<span class=\"name\">\n";
                strVar += "<strong><span class='elderName'>" + elderName + "<\/span>：<span class='alarmTypeName'>" + alarmTypeName + "<\/span><\/strong>\n";
                strVar += "<span class=\"time small\">" + alarmTime + "<\/span>\n";
                strVar += "<\/span>\n";
                strVar += "<div class=\"icon-primary\" style=\"position:absolute;top:25px;right:10px;\" onclick='goMap("+elderID+")'>\n";
                strVar += "<i class=\"fa fa-map-marker\" style=\"font-size:30px\"><\/i>\n";
                strVar += "<\/div>\n";
                strVar += "<\/div>\n";
                strVar += "<\/a>\n";
                strVar += "<\/li>\n";
            }
            $(".alarm-list").append(strVar);
            alarmListCount = $(".alarm-list").children().length;
            if (alarmListCount == 0) {
                $('.alarm-bell').removeClass('twinkle-bell');
            } else if (alarmListCount > 0) {
                $('.alarm-bell').addClass('twinkle-bell');
            }
        }
    })
}

function getNewAlarm() {
    var strVar = '';
    $.ajax({
        type: "get",
        url: "/Adm/Control/GetAlarm",
        dataType: "json",
        success: function (result) {
            newAlarmArray.length = 0;
            for (var i = 0, l = result.data.length; i < l ; i++) {
                newAlarmArray.push(result.data[i].ID);
            }
            if (diff(newAlarmArray, alarmArray) != 0) {

            }
        }
    })
}

getAlarmList();
setInterval(function () {
    $('.alarm-list').empty();
    getAlarmList();
}, 60000)

function clickFun(obj) {
    var name = $(obj).find(".elderName").html();
    var img = $(obj).find(".user-img a img").attr("src");
    var img = '../../../../../assets/images/profile.png';
    var id = $(obj).attr("id");
    var alarmTime = $(obj).find(".time").html();
    var alarm = $(obj).find(".alarmTypeName").html();
    console.log(name, alarm, id)
    if ($(obj).hasClass("active")) {
        $(obj).toggleClass("active");
        $("#user-window" + id).hide();
    } else {
        $(obj).toggleClass("active");

        if ($(".alarm-windows #user-window" + id).length) {

            $(".alarm-windows #user-window" + id).removeClass("minimizeit").show();
        } else {
            var msg = chatformat_msg(alarm, 'receive', name, alarmTime);
            //msg += chatformat_msg('Yes! Ultra Admin Theme ;)', 'sent', 'You');
            var html = "<div class='user-window' id='user-window" + id + "' data-user-id='" + id + "'>";
            html += "<div class='controlbar'><img src='" + img + "' data-user-id='" + id + "' rel='tooltip' data-animate='animated fadeIn' data-toggle='tooltip' data-original-title='" + name + "' data-placement='top' data-color-class='primary'><span class='status busy'><i class='fa fa-circle'></i></span><span class='name'>" + name + "</span><span class='opts'><i class='fa fa-times closeit' data-user-id='" + id + "'></i><i class='fa fa-minus minimizeit' data-user-id='" + id + "'></i></span></div>";
            html += "<div class='chatarea'>" + msg + "</div>";
            html += "<div class='typearea'><input type='text' data-user-id='" + id + "' placeholder='請輸入...' class='form-control'></div>";
            html += "</div>";
            $(".alarm-windows").append(html);
        }
    }
}


$('#elderQuery').click(function () {
    if ($('#elderQueryBox').hasClass('focus')) {
        var html = '';
        var elderList = [];
        var queryText = $('#elderQueryText').val();
        $('#elderQueryText').val('');
        if (queryText != '') {
            goQuery(queryText);
        } else {
            
            
        }
    }
})

$('#elderQueryBox').bind('keypress', function (event) {
    if (event.keyCode == "13") {
        if ($('#elderQueryBox').hasClass('focus')) {
            var html = '';
            var elderList = [];
            var queryText = $('#elderQueryText').val();
            $('#elderQueryText').val('');
            if (queryText != '') {
                goQuery(queryText);
            }
        }
    }
});

$('.logout').click(function () {
    $.ajax({
        type: 'post',
        url: '/User/Logout',
        dataType: 'json',
        success: function (d) {
            //console.log(d);
            window.location.href = "http://192.168.1.124";
        }
    })
})


function goQuery(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Elder/ElderQueryR/0/0/0?queryValue=' + id;
}

function goMap(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Control/Welcome?flag=true&elderId=' + id;
}