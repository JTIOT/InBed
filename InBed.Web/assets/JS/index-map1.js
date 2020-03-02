
var mheight=document.body.scrollHeight-60;
$("#map").css("height",mheight+'px');
var map = new BMap.Map("map",{minZoom:5,maxZoom:15});          // 创建地图实例  
    //var point = new BMap.Point(116.404, 39.915);             // 创建点坐标  
    map.centerAndZoom(new BMap.Point(105.000, 40.000), 5);     // 初始化地图，设置中心点坐标和地图级别  
    map.enableScrollWheelZoom(true);
    var provinceName;
    map.addEventListener("click", function (evt) {
        var myGeo=new BMap.Geocoder();

        myGeo.getLocation(new BMap.Point(evt.point.lng,evt.point.lat),function(result){

            provinceName=result.addressComponents.province;

            getBoundary();
            
            //alert(provinceName);
        });
        
    });
    function getBoundary() {
        var bdary = new BMap.Boundary();
        var name = provinceName;
        //console.log(name);
        bdary.get(name, function(rs){               //获取行政区域
            map.clearOverlays();                    //清除地图覆盖物
            var count = rs.boundaries.length;       //行政区域的点有多少个
            
            for(var i = 0; i < count; i++){
                var ply = new BMap.Polygon(rs.boundaries[i], {strokeWeight: 1, strokeColor: "#000" , fillOpacity: 0.1}); 
                //建立多边形覆盖物
                map.addOverlay(ply);                //添加覆盖物
                map.setViewport(ply.getPath());     //调整视野
                $(".service_number").css("display","block");
                $(".p_name").html(provinceName);
            }
        });
    }
    function drawPoint() {
        var myGeo = new BMap.Geocoder();
        $.ajax({
            type: "get",
            url: "/Control/GetMapPosition",
            dataType: "json",
            success: function (rs) {
                var pointData = [];
                var count = 0;
                var l = rs.length;
                for (var i = 0; i < l; i++) {
                    var address = rs[i].Homeaddress;
                    var geoCoord = new Array();
                    count++;
                    myGeo.getPoint(address, function (point) {
                        if (point) {
                            geoCoord[0]=point.lng;
                            geoCoord[1]=point.lat;
                            pointData.push({
                                geometry: {
                                    type: 'Point',
                                    coordinates: geoCoord
                                },
                                time: Math.random() * 10
                            })
                            console.log(geoCoord);
                            console.log(pointData);
                        }
                    })
                }
                var dataSet = new mapv.DataSet(pointData);
                var options = {
                    fillStyle: 'rgba(50, 250, 50, 0.5)',
                    //shadowColor: 'rgba(255, 250, 50, 0.5)',
                    //shadowBlur: 3,
                    updateCallback: function (time) {
                        time = time.toFixed(2);
                        $('#time').html('时间' + time);
                    },
                    size: 2,
                    draw: 'simple',
                    animation: {
                        type: 'time',
                        stepsRange: {
                            start: 0,
                            end: 10
                        },
                        trails: 1,
                        duration: 6,
                    }
                }
                var mapvLayer = new mapv.baiduMapLayer(map, dataSet, options);
            }
        });
    }
    drawPoint();
    map.setMapStyle({
        'styleJson': [{
            'featureType': 'water',
            'elementType': 'all',
            'stylers': {
                'color': '#054561'
            }
        }, {
            'featureType': 'land',
            'elementType': 'geometry',
            'stylers': {
                'color': '#091934'
            }
        }, {
            'featureType': 'highway',
            'elementType': 'all',
            'stylers': {
                'visibility': 'off'
            }
        }, {
            'featureType': 'arterial',
            'elementType': 'geometry.fill',
            'stylers': {
                'color': '#000000'
            }
        }, {
            'featureType': 'arterial',
            'elementType': 'geometry.stroke',
            'stylers': {
                'color': '#0b3d51'
            }
        }, {
            'featureType': 'local',
            'elementType': 'geometry',
            'stylers': {
                'color': '#000000'
            }
        }, {
            'featureType': 'railway',
            'elementType': 'geometry.fill',
            'stylers': {
                'color': '#000000'
            }
        }, {
            'featureType': 'railway',
            'elementType': 'geometry.stroke',
            'stylers': {
                'color': '#08304b'
            }
        }, {
            'featureType': 'subway',
            'elementType': 'geometry',
            'stylers': {
                'lightness': -70
            }
        }, {
            'featureType': 'building',
            'elementType': 'geometry.fill',
            'stylers': {
                'color': '#000000'
            }
        }, {
            'featureType': 'all',
            'elementType': 'labels.text.fill',
            'stylers': {
                'color': '#857f7f'
            }
        }, {
            'featureType': 'all',
            'elementType': 'labels.text.stroke',
            'stylers': {
                'color': '#000000'
            }
        }, {
            'featureType': 'building',
            'elementType': 'geometry',
            'stylers': {
                'color': '#022338'
            }
        }, {
            'featureType': 'green',
            'elementType': 'geometry',
            'stylers': {
                'color': '#062032'
            }
        }, {
            'featureType': 'boundary',
            'elementType': 'all',
            'stylers': {
                'color': '#0374AB'
            }
        }, {
            'featureType': 'manmade',
            'elementType': 'all',
            'stylers': {
                'color': '#022338'          
            }
        }, {
            'featureType': 'label',
            'elementType': 'all',
            'stylers': {
                'visibility': 'off'
            }
        }]
    });
    //var data = [];
    //$.get('../../../../../assets/point.json', function (rs) {
    //    for (var i = 0; i < rs[0].length; i++) {
    //        var geoCoord = rs[0][i].geoCoord;
    //        data.push({
    //            geometry: {
    //                type: 'Point',
    //                coordinates: geoCoord
    //            },
    //            time: Math.random() * 10
    //        });
    //    }
    //    console.log(data)
    //    var dataSet = new mapv.DataSet(data);
    //    var options = {
    //        fillStyle: 'rgba(50, 250, 50, 0.6)',
    //        //shadowColor: 'rgba(255, 250, 50, 0.5)',
    //        //shadowBlur: 3,
    //        updateCallback: function (time) {
    //            time = time.toFixed(2);
    //            $('#time').html('时间' + time);
    //        },
    //        size: 1,
    //        draw: 'simple',
    //        animation: {
    //            type: 'time',
    //            stepsRange: {
    //                start: 0,
    //                end: 10
    //            },
    //            trails: 1,
    //            duration: 6,
    //        }
    //    }
    //    var mapvLayer = new mapv.baiduMapLayer(map, dataSet, options);
    //});
    