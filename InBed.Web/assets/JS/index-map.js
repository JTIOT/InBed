
$(function () {
    $("#map-container").height($(window).height());//110是header高度,40是 footer高度 
    $(window).resize(function () {
        $("#map-container").height($(window).height());//110是header高度,40是 footer高度 
    });
    var map = new Map();
    var url = parent.document.getElementById("txtContentBody").contentWindow.location.href;
    index = url.indexOf("flag");
    if (index != -1) {
        taggingMap()
    } else {
        initMap();
    }
});
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function unique(arr) {
    var res = [];
    var json = {};
    for (var i = 0; i < arr.length; i++) {
        if (!json[arr[i]]) {
            res.push(arr[i]);
            json[arr[i]] = 1;
        }
    }
    return res;
}

function contains(arr, obj) {
    var i = arr.length;
    var count = 0;
    while (i--) {
        if (arr[i] === obj) {
            count++;
        }
    }
    return count;
}

function goDetail(id) {
    var subWindow = window.parent.document.getElementById('txtContentBody');
    subWindow.src = '/Adm/Statistics/ElderStatistics/1095/1145/0?elderId=' + id;
}

function getBoundary() {
    var bdary = new BMap.Boundary();
    var name = provinceName;
    bdary.get(name, function (rs) {               //获取行政区域
        map.clearOverlays();                    //清除地图覆盖物
        var count = rs.boundaries.length;       //行政区域的点有多少个
        reDrawPoint(name);
        for (var i = 0; i < count; i++) {
            var ply = new BMap.Polygon(rs.boundaries[i], { strokeWeight: 1, strokeColor: "#000", fillOpacity: 0.1 });
            //建立多边形覆盖物
            map.addOverlay(ply);                //添加覆盖物
            //map.setViewport(ply.getPath());     //调整视野
            $(".p_name").html(provinceName + '：');
        }
    });
}

function reDrawPoint(pName) {
    var myGeo = new BMap.Geocoder();
    var count = 0;
    var serviceNum = [];
    $.ajax({
        type: "get",
        url: "/Control/GetMapPosition?province=" + pName,
        dataType: "json",
        success: function (rs) {
            var data = [];
            data.length = 0;
            for (var i = 0; i < rs.length; i++) {
                serviceNum.push(rs[i].FacilitatorName);
                var mapPoint = rs[i].MapPoint.split(',');
                var geoCoord = new Array();
                geoCoord[0] = mapPoint[0];
                geoCoord[1] = mapPoint[1];
                data.push({
                    geometry: {
                        type: 'Point',
                        coordinates: geoCoord
                    },
                    time: Math.random() * 10
                });
            }
            var dataSet = new mapv.DataSet(data);
            var options = {
                fillStyle: 'rgba(50, 250, 50, 0.5)',
                updateCallback: function (time) {
                    time = time.toFixed(2);
                    $('#time').html('时间' + time);
                },
                size: 1,
                draw: 'simple',
                animation: {
                    type: 'time',
                    stepsRange: {
                        start: 0,
                        end: 10
                    },
                    trails: 1,
                    duration: 2,
                }
            }
            var serviceCount = unique(serviceNum);
            var service1 = [];
            var strVar = "";
            $('.service_number1').children('.ser123').remove();
            for (var i = 0; i < serviceCount.length; i++) {
                var serNum = contains(serviceNum, serviceCount[i]);
                strVar += "<p class='ser123'>" + serviceCount[i] + "：<span>" + serNum + "人<\/span><\/p>\n";
            }
            $(".service_number1").append(strVar);
            if (serviceCount.length != 0 && serviceCount.length < 10) {
                $(".service_number").css("display", "block");
            } else {
                $(".service_number").css("display", "none");
            }
            var mapvLayer = new mapv.baiduMapLayer(map, dataSet, options);
            map.addEventListener("zoomend", function () {
                var u = this.getZoom();
                if (u >= 12) {
                    mapvLayer.update({
                        options: {
                            size: 3
                        }
                    });
                } else {
                    mapvLayer.update({
                        options: {
                            size: 1
                        }
                    });
                }
            });
        }
    });
}

