var fWidth = $('.elder-container').width();
var colCount = Math.floor($('.elder-container').width() / 335);
var elderList = [];
var gPageSize = 10;
var i = 1; 
var url = '/Elder/GetElderPages?Start=0&&Length=0&&OrderBy=%27id%27&&%20OrderDir=‘desc’';
var url_left = url.substring(0, 27);
var url_Length = url.substring(28, 37);
var url_right = url.substring(39);
var Length = colCount * 10;
var nextPage = 0;
var isScroll = true;

$(function () {
    
    $(window).scroll(function () {
        var scrollTop = $(this).scrollTop();
        var scrollHeight = $(document).height();
        var windowHeight = $(this).height();
        if (Math.round(scrollTop + windowHeight) >= scrollHeight) {
            if (isScroll) {
                isScroll = false;
                $('.loaddiv').show();
                nextPage++;
                insertElder(0, url_left + nextPage * Length + url_Length + Length + url_right, true, 1);
            }
        }
    });
    //5秒更新
    setInterval(function () {
        var urlleft = url.substring(0, 37);
        var urlright = url.substring(39);
        var urlLength = nextPage * Length + Length;
        insertElder(0, url_left + 0 + url_Length + urlLength + url_right, true,0)
    },5000);

    function insertElder(flag, s_url, isShow, roll) {
        var html = '';
        $.ajax({
            type: "get",
            url:s_url,
            dataType: "json",
            success: function (result) {
                $('.loaddiv').hide();
                if (flag == 1){
                    for (var i = 0; i < result.data.length; i++) {
                        var count = 0;
                        for (var j = i; j < elderList.length; j++) {
                            if (result.data[i].ElderID == elderList[j].ElderID) {
                                count++;
                                if (result.data[i].Online != elderList[j].Online ||
                                    result.data[i].Inbed != elderList[j].Inbed ||
                                    result.data[i].HD != elderList[j].HD) {
                                    elderList[j].Online = result.data[i].Online;
                                    elderList[j].Inbed = result.data[i].Inbed;
                                    elderList[j].HD = result.data[i].HD;
                                    var obj = document.getElementsByClassName(result.data[i].ElderID)[0];
                                    var online = '';
                                    var inBed = '';
                                    var islight = '';
                                    if (obj != null) {
                                        obj.querySelector('.heartNum').innerHTML = elderList[j].HD;
                                        console.log(elderList[j].HD)
                                        var beatImg = '';
                                        if (elderList[j].HD == 0) {
                                            beatImg = '../../../../../assets/images/electrocardiogram0.gif'
                                        } else if (elderList[j].HD > 0 && elderList[j].HD < 60) {
                                            beatImg = '../../../../../assets/images/electrocardiogram1.gif'
                                        } else if (elderList[j].HD >= 60 && elderList[j].HD <= 80) {
                                            beatImg = '../../../../../assets/images/electrocardiogram2.gif'
                                        } else if (elderList[j].HD > 80) {
                                            beatImg = '../../../../../assets/images/electrocardiogram3.gif'
                                        }
                                        obj.querySelector('.beatImg').src = beatImg;
                                        if (elderList[j].Online == 0) {
                                            online = '設備離線';
                                            onlineLight = 'gray';
                                        }
                                        else {
                                            online = '設備在線';
                                            onlineLight = 'green';
                                        }
                                        if (elderList[j].Inbed == 0) {
                                            inBed = '老人離床';
                                            bedLight = 'gray';
                                        }
                                        else {
                                            inBed = '老人在床';
                                            bedLight = 'green';
                                        }
                                        obj.querySelector('.isOnline').innerHTML = online;
                                        obj.querySelector('.eq-light').style.background = onlineLight;
                                        obj.querySelector('.isInBed').innerHTML = inBed;
                                        obj.querySelector('.bed-light').style.background = bedLight;
                                    }
                                }
                            }
                        }
                    }
                } else {
                    if (isShow) {
                        for (var i = 0; i < result.data.length; i++) {
                            var elderID = result.data[i].ElderID;
                            var elderName = (result.data[i].ElderName).substring(0, 4);
                            var homePhone = result.data[i].HomePhone;
                            var homeAdderss = result.data[i].HomeAdderss;
                            var elderAge = result.data[i].Age;
                            var sexName = result.data[i].SexName;
                            var heartNum = result.data[i].HD;
                            var maxheart = result.data[i].AlarmSetup.maxheart;
                            var minheart = result.data[i].AlarmSetup.minheart;
                            var beat = '';
                            if (heartNum == 0) {
                                beat = 0;
                            } else if (heartNum > 0 && heartNum < minheart) {
                                beat = 1;
                            } else if (heartNum >= minheart && heartNum <= maxheart) {
                                beat = 2;
                            } else if (heartNum > maxheart) {
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
                            html += "<div class='elder-box " + elderID + "'>";
                            html += "<div class='elder-list bg1'>";
                            html += "<div class='elder-info'>";
                            html += "<button class='skip' onclick='goDetail(" + elderID + ")'></button>";
                            html += "<div class='elder-info-img'>";
                            html += "<img src='../../../../../assets/images/oldman-img/pic1.jpg'>";
                            html += "</div>";
                            html += "<div class='elder-info-txt'>";
                            html += "<ul><li>姓名：" + elderName + "</li><li>性别：" + sexName + "</li></ul>";
                            html += "<ul class='age-tele'><li>年齡：" + elderAge + "</li><li>電話：" + homePhone + "</li></ul>";
                            html += "</div>";
                            html += "<div class='goMapIcon'>";
                            html += "<i class='fa fa-map-marker'></i>"
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
                        if (roll == 1) {
                            $('.loaddiv').show();
                            $(".elder-container").append(html);
                            isScroll = true;
                        } else if (roll == 0) {
                            $('.elder-container').empty();
                            $(".elder-container").append(html);
                            //$('.loaddiv').hide();
                            //$("html , body").animate({ scrollTop: 40 }, 30);
                        }
                    }
                }
            },
            error: function (error) {
                $('.loaddiv').hide();
                console.log('error');
            }
        })
    }
    insertElder(0, url_left + nextPage + url_Length + Length + url_right, true,0);
});

function goDetail(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Statistics/ElderStatistics/1095/1145/0?elderId=' + id;
}

function goMap(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Control/Welcome?flag=true&elderId=' + id;
}