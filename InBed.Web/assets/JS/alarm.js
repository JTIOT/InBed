
function drawPoint(echarts) {
    var myPointChart1 = echarts.init(document.getElementById('echart4'));
    var elderName = $('#searchElder').val();
    var searchDate = $('#searchDate').val();
    myPointChart1.setOption( option = {
            title: {
                x: 'left',
                padding: 15,
                textStyle: {
                    fontSize: 18,
                    color: '#fff'
                }
            },
            grid: {
                left: '3%',
                right: '7%',
                bottom: '3%',
                containLabel: true
            },
            tooltip : {
                showDelay: 0,
                formatter: function (params) {
                    if (params.componentSubType == "effectScatter") {
                        return '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:' + params.color + '"></span>'
                        + params.seriesName + '—未處理 :<br/>'
                        + new Date(params.value[0]).Format("yyyy-MM-dd") + ' <br/>'
                        + timeTamp(params.value[1])
                    } else {
                        return '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:' + params.color + '"></span>'
                        + params.seriesName + ' :<br/>'
                        + new Date(params.value[0]).Format("yyyy-MM-dd") + ' <br/>'
                        + timeTamp(params.value[1])
                    }
                },
                axisPointer:{
                    type: 'cross',
                    label: {
                        formatter: function (params) {
                            var pointerValue = '';
                            if (params.value > 86400) {
                                pointerValue = (new Date(params.value).Format("yyyy-MM-dd")).substring(5);
                            } else {
                                pointerValue = timeTamp(params.value)
                            }
                            return pointerValue
                        }
                    },
                    lineStyle: {
                        type : 'dashed',
                        width : 1
                    }
                }
            },
            legend: {
                data: [],
                left: 'center',
                textStyle: {
                    color: '#fff',
                    fontSize:15
                }
            },
            xAxis : [
                {
                    type: 'value',
                    scale:true,
                    axisLabel: {
                        textStyle: {
                            color: '#fff'
                        },
                        formatter: function (params) {
                            return new Date(params).Format("yyyy-MM-dd")
                        },
                    },
                    splitLine: {
                        show: false
                    },
                    splitNumber: 6
                }
            ],
            yAxis : [
                {
                    type: 'value',
                    scale: true,
                    min: 0,
                    max: 86400,
                    axisLabel: {
                        textStyle: {
                            color: '#fff'
                        },
                        formatter: function (v) {
                            return timeTamp(v)
                        },
                    },
                    splitLine: {
                        show: false
                    },
                    splitNumber: 6
                }
            ],
            series : []
        }
    );
    $.ajax({
        type: "get",
        url: '/Adm/Alarm/GetCharData/1097/1116/0?orderBy=Id&orderDir=desc&start=0&length=max&ElderName=' + elderName + '&searchDate=' + searchDate + '',
        dataType: "json",
        success: function (result) {
            if (result.data!=null) {
                var serie = [],
                alarmData = [],
                alarmTypeName = [],
                alarmName = [],
                alarmTime = [],
                alarmDate = [],
                alarmColor =[],
                isHandle = [],
                unHandle = [],
                data = [];
                myPointChart1.setOption(option = {
                    title: {
                        text:  '歷 史 告 警 — 姓 名：' + elderName
                    },
                    series: function () {
                        for (var i = 0, l = result.data.length; i < l; i++) {
                            if (result.data[i].IsHandle != 0) {
                                alarmTypeName = result.data[i].AlarmTypeName;
                                alarmName.push(result.data[i].AlarmTypeName);
                                alarmDate = (new Date(result.data[i].AlarmTime.substring(0, 10))).getTime();  
                                alarmTime = toHourMinute(result.data[i].AlarmTime.substring(11));
                                alarmColor = result.data[i].AlarmColor;
                                var item1 = {
                                    name: alarmTypeName,
                                    type: 'scatter',
                                    rippleEffect: {
                                        scale: 4,
                                        brushType: 'stroke'
                                    },
                                    itemStyle: {
                                        normal: {
                                            shadowBlur: 10,
                                            shadowColor: 'rgba(120, 36, 50, 0.5)',
                                            shadowOffsetY: 5,
                                            color: alarmColor
                                        }
                                    },
                                    data: [[alarmDate, alarmTime]]
                                }
                                serie.push(item1);
                            } else {
                                alarmTypeName = result.data[i].AlarmTypeName;
                                alarmName.push(result.data[i].AlarmTypeName);
                                alarmDate = (new Date(result.data[i].AlarmTime.substring(0, 10))).getTime(); 
                                alarmTime = toHourMinute(result.data[i].AlarmTime.substring(11));
                                alarmColor = result.data[i].AlarmColor;
                                var item2 = {
                                    name: alarmTypeName,
                                    type: 'effectScatter',
                                    rippleEffect: {
                                        scale: 4,
                                        brushType: 'stroke'
                                    },
                                    itemStyle: {
                                        normal: {
                                            shadowBlur: 10,
                                            shadowColor: 'rgba(0, 0, 0, 0.5)',
                                            shadowOffsetY: 5,
                                            color: alarmColor
                                        }
                                    },
                                    data: [[alarmDate, alarmTime]]
                                }
                                serie.unshift(item2);
                            }
                        }
                        return serie
                    }(),
                    legend: {
                        data: alarmName.unique()
                    },
                })
            }
        }
    })
}

