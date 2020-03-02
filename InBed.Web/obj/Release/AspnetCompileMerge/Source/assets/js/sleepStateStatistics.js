
function drawSleepStateChart(echarts) {
    var mySleepStateChart = echarts.init(document.getElementById('echartSleepState'));
    var searchElder = $('#searchElder').val();
    var searchDate = $('#searchDate').val();
    mySleepStateChart.setOption(
        option = {
            tooltip : {
                trigger: 'axis',
                axisPointer : {            // 坐标轴指示器，坐标轴触发有效
                    type : 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            title: {
                x: 'center',
                padding: 15,
                textStyle: {
                    fontSize: 18,
                    color: '#fff'
                },
                text: '睡 眠 統 計'
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            xAxis:  {
                type: 'category',
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    }
                },
            },
            yAxis: {
                type: 'value',
                name: '分鐘',
                nameTextStyle: {
                    color: '#fff',
                },
                axisLabel: {
                    textStyle: {
                        color: '#fff'
                    }
                },
                
            },
            series: [
                {
                    name: '清醒時長',
                    type: 'bar',
                    stack: '总量',
                    itemStyle: {
                        normal: {
                            color: '#53FF53'
                        }
                    }
                },
                {
                    name: '淺睡時長',
                    type: 'bar',
                    stack: '总量',
                    itemStyle: {
                        normal: {
                            color: '#FFD306'
                        }
                    }
                },
                {
                    name: '深睡時長',
                    type: 'bar',
                    stack: '总量',
                    itemStyle: {
                        normal: {
                            color: '#F75000'
                        }
                    }
                },
            ]
        }
    );

    $.ajax({
        type: "get",
        async: true,
        url: "/Statistics/GetHistorySleep?ElderName=" + searchElder + "&searchDateTime=" + searchDate,
        dataType: "json",
        success: function (result) {
            console.log(result);
            if (result.msg) {
                alert('查詢訊息量過大，請減少查詢日期間隔');
            };
            var soberSleepLong = [];// 清醒时长
            var depthSeepLong = [];// 浅睡时长
            var shallowSleepLong = [];// 深睡时长
            var times = [];
            for (var i = 0, l = result.data.length; i < l; i++) {
                soberSleepLong.unshift(result.data[i].SoberSleepLong);
                depthSeepLong.unshift(result.data[i].DepthSeepLong);
                shallowSleepLong.unshift(result.data[i].ShallowSleepLong);
                times.unshift(dateStart(result.data[i].AnalysisDate))
            }
            mySleepStateChart.setOption(
                option = {
                    title: {
                        text: '睡 眠 統 計'
                    },
                    xAxis: [{
                        data: times,
                    }],
                    series: [{
                        data: soberSleepLong
                    },
                    {
                        data: depthSeepLong
                    },
                    {
                        data: shallowSleepLong
                    }]
                }
            )
        },
        error: function (errorMsg) {
            alert("請求數據失敗!");
        }
    });
}
