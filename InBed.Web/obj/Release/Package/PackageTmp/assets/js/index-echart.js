
function drawEcharts(echarts) {
drawBar1(echarts);
drawBar2(echarts);
drawPie(echarts);
}

function drawBar1(echarts) {
    var myBarChart1 = echarts.init(document.getElementById('echart1'));
    $.ajax({
        type: "get",
        async: true,
        url: "/Control/GetPlaceNameeNumber",
        dataType: "json",
        success: function (result) {
            var placeName = [];
            var number = [];
            for (var i = 0; i <result.length; i++) {
                placeName.unshift(result[i].PlaceNamee);
                number.unshift(result[i].Number);
            }
            myBarChart1.setOption({
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        type: 'shadow'
                    },
                    showDelay: 0,
                },
                calculable: true,
                xAxis: [
                    {
                        type: 'value',
                        splitLine: {
                            show: false
                        },
                        axisLine: {
                            lineStyle: {
                                width: '0'
                            }
                        }
                    }
                ],
                yAxis: [
                    {
                        type: 'category',
                        data: placeName,
                        splitLine: { show: false },
                        //splitArea : {show : false},
                        offset: -4,
                        axisLabel: {
                            show: true,
                            textStyle: {
                                color: '#fff'
                            }
                        },
                        axisLine: {
                            lineStyle: {
                                width: '0'
                            }
                        },
                    }
                ],
                grid: {
                    top: 200,
                    left: 200,
                },
                series: [
                    {
                        name: '自費用戶',
                        type: 'bar',
                        stack: '总量',
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true, position: 'right'
                                },
                                color: function (params) {

                                    var colorList = ['rgb(220,20,60)', 'rgb(46,163,221)', 'rgb(92,40,133)', 'rgb(104,90,78)', 'rgb(185,31,44)', 'rgb(42,44,135)', 'rgb(245,137,44)', 'rgb(139,0,139)', 'rgb(72,61,139)', 'rgb(30,144,255)',
                                                     'rgb(0,206,209)', 'rgb(144,238,144)', 'rgb(218,165,32)', 'rgb(255,140,0)', 'rgb(255,99,71)', 'rgb(128,0,0)', 'rgb(199,21,133)', 'rgb(72,209,204)', 'rgb(135,206,250)', 'rgb(123,104,238)',
                                                     'rgb(0,255,127)', 'rgb(240,230,140)', 'rgb(210,105,30)', 'rgb(124,252,0)', 'rgb(233,150,122)', 'rgb(128,0,128)', 'rgb(95,158,160)', 'rgb(255,215,0)', 'rgb(255,127,80)', 'rgb(0,139,139)'
                                    ];

                                    return colorList[params.dataIndex];
                                }
                            },
                            //鼠标悬停时
                            emphasis: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.1)'
                            }
                        },
                        data: number,
                        barWidth: 20,
                        barGap:'10%'
                    },
                    //{
                    //    name: '公益用戶',
                    //    type: 'bar',
                    //    stack: '总量',
                    //    itemStyle: {
                    //        normal: {
                    //            label: {
                    //                show: true, position: 'right'
                    //            },
                    //            color: 'rgba(255,255,255,0.9)'
                    //        }
                    //    },
                    //    data: [2000, 3000, 4000, 2000, 1000, 3000],
                    //    barWidth: 20,
                    //}
                ],
                grid: {
                    borderWidth: 0
                },
            });
        },
    })
}


