var elderList = [];
var gPageSize = 10;
var i = 1; //设置当前页数，全局变量
var url = '/Elder/GetElderPages?Start=0&&Length=0&&OrderBy=%27id%27&&%20OrderDir=‘desc’';
var url_left = url.substring(0, 27);
var url_Length = url.substring(28, 37);
var url_right = url.substring(39);
var Length = 20;
var nextPage = 0;

$(function () {
    
    $(window).scroll(function () {
        var scrollTop = $(this).scrollTop();
        var scrollHeight = $(document).height();
        var windowHeight = $(this).height();
        if (scrollTop + windowHeight == scrollHeight) {
            $('.loaddiv').show();
            nextPage++;
            insertElder(0, url_left + nextPage * Length + url_Length + Length + url_right, true)
        }
    });

    //5秒更新
    setInterval(function () {
        var urlleft = url.substring(0, 37);
        var urlright = url.substring(39);
        var urlLength = nextPage * Length + 20;
        insertElder(1, url_left + 0 + url_Length + urlLength + url_right, false)
    },5000);

    function insertElder(flag, s_url, isShow) {
        var html = '';
        $.ajax({
            type: "get",
            url:s_url,
            dataType: "json",
            success: function (result) {
                console.log(result)
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
                                    var obj = document.getElementById(result.data[i].ElderID);
                                    var online = '';
                                    var inBed = '';
                                    var islight = '';
                                    if (obj != null) {
                                        obj.querySelector('.heartNum').innerHTML=elderList[j].HD;
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
                                            online = '设备离线';
                                            onlineLight = 'gray';
                                        }
                                        else {
                                            online = '设备在线';
                                            onlineLight = 'green';
                                        }
                                        if (elderList[j].Inbed == 0) {
                                            inBed = '老人离床';
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
                                        console.log(obj);
                                        console.log(islight);
                                        console.log(inBed);
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
                            //var elderInfo = JSON.stringify(result.data[i]);
                            //var elderJson = {
                            //    ElderName: elderName,
                            //    Age: elderAge,
                            //    HomePhone: homePhone,
                            //    HomeAdderss: homeAdderss
                            //}
                            //var elderInfo = JSON.stringify(elderJson);
                            //console.log(elderInfo);
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
                                online = '设备离线';
                            }
                            else {
                                online = '设备在线';
                            }
                            var isInBed = result.data[i].Inbed;
                            var inBed = '';
                            if (isInBed == 0) {
                                inBed = '老人离床';
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
                            html += "<ul><li>姓名：" + elderName + "</li><li>性别：" + sexName + "</li></ul>";
                            html += "<ul class='age-tele'><li>年龄：" + elderAge + "</li><li>电话：" + homePhone + "</li></ul>";
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
                        //$('.elder-container').empty();
                        $(".elder-container").append(html);
                        $('.loaddiv').hide();
                        //$("html , body").animate({ scrollTop: 40 }, 30);
                    }
                }
            },
        })
    }
    insertElder(0, url_left + nextPage + url_Length + Length + url_right, true);
});

function goDetail(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Statistics/ElderStatistics/1095/1145/0?elderId=' + id;
}

function goMap(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Control/Welcome?flag=true&elderId=' + id;
    //map123()
}