function drawPoint() {
    var myGeo = new BMap.Geocoder();
    var count = 0;
    $.ajax({
        type: "get",
        url: "/Control/GetMapPosition?province=",
        dataType: "json",
        success: function (rs) {
            var data1 = [];
            for (var i = 0; i < rs.length; i++) {
                var mapPoint = rs[i].MapPoint.split(',');
                var geoCoord = new Array();
                geoCoord[0] = mapPoint[0];
                geoCoord[1] = mapPoint[1];
                data1.push({
                    geometry: {
                        type: 'Point',
                        coordinates: geoCoord
                    },
                    time: Math.random() * 10
                });
            }
            var dataSet = new mapv.DataSet(data1);
            var options = {
                fillStyle: 'rgba(50, 250, 50, 0.5)',
                //shadowColor: 'rgba(255, 250, 50, 0.5)',
                //shadowBlur: 3,
                updateCallback: function (time) {
                    time = time.toFixed(2);
                    $('#time').html('时间' + time);
                },
                size: 1,
                draw: 'simple',
                animation: {
                    type: 'time',
                    stepsRange: {
                        start: 0,
                        end: 10
                    },
                    trails: 1,
                    duration: 2,
                }
            }
            var mapvLayer = new mapv.baiduMapLayer(map, dataSet, options);
            map.addEventListener("zoomend", function () {
                var u = this.getZoom();
                if (u >= 12) {
                    mapvLayer.update({
                        options: {
                            size: 3,
                            animation: {
                                type: 'time',
                                stepsRange: {
                                    start: 0,
                                    end: 10
                                },
                                trails: 1,
                                duration: 2,
                            }
                        }
                    });
                } else {
                    mapvLayer.update({
                        options: {
                            size: 1,
                            animation: {
                                type: 'time',
                                stepsRange: {
                                    start: 0,
                                    end: 10
                                },
                                trails: 1,
                                duration: 2,
                            }
                        }
                    });
                }
            });
        }
    });
}

var provinceName;
//创建和初始化地图函数：
function initMap() {
    createMap();//创建地图
    //setMapStyle()//设置地图样式
    setMapEvent();//设置地图事件
    //drawPoint();
    /*setMapClick();*/
    elderMarks();
    //setMapScroll()
    //addMapControl();//向地图添加控件
    //addMapOverlay();//向地图添加覆盖物


}

function createMap() {
    map = new BMap.Map("map", { minZoom: 5, maxZoom: 16, enableMapClick: true });          // 创建地图实例  
    map.centerAndZoom(new BMap.Point(121.105, 23.692), 8);     // 初始化地图，设置中心点坐标和地图级别
    //map.centerAndZoom("台湾省", 15);
}

// 定义自定义覆盖物的构造函数  
function SquareOverlay(center, length, color) {
    this._center = center;
    this._length = length;
    this._color = color;
}
// 继承API的BMap.Overlay    
SquareOverlay.prototype = new BMap.Overlay();

// 实现初始化方法  
SquareOverlay.prototype.initialize = function (map) {
    // 保存map对象实例   
    this._map = map;
    // 创建div元素，作为自定义覆盖物的容器   
    var div = document.createElement("div");
    div.style.position = "absolute";
    // 可以根据参数设置元素外观   
    div.style.width = this._length + "px";
    div.style.height = this._length + "px";
    div.style.background = this._color;
    div.style.borderRadius = "60px";
    // 将div添加到覆盖物容器中   
    map.getPanes().markerPane.appendChild(div);
    // 保存div实例   
    this._div = div;
    // 需要将div元素作为方法的返回值，当调用该覆盖物的show、   
    // hide方法，或者对覆盖物进行移除时，API都将操作此元素。   
    return div;
}

// 实现绘制方法   
SquareOverlay.prototype.draw = function () {
    // 根据地理坐标转换为像素坐标，并设置给容器    
    var position = this._map.pointToOverlayPixel(this._center);
    this._div.style.left = position.x - this._length / 2 + "px";
    this._div.style.top = position.y - this._length / 2 + "px";
}

SquareOverlay.prototype.toggle = function () {
    if (this._div) {
        if (this._div.style.display == "") {
            this.hide();
        }
        else {
            this.show();
        }
    }
}