function drawPie(echarts) {
    var myPieChart = echarts.init(document.getElementById('echart2'));

    $.ajax({
        type: "get",
        async: true,
        url: "/Control/GetAgeGroup",
        dataType: "json",
        success: function (result) {
            myPieChart.setOption(
                option = {
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} ({d}%)"
                    },
                    color: [ '#2EA3DD','#fa8564', '#9972b5', '#1fb5ac', '#FDB45C'],
                    //calculable : true,
                    series: [
                        {
                            name: '年齡分佈',
                            type: 'pie',
                            radius: ['50%', '70%'],
                            avoidLabelOverlap: false,
                            label: {
                                normal: {
                                    show: false,
                                    position: 'center'
                                },
                                emphasis: {
                                    show: true,
                                    textStyle: {
                                        fontSize: '19',
                                        fontWeight: 'bold'
                                    }
                                }
                            },
                            labelLine: {
                                normal: {
                                    show: false
                                }
                            },
                            data: result
                        }
                    ]
                }
            );
            $('.elder-dis').show();
        }
    })
}
function drawBar2(echarts) {
    var myBarChart2 = echarts.init(document.getElementById('echart3'));
    
    $.ajax({
        type: "get",
        async: true,
        url: "/Control/GetPlaceOnlineNumber",
        dataType: "json",
        success: function (result) {
            var placeName = [];
            var offline = [];
            var online = [];
            for (var i = 0; i < result.length; i++) {
                placeName.unshift(result[i].PlaceNamee);
                var calculate = result[i].SumNumber - result[i].OnlineNumber;
                offline.unshift(calculate);
                online.unshift(result[i].OnlineNumber);
            }
            myBarChart2.setOption(
                option = {
                    title : {
                        x: 'center',
                        padding: 50,
                        textStyle: {
                            fontSize: 16,
                            color: '#fff'
                        }
                        //text: '設備在線狀況（在線/離線）'
                
                    },
                    tooltip : {
                        trigger: 'axis',
                        axisPointer : {            // 坐标轴指示器，坐标轴触发有效
                            type : 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                        },
                    },
                    calculable : true,
                    xAxis : [
                        {
                            type : 'category',
                            data: placeName,
                            inverse: true,
                            axisLabel: {
                                show: true,
                                textStyle: {
                                    color: '#fff'
                                }
                            },
                            axisLine: {
                                lineStyle: {
                                    width:'0'
                                }
                            }
                        }
                    ],
                    yAxis : [
                        {
                            type: 'value',
                            show: false,
                            boundaryGap: [0, 0.1],
                            splitLine:{
                                show:false
                            },
                            axisLine: {
                                lineStyle: {
                                    width:'0'
                                }
                            }
                        }
                    ],
                    grid: {
                        borderWidth:0
                    },
                    series : [
                        {
                            name:'在線',
                            type:'bar',
                            stack: '总量',
                            barWidth:35,
                            itemStyle : { 
                                normal: {
                                    label : {
                                        show: true, position: 'inside',
                                        textStyle: {
                                            color: '#fff',
                                            baseline: 'top',
                                        }

                                    },
                                    color: function (params) {

                                        var colorList = ['rgb(220,20,60)', 'rgb(46,163,221)', 'rgb(92,40,133)', 'rgb(104,90,78)', 'rgb(185,31,44)', 'rgb(42,44,135)', 'rgb(245,137,44)', 'rgb(139,0,139)', 'rgb(72,61,139)', 'rgb(30,144,255)',
                                                         'rgb(0,206,209)', 'rgb(144,238,144)', 'rgb(218,165,32)', 'rgb(255,140,0)', 'rgb(255,99,71)', 'rgb(128,0,0)', 'rgb(199,21,133)', 'rgb(72,209,204)', 'rgb(135,206,250)', 'rgb(123,104,238)',
                                                         'rgb(0,255,127)', 'rgb(240,230,140)', 'rgb(210,105,30)', 'rgb(124,252,0)', 'rgb(233,150,122)', 'rgb(128,0,128)', 'rgb(95,158,160)', 'rgb(255,215,0)', 'rgb(255,127,80)', 'rgb(0,139,139)'
                                        ];

                                        return colorList[params.dataIndex];
                                    }
                                },
                            },
                            data: online
                        },
                        {
                            name:'離線',
                            type:'bar',
                            stack: '总量',
                            itemStyle: {
                                normal: {
                                    color: '#2E2E2E',
                                    barBorderColor: '#2E2E2E',
                                    barBorderWidth: 0,
                                    barBorderRadius:0,
                                }
                            },
                            data: offline
                        }
                    ]
                }
            );
        }
    })
}

drawEcharts(echarts)