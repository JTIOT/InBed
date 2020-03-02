
var mheight = $(window).height();
$('.elder-container').css('min-height', mheight)

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return null;
}
var queryText = GetQueryString("queryValue");
(function () {
    var html = '';
    var elderList = [];
    $.ajax({
        type: "get",
        url: "/Elder/ElderQuery?queryValue=" + queryText,
        dataType: "json",
        success: function (result) {
            console.log(result);
            //$('.elder-container').empty();
            if (result.data == 0) {
                alert('沒有查詢到相關訊息')
            } else {
                for (var i = 0; i < result.data.length; i++) {
                    var elderID = result.data[i].ElderID;
                    var elderName = (result.data[i].ElderName).substring(0, 4);
                    var homePhone = result.data[i].HomePhone;
                    var elderAge = result.data[i].Age;
                    var sexName = result.data[i].SexName;
                    var heartNum = result.data[i].HD;
                    var beat = '';
                    if (heartNum == 0) {
                        beat = 0;
                    } else if (heartNum > 0 && heartNum < 60) {
                        beat = 1;
                    } else if (heartNum >= 60 && heartNum <= 80) {
                        beat = 2;
                    } else if (heartNum > 80) {
                        beat = 3;
                    }
                    var lightBg = '';
                    var isOnline = result.data[i].Online;
                    var online = '';
                    if (isOnline == 0) {
                        online = '設備離線';
                    }
                    else {
                        online = '設備在線';
                    }
                    var isInBed = result.data[i].Inbed;
                    var inBed = '';
                    if (isInBed == 0) {
                        inBed = '老人離床';
                    }
                    else {
                        inBed = '老人在床';
                    }
                    html += "<div class='elder-box' id=" + elderID + ">";
                    html += "<div class='elder-list bg1'>";
                    html += "<div class='elder-info'>";
                    html += "<button class='skip' onclick='goDetail(" + elderID + ")'></button>";
                    html += "<div class='elder-info-img'>";
                    html += "<img src='../../../../../assets/images/oldman-img/pic1.jpg'>";
                    html += "</div>";
                    html += "<div class='elder-info-txt'>";
                    html += "<ul><li>姓名：" + elderName + "</li><li>性別：" + sexName + "</li></ul>";
                    html += "<ul class='age-tele'><li>年齡：" + elderAge + "</li><li>電話：" + homePhone + "</li></ul>";
                    html += "</div>";
                    html += "<div class='goMapIcon'>";
                    html += "<i class='fa fa-location-arrow'></i>"
                    html += "</div>";
                    html += "<button class='goMap' onclick='goMap(" + elderID + ")'>";
                    html += "</button>";
                    html += "</div>";
                    html += "<div class='system-state'>";
                    html += "<div class='xindiantu'>";
                    html += "<img class='beatImg' src='../../../../../assets/images/electrocardiogram" + beat + ".gif'>";
                    html += "<span class='heartNum'>" + heartNum + "</span>";
                    html += "</div>";
                    html += "<div class='state-light'>";
                    html += "<div class='equipment-online'>";
                    html += "<div class='eq-light isOn" + isOnline + "'>";
                    html += "<img src='../../../../../assets/images/online.png'>";
                    html += "</div>";
                    html += "<p class='isOnline'>" + online + "</p>";
                    html += "</div>";
                    html += "<div class='elder-inbed'>";
                    html += "<div class='bed-light isOn" + isInBed + "'>";
                    html += "<img src='../../../../../assets/images/inbed.png'>";
                    html += "</div>";
                    html += "<p class='isInBed'>" + inBed + "</p>";
                    html += "</div>";
                    html += "</div>";
                    html += "</div>";
                    html += "</div>";
                    html += "</div>";
                    elderList.push(result.data[i]);
                }
                $(".elder-container").append(html);
                //$(".elder-container").css('minHeight', mheight);
            }
        }
    })
}());

function goDetail(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Statistics/ElderStatistics/1095/1145/0?elderId=' + id;
}

function goMap(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Control/Welcome?flag=true&elderId=' + id;
}