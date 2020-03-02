
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
var elderID = GetQueryString("elderId");

function drawPressureChart(echarts) {
    var myPressureChart = echarts.init(document.getElementById('echartPressure'));
    var searchElder = $('#searchElder').val();
    var searchDate = $('#searchDate').val();
    
    myPressureChart.setOption(option = {
        title: {
            x: 'center',
            padding: 15,
            textStyle: {
                fontSize: 18,
                color: '#fff'
            },
            text: '歷 史 重 壓 值'
        },
        tooltip: {
            trigger: 'axis'
        },
        dataZoom: [
            {
                show: true,
                realtime: true,
                textStyle: {
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
            name: '重壓值',
            type: 'line',
            data: []
        }
    });
    console.log(searchElder);
    console.log(searchDate);
    $.ajax({
        type: "get",
        async: true,
        url: "/Statistics/GetHistoryWeight?ElderName=" + searchElder + "&searchDateTime=" + searchDate,
        dataType: "json",
        success: function (result) {
            if (result.msg) {
                alert('查詢訊息量過大，請減少查詢日期間隔');
            };
            console.log(result);
            var weight = [];
            var times = [];
            for (var i = 0, l = result.length; i < l; i++) {
                weight.unshift(result[i].value);
                times.unshift(result[i].time)
            }
            myPressureChart.setOption(option = {
                title: {
                    text: '歷 史 重 壓 值'
                },
                xAxis: [{
                    data: times
                }],
                yAxis: [{
                    name: '姓名：' + searchElder,
                }],
                series: {
                    data: weight
                }
            });
        },
        error: function (errorMsg) {
            alert("請求數據失敗!");
        }
    });
}
