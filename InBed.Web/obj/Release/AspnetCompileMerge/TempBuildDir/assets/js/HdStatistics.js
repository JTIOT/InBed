

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
var elderID = GetQueryString("elderId");

function drawHdChart(echarts) {
    var myHdChart = echarts.init(document.getElementById('echartHd'));
    var searchElder = $('#searchElder').val();
    var searchDate = $('#searchDate').val();
    myHdChart.setOption(option = {
        title: {
            x: 'center',
            padding: 15,
            textStyle: {
                fontSize: 18,
                color: '#fff'
            },
            text: '歷 史 心 率'
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
                //boundaryGap: [0, '100%'],
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
            name: '心率',
            type: 'line',
            data: []
        }
    });
    $.ajax({
            type: "get",
            async: true,
            url: "/Statistics/GetHistoryHeartRate?ElderName=" + searchElder + "&searchDateTime=" + searchDate,
            dataType: "json",
            success: function (result) {
                if (result.msg) {
                    alert('查詢訊息量過大，請減少查詢日期間隔');
                };
                var heartRates = [];
                var times = [];
                var min = result.ElderSetUP.minheart;
                var max = result.ElderSetUP.maxheart;
                $('.min').val(min);
                $('.max').val(max);
                for (var i = 0, l = result.Data.length; i < l; i++) {
                    heartRates.unshift(result.Data[i].value);
                    times.unshift(result.Data[i].time)
                }
                myHdChart.setOption(option = {
                    title: {
                        text: '歷 史 心 率'
                    },
                    xAxis: [{
                            data: times
                    }],
                    yAxis: [{
                            name: '姓名：' + searchElder,
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
                $('.input-box').show();
            },
            error: function (errorMsg) {
                alert("請求數據失敗!");
            }
    });
}
