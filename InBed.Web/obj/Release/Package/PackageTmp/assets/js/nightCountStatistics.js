

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
var elderID = GetQueryString("elderId");

function drawNightCountChart(echarts) {
    var myNightCountChart = echarts.init(document.getElementById('echartNightCount'));
    var searchElder = $('#searchElder').val();
    var searchDate = $('#searchDate').val();
    myNightCountChart.setOption(
        option = {
            title : {
                x: 'center',
                padding: 50,
                textStyle: {
                    fontSize: 16,
                    color: '#fff'
                },
                text: '起 夜 次 數 統 計'
                
            },
            tooltip : {
                trigger: 'axis',
                axisPointer : {
                    type : 'shadow'
                },
            },
            xAxis : [
                {
                    type : 'category',
                    data : [],
                    splitLine:{
                        show:false
                    },
                    axisLabel: {
                        textStyle: {
                            color: '#fff'
                        }
                    },
                }
            ],
            yAxis : [
                {
                    type : 'value',
                    boundaryGap: [0, 0.1],
                    splitLine:{
                        show:false
                    },
                    axisLabel: {
                        textStyle: {
                            color: '#fff'
                        }
                    },
                }
            ],
            grid: {
                borderWidth:0
            },
            series : [
                {
                    name:'起夜次數',
                    type:'bar',
                    stack: 'sum',
                    itemStyle : { 
                        normal: {
                            label : {
                                show: true, position: 'inside',
                                textStyle: {
                                    color: '#fff',
                                    baseline: 'top',
                                }

                            },
                        },
                    },
                    data:[]
                },
            ]
        }
    );
    $.ajax({
        type: "get",
        async: true,
        url: "/Statistics/GetHistoryNightCount?ElderName=" + searchElder + "&searchDateTime=" + searchDate,
        dataType: "json",
        success: function (result) {
            console.log(result);
            if (result.msg) {
                alert('查詢訊息量過大，請減少查詢日期間隔');
            };
            var nightCount = [];
            var times = [];
            for (var i = 0, l = result.data.length; i < l; i++) {
                nightCount.unshift(result.data[i].NightCount);
                times.unshift(dateStart(result.data[i].AnalysisDate))
            }
            myNightCountChart.setOption(
                option = {
                    title: {
                        text: '起 夜 次 數 統 計'
                    },
                    xAxis: [{
                        data: times,
                    }],
                    series: [{
                        data: nightCount
                    }]
                }
            )
        },
        error: function (errorMsg) {
            //请求失败时执行该函数
            alert("請求數據失敗!");
        }
    });
}