function elderMarks() {
    var elderArrData = (function () {
        var data;
        $.ajax({
            type: 'get',
            url: '/Elder/ElderDetails',
            dataType: 'json',
            async: false,
            success: function (result) {
                data = result;
            }
        });
        return data;
    })();
    console.log('elder data', elderArrData);
    for (let i = 0; i < elderArrData.length; i++) {
        const elderData = elderArrData[i]

        if (!elderData.MapPoint) continue;

        const elderID = elderData.Id;
        const age = elderData.Age;
        const name = elderData.name;
        const phone = elderData.homephone;
        const address = elderData.homeaddress;

        const sContent =
            "<div style='width:350px'>" +
            "<h4 style='margin:0 0 5px 0;padding:0.2em 0;cursor:pointer;display:inline-block ' onclick='goDetail(" + elderID + ")'>老人信息</h4>" +
            "<span style='font-size:20px;margin-left:10px'><i class='fa fa-long-arrow-left'></i></span>" +
            "</div>" +
            "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>姓名：" + name + "</p>" +
            "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>年齡：" + age + "</p>" +
            "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>電話：" + phone + "</p>" +
            "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>住址：" + address + "</p>" +
            "</div>";
        const mapPoint = elderData.MapPoint.split(",");
        const point = new BMap.Point(mapPoint[0], mapPoint[1]);
        //map.centerAndZoom(point, 1);
        var mySquare = new BMap.Marker(point);  // 创建标注
        //var mySquare = new SquareOverlay(point, 6, "green");
        map.addOverlay(mySquare);// 将标注添加到地图中
        //var infoWindow = new BMap.InfoWindow(sContent);
        mySquare.addEventListener("click", function () {
            this.openInfoWindow(new BMap.InfoWindow(sContent));
        });
    }
}

function createMap123() {
    var elderID = GetQueryString("elderId");
    var elderData = (function () {
        var data;
        $.ajax({
            type: 'get',
            url: '/Elder/ElderDeteail?id=' + elderID,
            dataType: 'json',
            async: false,
            success: function (result) {
                data = result;
            }
        });
        return data;
    })();
    var elderID = elderData.Id;
    var age = elderData.Age;
    var name = elderData.name;
    var phone = elderData.homephone;
    var address = elderData.homeaddress;
    var mapPoint = elderData.MapPoint.split(",");
    var sContent =
        "<div style='width:350px'>" +
        "<h4 style='margin:0 0 5px 0;padding:0.2em 0;cursor:pointer;display:inline-block ' onclick='goDetail(" + elderID + ")'>老人信息</h4>" +
        "<span style='font-size:20px;margin-left:10px'><i class='fa fa-long-arrow-left'></i></span>" +
        "</div>" +
        "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>姓名：" + name + "</p>" +
        "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>年齡：" + age + "</p>" +
        "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>電話：" + phone + "</p>" +
        "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>住址：" + address + "</p>" +
        "</div>";
    map = new BMap.Map("map", { minZoom: 5, maxZoom: 16, enableMapClick: false });          // 创建地图实例  
    var point = new BMap.Point(mapPoint[0], mapPoint[1]);
    map.centerAndZoom(point, 14);
    var marker = new BMap.Marker(point);  // 创建标注
    map.addOverlay(marker);               // 将标注添加到地图中
    marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
    var infoWindow = new BMap.InfoWindow(sContent);  // 创建信息窗口对象
    marker.openInfoWindow(infoWindow);
    marker.addEventListener("click", function () {
        this.openInfoWindow(infoWindow);
    });


}

function setMapStyle() {
    map.setMapStyle({
        'styleJson': [
            {
                'featureType': 'poi',
                'elementType': 'all',
                'stylers': {
                    'visibility': 'off'
                }
            },
            {
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
                    'color': '#a3a39d'
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
                    'color': '#d1d1c7',
                    'visibility': 'on'
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
}

function setMapEvent() {
    map.enableScrollWheelZoom();//启用地图滚轮放大缩小
    map.enableKeyboard();//启用键盘上下左右键移动地图
    map.enableDragging(); //启用地图拖拽事件
    map.enableDoubleClickZoom()//启用鼠标双击放大
}

function setMapClick() {
    map.addEventListener("click", function (evt) {
        var myGeo = new BMap.Geocoder();
        myGeo.getLocation(new BMap.Point(evt.point.lng, evt.point.lat), function (result) {
            provinceName = result.addressComponents.province;
            getBoundary();
        });
    });
}

function taggingMap() {
    createMap123();//创建地图
    setMapStyle()//设置地图样式
    setMapEvent();//设置地图事件
    drawPoint();
    //setMapClick();
    //setMapScroll()
    //addMapControl();//向地图添加控件
    //addMapOverlay();//向地图添加覆盖物

}
//initMap();
var map;


