
$('#icon_validate').validate({
    errorElement: 'span',
    errorClass: 'error',
    focusInvalid: false,
    ignore: "",
    rules: {
        formfield1: {
            minlength: 2,
            required: true
        },
        formfield2: {
            minlength: 2,
            required: true
        },
        formfield3: {
            required: true,
            identityCard: true
        },
        formfield4: {
            minlength: 8,
            required: true
        },
        formfield5: {
            minlength: 2,
            required: true
        },
        formfield6: {
            required: true,
            telephone: true
        },
        formfield7: {
            minlength: 2,
            required: true
        },
    },

    invalidHandler: function (event, validator) {
        //display error alert on form submit

    },

    errorPlacement: function (error, element) { // render error placement for each input type
        var icon = $(element).parent().parent('.form-group').find('i');
        var parent = $(element).parent().parent('.form-group');
        icon.removeClass('fa fa-check').addClass('fa fa-times');
        parent.removeClass('has-success').addClass('has-error');
    },

    highlight: function (element) { // hightlight error inputs
        var parent = $(element).parent().parent('.form-group');
        parent.removeClass('has-success').addClass('has-error');
    },

    unhighlight: function (element) { // revert the change done by hightlight

    },

    success: function (label, element) {
        var icon = $(element).parent().parent('.form-group').find('i');
        var parent = $(element).parent().parent('.form-group');
        icon.removeClass("fa fa-times").addClass('fa fa-check');
        parent.removeClass('has-error').addClass('has-success');
    },

    submitHandler: function (form) {
        formEdit()
    }
});
//var map = new BMap.Map();
var localSearch = new BMap.LocalSearch(new BMap.Map());

function searchByStationName() {
    keyword = document.getElementById("formfield7").value;
    localSearch.setSearchCompleteCallback(function (searchResult) {
        var poi = searchResult.getPoi(0);
        document.getElementById("mapPoint").value = poi.point.lng + "," + poi.point.lat; //获取经度和纬度，将结果显示在文本框中
    });
    localSearch.search(keyword);
}



var radioCount = 0;

if($(".radio").attr("checked")=="checked"){
       radioCount=1; 
}
$(".radio").click(function(){
   if(radioCount == 1){
        $(this).removeAttr("checked");
        $(this).siblings().removeAttr("disabled");
        radioCount=0;
   }else {
        $(this).attr("checked","checked");
        $(this).siblings().removeAttr("checked");
        $(this).siblings().attr("disabled","disabled");
        $(this).siblings("input[type='text']").val("");
        radioCount=1;
    }
});
function fun(){
    obj = document.getElementsByName("expose");
    check_val = [];
    for(k in obj){
        if(obj[k].checked)
            check_val.push(obj[k].value);
    }
    //console.log(check_val);
}
$("#formfield7").blur(function () {
    searchByStationName();
});
   
function formEdit() {
    var id = $("#Id").val();
    var name = $(".oldman-name").val();
    var sex = $("input[name='sex']:checked").val();
    var birthday = $(".birthday").val();
    var number = $(".identity-card").val();
    var homeaddress = $(".homeaddress").val();
    var homephone = $(".home-phone").val();
    var contacts = $(".contact-name").val();
    var c_telephone = $(".contact-phone").val();
    var household = $("input[name='household']:checked").val();
    var nation = $("input[name='nation']:checked").val();
    var blood_type = $("input[name='blood-type']:checked").val();
    var education = $("input[name='education']:checked").val();
    var occupation = $("input[name='professional']:checked").val();
    var marriageStatus = $("input[name='marriage']:checked").val();
    var dispensedmode = $("input[name='medical-payment']:checked").val();
    var H_allergydrug = $("input[name='allergy']:checked").val();
    var expose = $("input[name='expose']:checked").val(fun());
    var H_disease = $("input[name='disease']").val();
    var disease_time = $("input[name='disease-time']").val();
    var is_ops = $("input[name='operation']").val();
    var ops_disease = $(".operation-name").val();
    var ops_data = $(".operation-time").val();
    var injury = $("input[name='injury']").val();
    var injury_name = $(".injury-name").val();
    var injury_time = $(".injury-time").val();
    var bloodtrans = $("input[name='blood-trans']").val();
    var bloodtrans_name = $(".bloodtrans-name").val();
    var bloodtrans_time = $(".bloodtrans-time").val();
    var _father = $("._father").val();
    var _mother = $("._mother").val();
    var _brother = $("._brother").val();
    var _children = $("._children").val();
    var family_disease = $("input[name='family-disease']").val();
    var is_genetic = $("input[name='inherited-disease']").val();
    var genetic_disease = $(".inherited-disease-name").val();
    var is_deformity = $("input[name='disability']").val();
    var deformity_number = $(".disability-identity").val();
    var mappoint = $("#mapPoint").val();

    $.ajax({
        type: "post",
        url: "/Adm/Elder/Edit",
        dataType: "json",
        data: {
            id:id,
            name: name,
            sex: sex,
            birthday: birthday,
            number: number,
            homeaddress: homeaddress,
            MapPoint:mappoint,
            homephone: homephone,
            contacts: contacts,
            c_telephone: c_telephone,

            //resident_type: household,
            //nation: nation,
            //blood_type: blood_type,
            //education: education,
            //occupation: occupation,
            //marriageStatus: marriageStatus,
            //dispensedmode: dispensedmode,
            //H_allergydrug: H_allergydrug,
            //H_disease: H_disease,
            //is_ops: is_ops,
            //ops_disease: ops_disease,
            //ops_data: ops_data,
            //is_genetic: is_genetic,
            //genetic_disease: genetic_disease,
            //is_deformity: is_deformity,
            //deformity_number: deformity_number
        },
        success: function (result) {
            //console.log(result);
            alert('老人訊息修改成功！');
            var subWindow = window.parent.document.getElementById('txtContentBody');
            subWindow.src = '/Adm/Elder/Index/1094/1099/0';
        },
    })
}