

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
var elderID = GetQueryString("elderId");

function drawBRChart(echarts) {
    var myBRChart = echarts.init(document.getElementById('echartBR'));
    var searchElder = $('#searchElder').val();
    var searchDate = $('#searchDate').val();
    myBRChart.setOption(option = {
        title: {
            x: 'center',
            padding: 15,
            textStyle: {
                fontSize: 18,
                color: '#fff'
            },
            text: '歷 史 呼 吸'
        },
        tooltip: {
            trigger: 'axis'
        },
        dataZoom: [
            {
                show: true,
                realtime: true,
                textStyle:{
                    color: 'rgba(255,255,255,0.8)'
                },
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
                data: []
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
                nameTextStyle: {
                    color: '#fff',
                    fontSize: 16
                },
                boundaryGap: [0, 0],
                splitNumber: 5
            }
        ],
        series: {
            name: '呼吸',
            type: 'line',
            data: []
        }
    });
    $.ajax({
            type: "get",
            async: true,
            url: "/Statistics/GetHistoryBreathing?ElderName=" + searchElder + "&searchDateTime=" + searchDate,
            dataType: "json",
            success: function (result) {
                console.log(result);
                if (result.msg) {
                    alert('查詢訊息量過大，請減少查詢日期間隔');
                };
                var breathing = [];
                var times = [];
                for (var i = 0, l = result.length; i < l; i++) {
                    breathing.unshift(result[i].value);
                    times.unshift(result[i].time)
                }
                myBRChart.setOption(option = {
                    title: {
                        text: '歷 史 呼 吸'
                    },
                    xAxis: [{
                            data: times
                    }],
                    yAxis: [{
                            name: '姓名：' + searchElder,
                    }],
                    series: {
                        data: breathing
                    }
                });
            },
            error: function (errorMsg) {
                alert("請求數據失敗!");
            }
    });
}
