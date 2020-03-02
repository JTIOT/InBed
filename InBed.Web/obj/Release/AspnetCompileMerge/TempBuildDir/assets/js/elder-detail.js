
var chartWidth = $('#echart1').width();
$('#echart2').width(chartWidth);
$('#echart3').width(chartWidth);
$('#echart4').width(chartWidth);

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
var elderID = GetQueryString("elderId");

var mapElderIdToCustomerId = {
    399: 201812290001,
    411: 201812290005,
    410: 201812290004,
    400: 201812290002,
    414: 201911040001,
    415: 201911040002,
    416: 201911040003,
    45: 201503120001,
    44: 201503100002
}

console.log("/Statistics/GetElderStatistics?elderId=" + elderID + "&customerId=" + mapElderIdToCustomerId[elderID]);

(function () {
    var elderName = $('#elderName');
    var birthday = $('#birthday');
    var sex = $('#sex');
    var homePhone = $('#homePhone');
    var homeAddress = $('#homeAddress');
    var sleepLength = $('#sleepLength');
    var inBedTime = $('#inBedTime');
    var nightCount = $('#nightCount');
    var deepSleepLength = $('#deepSleepLength');
    var getUpTime = $('#getUpTime');
    var fallAsleepTime = $('#fallAsleepTime');
    var averageBreath = $('#averageBreath');
    var averageHeartRate = $('#averageHM');
    var fallAsleepLength = $('#fallAsleepLength');
    $.ajax({
        type: "get",
        async: true,            //异步请求（同步请求将会锁住浏览器，用户其他操作必须等待请求完成才可以执行）
        url: "/Statistics/GetElderStatistics?elderId=" + elderID + "&customerId=" + mapElderIdToCustomerId[elderID],
        dataType: "json",        //返回数据形式为json
        success: function (result) {
            elderName.html(result.data.ElderName);
            birthday.html(result.data.Birthday);
            homePhone.html(result.data.HomePhone);
            homeAddress.html(result.data.HomeAddress);
            if (result.data.Sex == 1) {
                sex.html('男');
            } else {
                sex.html('女');
            };
            if (result.data.SleepLength) {
                sleepLength.html(timeStampSec(result.data.SleepLength));
            } else {
                sleepLength.html('暫無數據');
            }
            if (result.data.InBedTime) {
                inBedTime.html(result.data.InBedTime);
            } else {
                inBedTime.html('暫無數據');
            }
            if (result.data.NightCount!=null) {
                nightCount.html(result.data.NightCount+'次');
            } else {
                nightCount.html('暫無數據');
            }
            if (result.data.DeepSleepLength) {
                deepSleepLength.html(timeStampSec(result.data.DeepSleepLength));
            } else {
                deepSleepLength.html('暫無數據');
            }
            if (result.data.GetUpTime) {
                getUpTime.html(result.data.GetUpTime);
            } else {
                getUpTime.html('暫無數據');
            }
            if (result.data.FallAsleepTime) {
                fallAsleepTime.html(result.data.FallAsleepTime);
            } else {
                fallAsleepTime.html('暫無數據');
            }
            if (result.data.AverageBreath) {
                averageBreath.html(result.data.AverageBreath + '次/分');
            } else {
                averageBreath.html('暫無數據');
            }
            if (result.data.AverageHM) {
                averageHeartRate.html(result.data.AverageHM + '次/分');
            } else {
                averageHeartRate.html('暫無數據');
            }
            if (result.data.FallAsleepLength) {
                fallAsleepLength.html(result.data.FallAsleepLength+'分');
            } else {
                fallAsleepLength.html('暫無數據');
            }
        },
        error: function (errorMsg) {

        }
    });
}());

drawEcharts(echarts);
function drawEcharts(echarts) {
    drawLine1(echarts);
    drawKLine1(echarts);
    drawSleepChart(echarts);
    drawLine2(echarts);
}

var elderData = (function () {
    var data;
    $.ajax({
        type: 'get',
        url: '/Statistics/GetHeartRate?elderId=' + elderID,
        dataType: 'json',
        async: false,
        success: function (result) {
            data = result;
        }
    });
    return data;
})();

