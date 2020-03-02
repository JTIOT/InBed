
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
            text: '歷 史 作 息'
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
                    },
                    formatter: function (v) {
                        return dateStart(v)
                    },
                },
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value',
                inverse: true,
                //boundaryGap: [0, '100%'],
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    },
                    formatter: function (v) {
                        return timeTamp(v)
                    },
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
        series: [{
            name: '起床時間',
            type: 'line',
            data: []
        },
        {
            name: '入睡時間',
            type: 'line',
            itemStyle: {
                normal: {
                    lineStyle: {
                        color: 'green'
                    }
                }
            },
            data: []
        }]
    });
    $.ajax({
        type: "get",
        async: true,
        url: "/Statistics/GetHistoryRest?ElderName=" + searchElder + "&searchDateTime=" + searchDate,
        dataType: "json",
        success: function (result) {
            //console.log(result.data);
            if (result.msg) {
                alert('查詢訊息量過大，請減少查詢日期間隔');
            };
            var sleepData1 = [];
            var sleepData2 = [];
            var inbedTime = [];
            var inbedDate = [];
            var getupTime = [];
            var getupDate = [];
            if (result.data != null) {
                for (var i = 0, l = result.data.length; i < l; i++) {
                    var time1 = result.data[i].InBedTime;
                    var time2 = result.data[i].GetUpTime;
                    inbedTime.unshift(toHourMinute(dateEnd(time1)));
                    inbedDate.unshift(dateStart(result.data[i].InBedTime) +' '+ dateStart(result.data[i].GetUpTime));
                    getupTime.unshift(toHourMinute(dateEnd(time2)));
                }
                myPressureChart.setOption(option = {
                    tooltip: {
                        formatter: function (params) {
                            return '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:green"></span>'
                            +'起床時間：' + dateEnd(params[0].axisValue) + '&nbsp;&nbsp' + timeTamp(params[1].data) + ' <br/>'
                            +'<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:' + params[0].color + '"></span>'
                            + '上床時間：' + dateStart(params[0].axisValue) + '&nbsp;&nbsp' + timeTamp(params[0].data) + ' <br/>'
                        },
                    },
                    xAxis: [{
                        data: inbedDate, getupDate
                    }],
                    series: [
                        { 
                            data: inbedTime
                        },
                        {
                            data: getupTime
                        },
                        
                    ]
                })
            }
        },
        error: function (errorMsg) {
            alert("請求數據失敗!");
        }
    });
}

