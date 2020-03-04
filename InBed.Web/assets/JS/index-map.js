
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
    //styleMap();
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

function styleMap() {
    map.setMapStyle({
        'styleJson': [{
            "featureType": "land",
            "elementType": "geometry",
            "stylers": {
                "color": "#f5f6f7ff"
            }
        }, {
            "featureType": "water",
            "elementType": "geometry",
            "stylers": {
                "color": "#c4d7f5ff"
            }
        }, {
            "featureType": "green",
            "elementType": "geometry",
            "stylers": {
                "color": "#dcf2d5ff"
            }
        }, {
            "featureType": "highway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#ffe59eff"
            }
        }, {
            "featureType": "highway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#f5d48cff"
            }
        }, {
            "featureType": "nationalway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#fff6ccff"
            }
        }, {
            "featureType": "provincialway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#fff6ccff"
            }
        }, {
            "featureType": "cityhighway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#fff6ccff"
            }
        }, {
            "featureType": "arterial",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#fff6ccff"
            }
        }, {
            "featureType": "nationalway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#f2dc9dff"
            }
        }, {
            "featureType": "provincialway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#f2dc9dff"
            }
        }, {
            "featureType": "cityhighway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#f2dc9dff"
            }
        }, {
            "featureType": "arterial",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#f2dc9dff"
            }
        }, {
            "featureType": "building",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#e6ebf0ff"
            }
        }, {
            "featureType": "building",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#d8e2ebff"
            }
        }, {
            "featureType": "tertiaryway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "tertiaryway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#dfe4ebff"
            }
        }, {
            "featureType": "fourlevelway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "fourlevelway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#dfe4ebff"
            }
        }, {
            "featureType": "local",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "local",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#dfe4ebff"
            }
        }, {
            "featureType": "scenicspotsway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "scenicspotsway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#dfe4ebff"
            }
        }, {
            "featureType": "universityway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#dfe4ebff"
            }
        }, {
            "featureType": "universityway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "vacationway",
            "elementType": "geometry.stroke",
            "stylers": {
                "color": "#dfe4ebff"
            }
        }, {
            "featureType": "vacationway",
            "elementType": "geometry.fill",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "town",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 18
            }
        }, {
            "featureType": "town",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "town",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "highway",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#c0792dff"
            }
        }, {
            "featureType": "highway",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "nationalway",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#c0792dff"
            }
        }, {
            "featureType": "nationalway",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff60"
            }
        }, {
            "featureType": "provincialway",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#c0792dff"
            }
        }, {
            "featureType": "provincialway",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "cityhighway",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#c0792dff"
            }
        }, {
            "featureType": "cityhighway",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "arterial",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "arterial",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#c0792dff"
            }
        }, {
            "featureType": "arterial",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 24
            }
        }, {
            "featureType": "cityhighway",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 24
            }
        }, {
            "featureType": "provincialway",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 24
            }
        }, {
            "featureType": "nationalway",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 24
            }
        }, {
            "featureType": "highway",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 24
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "on"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#000000ff"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "companylabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "companylabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "companylabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "lifeservicelabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "lifeservicelabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "lifeservicelabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "carservicelabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "carservicelabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "carservicelabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "transportationlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "on"
            }
        }, {
            "featureType": "transportationlabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "transportationlabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "financelabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "financelabel",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "financelabel",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "tertiaryway",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "tertiaryway",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "tertiaryway",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "fourlevelway",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "fourlevelway",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "fourlevelway",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "local",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "local",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "local",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "companylabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "lifeservicelabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "carservicelabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "financelabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "manmade",
            "elementType": "geometry",
            "stylers": {
                "color": "#f5f6f7ff"
            }
        }, {
            "featureType": "subway",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "12"
            }
        }, {
            "featureType": "subway",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "13"
            }
        }, {
            "featureType": "subway",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "12"
            }
        }, {
            "featureType": "subway",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "13"
            }
        }, {
            "featureType": "subwaylabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,13",
                "level": "13"
            }
        }, {
            "featureType": "subwaylabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,13",
                "level": "13"
            }
        }, {
            "featureType": "subwaylabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,13",
                "level": "13"
            }
        }, {
            "featureType": "railway",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "scenicspotslabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "10"
            }
        }, {
            "featureType": "scenicspotslabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "11"
            }
        }, {
            "featureType": "scenicspotslabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "12"
            }
        }, {
            "featureType": "scenicspotslabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "13"
            }
        }, {
            "featureType": "scenicspotslabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "14"
            }
        }, {
            "featureType": "scenicspotslabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "15"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "10"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "11"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "12"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "13"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "14"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "15"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "10"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "11"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "12"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "13"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "14"
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "10,15",
                "level": "15"
            }
        }, {
            "featureType": "district",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "district",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "city",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "city",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "city",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "on"
            }
        }, {
            "featureType": "country",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "country",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "continent",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#a77726ff"
            }
        }, {
            "featureType": "continent",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "medicallabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "13"
            }
        }, {
            "featureType": "medicallabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "14"
            }
        }, {
            "featureType": "medicallabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "15"
            }
        }, {
            "featureType": "medicallabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "16"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "13"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "14"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "15"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "16"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "13"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "14"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "15"
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "16"
            }
        }, {
            "featureType": "entertainmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "12"
            }
        }, {
            "featureType": "entertainmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "13"
            }
        }, {
            "featureType": "entertainmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "14"
            }
        }, {
            "featureType": "entertainmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "15"
            }
        }, {
            "featureType": "entertainmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "16"
            }
        }, {
            "featureType": "entertainmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "17"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "12"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "13"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "14"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "15"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "16"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "17"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "12"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "13"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "14"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "15"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "16"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,17",
                "level": "17"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "estatelabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "13"
            }
        }, {
            "featureType": "estatelabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "14"
            }
        }, {
            "featureType": "estatelabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "15"
            }
        }, {
            "featureType": "estatelabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "16"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "13"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "14"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "15"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "16"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "13"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "14"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "15"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,16",
                "level": "16"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "businesstowerlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "12"
            }
        }, {
            "featureType": "businesstowerlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "13"
            }
        }, {
            "featureType": "businesstowerlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "14"
            }
        }, {
            "featureType": "businesstowerlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "15"
            }
        }, {
            "featureType": "businesstowerlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "16"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "12"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "13"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "14"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "15"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "16"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "12"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "13"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "14"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "15"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,16",
                "level": "16"
            }
        }, {
            "featureType": "governmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "13"
            }
        }, {
            "featureType": "governmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "14"
            }
        }, {
            "featureType": "governmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "15"
            }
        }, {
            "featureType": "governmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "16"
            }
        }, {
            "featureType": "governmentlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "17"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "13"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "14"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "15"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "16"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "17"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "13"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "14"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "15"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "16"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,17",
                "level": "17"
            }
        }, {
            "featureType": "restaurantlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "13"
            }
        }, {
            "featureType": "restaurantlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "14"
            }
        }, {
            "featureType": "restaurantlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "15"
            }
        }, {
            "featureType": "restaurantlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "16"
            }
        }, {
            "featureType": "restaurantlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "17"
            }
        }, {
            "featureType": "restaurantlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "18"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "13"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "14"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "15"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "16"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "17"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "18"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "13"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "14"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "15"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "16"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "17"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "13,18",
                "level": "18"
            }
        }, {
            "featureType": "hotellabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "14"
            }
        }, {
            "featureType": "hotellabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "15"
            }
        }, {
            "featureType": "hotellabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "16"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 22,
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "14"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 22,
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "15"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 22,
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "16"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "14"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "15"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "16"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "14"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "15"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "14,16",
                "level": "16"
            }
        }, {
            "featureType": "shoppinglabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "11"
            }
        }, {
            "featureType": "shoppinglabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "12"
            }
        }, {
            "featureType": "shoppinglabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "13"
            }
        }, {
            "featureType": "shoppinglabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "14"
            }
        }, {
            "featureType": "shoppinglabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "15"
            }
        }, {
            "featureType": "shoppinglabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "16"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "11"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "12"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "13"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "14"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "15"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "16"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "11"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "12"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "13"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "14"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "15"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,16",
                "level": "16"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "companylabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 24
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "medicallabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 33
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "scenicspotslabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "airportlabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "water",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "water",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffffff"
            }
        }, {
            "featureType": "manmade",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#9ca0a3ff"
            }
        }, {
            "featureType": "manmade",
            "elementType": "labels.text.stroke",
            "stylers": {
                "color": "#ffffff00"
            }
        }, {
            "featureType": "education",
            "elementType": "labels",
            "stylers": {
                "visibility": "on"
            }
        }, {
            "featureType": "transportationlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "12"
            }
        }, {
            "featureType": "transportationlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "13"
            }
        }, {
            "featureType": "transportationlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "12"
            }
        }, {
            "featureType": "transportationlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "13"
            }
        }, {
            "featureType": "transportationlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "12"
            }
        }, {
            "featureType": "transportationlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "12,13",
                "level": "13"
            }
        }, {
            "featureType": "educationlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "11"
            }
        }, {
            "featureType": "educationlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "12"
            }
        }, {
            "featureType": "educationlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "13"
            }
        }, {
            "featureType": "educationlabel",
            "stylers": {
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "14"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "11"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "12"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "13"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "14"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "11"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "12"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "13"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off",
                "curZoomRegionId": "0",
                "curZoomRegion": "11,14",
                "level": "14"
            }
        }, {
            "featureType": "transportationlabel",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "manmade",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "scenicspots",
            "elementType": "labels.text.fill",
            "stylers": {
                "color": "#ab76b6ff"
            }
        }, {
            "featureType": "scenicspots",
            "elementType": "labels.text",
            "stylers": {
                "fontsize": 23
            }
        }, {
            "featureType": "airportlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "educationlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "airportlabel",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "entertainmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "estatelabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "businesstowerlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "governmentlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "restaurantlabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "hotellabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "shoppinglabel",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "arterial",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "tertiaryway",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "tertiaryway",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "fourlevelway",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "fourlevelway",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "local",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "local",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "scenicspotsway",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "universityway",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "vacationway",
            "elementType": "geometry",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "roadarrow",
            "elementType": "labels.icon",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "village",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }, {
            "featureType": "town",
            "elementType": "labels",
            "stylers": {
                "visibility": "off"
            }
        }]});
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