var upData;
setInterval(function () {
    var upData1 = (function () {
        var data;
        $.ajax({
            type: 'get',
            url: '/Statistics/GetHeartRate?elderId=' + elderID,
            dataType: 'json',
            async: false,
            success: function (result) {
                data = result;
            }
        });
        return data;
    })()
    upData = upData1;
}, 5000)

function drawLine1(echarts) {
    var myLineChart1 = echarts.init(document.getElementById('echart1'));
    var x = [];
    var y = [];
    var X_valueArray = [];
    var Y_valueArray = [];
    var heartRate = [];
    var heartRates = [];
    var time = [];
    var times = [];
    myLineChart1.setOption(option = {
        title: {
            x: 'center',
            padding: 15,
            textStyle: {
                fontSize: 18,
                color: '#fff'
            },
            text: '實 時 心 率'
        },
        tooltip: {
            trigger: 'axis'
        },
        dataZoom: [
        {
            show: true,
            realtime: true,
            handleColor: 'rgba(255,255,255,1)',
            start: 0,
            end: 100
        },
        {
            type: 'inside',
            realtime: true,
        }
        ],
        xAxis: [
            {
                splitLine: { show: false },
                type: 'category',
                boundaryGap: true,
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    }
                },
                //data: times
            }
        ],
        yAxis: [
            {
                type: 'value',
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    }
                },
                scale: true,
                name: '心率',
                nameTextStyle: {
                    color: '#fff',
                    fontSize: 16
                },
                boundaryGap: [0, 0],
                splitNumber: 5
            }
        ],
        
        series: {
            name: '實時心率',
            type: 'line',
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(255, 255, 255,0.1)'
                    }, {
                        offset: 1,
                        color: 'rgba(255, 255, 255,0.1)'
                    }])
                }
            },
        }
    });

    setTimeout(function () {
        var a = elderData.Data;
        var value_x = localStorage.getItem("key1_" + elderID);
        var value_y = localStorage.getItem("key2_" + elderID);
        var min = elderData.ElderSetUP.minheart;
        var max = elderData.ElderSetUP.maxheart;
        $('.min').val(min);
        $('.max').val(max);
        if (value_x != null) {
            X_valueArray = value_x.split(',');
            Y_valueArray = value_y.split(',');
        }
        if (a.length != 0) {
            if (300 - X_valueArray.length >= a.length) {
                for (var i = a.length - 1; i > -1; i--) {
                    X_valueArray.push(a[i].time);
                    Y_valueArray.push(a[i].value);
                }
                localStorage.setItem("key1_" + elderID, X_valueArray);
                localStorage.setItem("key2_" + elderID, Y_valueArray);
            } else {
                var removeCount = a.length - (300 - X_valueArray.length);
                X_valueArray.splice(0, removeCount);
                Y_valueArray.splice(0, removeCount);
                for (var i = a.length - 1; i > -1; i--) {
                    X_valueArray.push(a[i].time);
                    Y_valueArray.push(a[i].value);
                }
                localStorage.setItem("key1_" + elderID, X_valueArray);
                localStorage.setItem("key2_" + elderID, Y_valueArray);
            }
        }
        time = localStorage.getItem("key1_" + elderID);
        times = time.split(',');
        heartRate = localStorage.getItem("key2_" + elderID);
        heartRates = heartRate.split(',');
        myLineChart1.setOption(option = {
            xAxis: [{
                data: times
            }],
            visualMap: {
                top: 80,
                right: 55,
                textStyle: {
                    color: '#fff'
                },
                pieces: [{
                    lte: min,
                    color: '#ffde33',

                }, {
                    gt: min,
                    lte: max,
                    color: '#096'
                }, {
                    gt: max,
                    color: '#cc0033'
                }],
                outOfRange: {
                    color: '#999'
                }
            },
            series: {
                data: heartRates
            }
        });
    }, 200)

    setInterval(function () {
        if (upData) {
            var a = upData.Data;
            if (a != null && a.length != 0) {
                if (X_valueArray.length < 300) {
                    for (var i = 0; i < a.length; i++) {
                        X_valueArray.push(a[i].time);
                        Y_valueArray.push(a[i].value);
                    }
                    localStorage.setItem("key1_" + elderID, X_valueArray);
                    localStorage.setItem("key2_" + elderID, Y_valueArray);
                    myLineChart1.setOption(option = {
                        xAxis: [{
                            data: X_valueArray
                        }],
                        series: {
                            data: Y_valueArray
                        }
                    })
                } else {
                    for (var i = 0; i < a.length; i++) {
                        X_valueArray.push(a[i].time);
                        X_valueArray.shift();
                        Y_valueArray.push(a[i].value);
                        Y_valueArray.shift();
                    }
                    localStorage.setItem("key1_" + elderID, X_valueArray);
                    localStorage.setItem("key2_" + elderID, Y_valueArray);
                    myLineChart1.setOption(option = {
                        xAxis: [{
                            data: X_valueArray
                        }],
                        series: {
                            data: Y_valueArray
                        }
                    })
                }
            }
        }
    }, 5000)
}

function drawKLine1(echarts) {
    var myKlineChart1 = echarts.init(document.getElementById('echart2'));
    var x = [];
    var y = [];
    var X_valueArray = [];
    var Y_valueArray = [];
    var BRdata = [];
    var BRdatas = [];
    var time = [];
    var times = [];
    myKlineChart1.setOption(option = {
        title: {
            x: 'center',
            padding: 15,
            textStyle: {
                fontSize: 18,
                color: '#fff'
            },
            text: '實 時 呼 吸'
        },
        tooltip: {
            trigger: 'axis'
        },
        dataZoom: [
        {
            show: true,
            realtime: true,
            handleColor: 'rgba(255,255,255,1)',
            start: 0,
            end: 100
        },
        {
            type: 'inside',
            realtime: true,
        }
        ],
        xAxis: [
            {
                splitLine: { show: false },
                type: 'category',
                boundaryGap: true,
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    }
                },
                data: times
            }
        ],
        yAxis: [
            {
                type: 'value',
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    }
                },
                scale: true,
                name: '呼吸',
                nameTextStyle: {
                    color: '#fff',
                    fontSize: 16
                },
                boundaryGap: [0, 0],
                splitNumber: 5
            }
        ],
        series: {
            name: '實時呼吸',
            type: 'line',
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(255, 255, 255,0.1)'
                    }, {
                        offset: 1,
                        color: 'rgba(255, 255, 255,0.1)'
                    }])
                }
            },
            //data: heartRates
        }
    });
    setTimeout(function () {
        var a = elderData.BRData;
        var value_x = localStorage.getItem("BRkey1_" + elderID);
        var value_y = localStorage.getItem("BRkey2_" + elderID);

        if (value_x != null) {
            X_valueArray = value_x.split(',');
            Y_valueArray = value_y.split(',');
        }
        if (a.length != 0) {
            if (10000 - X_valueArray.length >= a.length) {
                for (var i = a.length - 1; i > -1; i--) {
                    X_valueArray.push(a[i].time);
                    Y_valueArray.push(a[i].value);
                }
                localStorage.setItem("BRkey1_" + elderID, X_valueArray);
                localStorage.setItem("BRkey2_" + elderID, Y_valueArray);
            } else {
                var removeCount = a.length - (10000 - X_valueArray.length);
                X_valueArray.splice(0, removeCount);
                Y_valueArray.splice(0, removeCount);
                for (var i = a.length - 1; i > -1; i--) {
                    X_valueArray.push(a[i].time);
                    Y_valueArray.push(a[i].value);
                }
                localStorage.setItem("BRkey1_" + elderID, X_valueArray);
                localStorage.setItem("BRkey2_" + elderID, Y_valueArray);
            }
        }
        time = localStorage.getItem("BRkey1_" + elderID);
        times = time.split(',');
        BRdata = localStorage.getItem("BRkey2_" + elderID);
        BRdatas = BRdata.split(',');
        myKlineChart1.setOption(option = {
            xAxis: [{
                data: times
            }],
            series: {
                data: BRdatas
            }
        })
    }, 200)

    setInterval(function () {
        if (upData) {
            var a = upData.BRData;
            if (a != null && a.length != 0) {
                if (X_valueArray.length < 10000) {
                    for (var i = 0; i < a.length; i++) {
                        X_valueArray.push(a[i].time);
                        Y_valueArray.push(a[i].value);
                    }
                    localStorage.setItem("BRkey1_" + elderID, X_valueArray);
                    localStorage.setItem("BRkey2_" + elderID, Y_valueArray);
                    myKlineChart1.setOption(option = {
                        xAxis: [{
                            data: X_valueArray
                        }],
                        series: {
                            data: Y_valueArray
                        }
                    })
                } else {
                    for (var i = 0; i < a.length; i++) {
                        X_valueArray.push(a[i].time);
                        X_valueArray.shift();
                        Y_valueArray.push(a[i].value);
                        Y_valueArray.shift();
                    }
                    localStorage.setItem("BRkey1_" + elderID, X_valueArray);
                    localStorage.setItem("BRkey2_" + elderID, Y_valueArray);
                    myKlineChart1.setOption(option = {
                        xAxis: [{
                            data: X_valueArray
                        }],
                        series: {
                            data: Y_valueArray
                        }
                    })
                }
            }
        }
    }, 5000)
}

function drawLine2(echarts) {
    var myLineChart2 = echarts.init(document.getElementById('echart4'));
    var x = [];
    var y = [];
    var X_valueArray = [];
    var Y_valueArray = [];
    var BRdata = [];
    var BRdatas = [];
    var time = [];
    var times = [];
    myLineChart2.setOption(option = {
        title: {
            x: 'center',
            padding: 15,
            textStyle: {
                fontSize: 18,
                color: '#fff'
            },
            text: '重 壓 值'
        },
        tooltip: {
            trigger: 'axis'
        },
        dataZoom: [
        {
            show: true,
            realtime: true,
            handleColor: 'rgba(255,255,255,1)',
            start: 0,
            end: 100
        },
        {
            type: 'inside',
            realtime: true,
        }
        ],
        xAxis: [
            {
                splitLine: { show: false },
                type: 'category',
                boundaryGap: true,
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    }
                },
                data: times
            }
        ],
        yAxis: [
            {
                type: 'value',
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    }
                },
                scale: true,
                name: '重壓值',
                nameTextStyle: {
                    color: '#fff',
                    fontSize: 16
                },
                boundaryGap: [0, 0],
                splitNumber: 5
            }
        ],
        series: {
            name: '重壓值',
            type: 'line',
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(255, 255, 255,0.1)'
                    }, {
                        offset: 1,
                        color: 'rgba(255, 255, 255,0.1)'
                    }])
                }
            },
            //data: heartRates
        }
    });
    setTimeout(function () {
        var a = elderData.PressureData;
        var value_x = localStorage.getItem("Pressurekey1_" + elderID);
        var value_y = localStorage.getItem("Pressurekey2_" + elderID);

        if (value_x != null) {
            X_valueArray = value_x.split(',');
            Y_valueArray = value_y.split(',');
        }
        if (a.length != 0) {
            if (17000 - X_valueArray.length >= a.length) {
                for (var i = a.length - 1; i > -1; i--) {
                    X_valueArray.push(a[i].time);
                    Y_valueArray.push(a[i].value);
                }
                localStorage.setItem("Pressurekey1_" + elderID, X_valueArray);
                localStorage.setItem("Pressurekey2_" + elderID, Y_valueArray);
            } else {
                var removeCount = a.length - (17000 - X_valueArray.length);
                X_valueArray.splice(0, removeCount);
                Y_valueArray.splice(0, removeCount);
                for (var i = a.length - 1; i > -1; i--) {
                    X_valueArray.push(a[i].time);
                    Y_valueArray.push(a[i].value);
                }
                localStorage.setItem("Pressurekey1_" + elderID, X_valueArray);
                localStorage.setItem("Pressurekey2_" + elderID, Y_valueArray);
            }
        }
        time = localStorage.getItem("Pressurekey1_" + elderID);
        times = time.split(',');
        BRdata = localStorage.getItem("Pressurekey2_" + elderID);
        BRdatas = BRdata.split(',');
        myLineChart2.setOption(option = {
            xAxis: [{
                data: times
            }],
            series: {
                data: BRdatas
            }
        })
    }, 200)

    setInterval(function () {
        if (upData) {
            var a = upData.PressureData;
            if (a != null && a.length != 0) {
                if (X_valueArray.length < 17000) {
                    for (var i = 0; i < a.length; i++) {
                        X_valueArray.push(a[i].time);
                        Y_valueArray.push(a[i].value);
                    }
                    localStorage.setItem("Pressurekey1_" + elderID, X_valueArray);
                    localStorage.setItem("Pressurekey2_" + elderID, Y_valueArray);
                    myLineChart2.setOption(option = {
                        xAxis: [{
                            data: X_valueArray
                        }],
                        series: {
                            data: Y_valueArray
                        }
                    })
                } else {
                    for (var i = 0; i < a.length; i++) {
                        X_valueArray.push(a[i].time);
                        X_valueArray.shift();
                        Y_valueArray.push(a[i].value);
                        Y_valueArray.shift();
                    }
                    localStorage.setItem("Pressurekey1_" + elderID, X_valueArray);
                    localStorage.setItem("Pressurekey2_" + elderID, Y_valueArray);
                    myLineChart2.setOption(option = {
                        xAxis: [{
                            data: X_valueArray
                        }],
                        series: {
                            data: Y_valueArray
                        }
                    })
                }
            }
        }
    }, 5000)
}

function drawSleepChart(echarts) {
    var mySleepChart = echarts.init(document.getElementById('echart3'));
    mySleepChart.setOption(option = {
        title: {
            x: 'center',
            padding: 15,
            textStyle: {
                fontSize: 18,
                color: '#fff'
            },
            text: '睡 眠 狀 況'
        },
        tooltip: {
            trigger: 'axis',
            formatter: function (params) {
                //console.log(params)
                params = params[0];
                return params.value[0] + ' <br/>' + '睡眠狀態：'+ params.name;
                //var date = new Date(params.name);
                //return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear() + ' : ' + params.value[1];
            },
        },
        dataZoom: [
        {
            show: true,
            realtime: true,
            handleColor: 'rgba(255,255,255,1)',
            start: 0,
            end: 100
        },
        {
            type: 'inside',
            realtime: true,
        }
        ],
        xAxis: [
            {
                splitLine: { show: false },
                type: 'time',
                boundaryGap: true,
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    },
                },
                //data: times
            }
        ],
        yAxis: [
            {
                type: 'value',
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    },
                    formatter: function (v) {
                        if (v == 4) { return '離床' }
                        else if (v == 3) { return '清醒' }
                        else if (v == 2) { return '淺睡' }
                        else if (v == 1) { return '深睡' }
                    },
                },
                scale: true,
                boundaryGap: [0, 0],
                splitNumber: 4
            }
        ],
        series: {
            name: '睡眠狀況',
            type: 'line',
            step: 'end',
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(255, 255, 255,0.1)'
                    }, {
                        offset: 1,
                        color: 'rgba(255, 255, 255,0.1)'
                    }])
                }
            },
            //data: heartRates
        }
    });
    $.ajax({
        type: "get",
        async: true,
        url: '/Statistics/GetCurrentDaySleepingDate?elderId=' + elderID,
        dataType: "json",
        success: function (result) {
            var sleepData = [];
            var times = [];
            for (var i = 0, l = result.length; i < l; i++) {
                var sleepType = {};
                var sleep = result[i].SleepType;
                var name = result[i].SleepTypeName;
                var time = result[i].BeginTime;
                if (sleep == -1) {
                    sleep = 4
                    sleepType.name = name;
                    sleepType.value = [time, sleep]
                } else if (sleep == 0) {
                    sleep = 3
                    sleepType.name = name;
                    sleepType.value = [time, sleep]
                } else if (sleep == 1) {
                    sleep = 2
                    sleepType.name = name;
                    sleepType.value = [time, sleep]
                } else if (sleep == 3) {
                    sleep = 1
                    sleepType.name = name;
                    sleepType.value = [time, sleep]
                }
                //sleepType.name = name;
                //sleepType.value = [time, sleep]
                sleepData.push(sleepType);
            }
            
            mySleepChart.setOption(option = {
                title: {
                    text: '睡 眠 狀 況'
                },
                series: {
                    data: sleepData
                }
            });
        },
        error: function (errorMsg) {
            alert("請求數據失敗");
        }
    });
}

Date.prototype.toLocaleString = function () {
    return this.getFullYear() + "/" + (this.getMonth() + 1) + "/" + this.getDate() + "/ " + this.getHours() + ":" + this.getMinutes() + ":" + this.getSeconds();
};