
// $(document).ready(function(){
//     var i18n = { // 本地化
//         previousMonth   : '上个月',
//         nextMonth       : '下个月',
//         months          : ['一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月'],
//         weekdays        : ['周日','周一','周二','周三','周四','周五','周六'],
//         weekdaysShort   : ['日','一','二','三','四','五','六']
//     }

//     var datepicker = new Pikaday({ 
//         field:      document.getElementById('datepicker'),
//         minDate:    new Date('1900-01-01'), 
//         maxDate:    new Date('2050-12-31'), 
//         yearRange:  [1900,2050],
//         i18n:       i18n,
//         onSelect:   function() {
//             var date = document.createTextNode(this.getMoment().format('YYYY-MM-DD') + ' '); //生成的时间格式化成 2013-09-25
//             //$('#datepicker').appendChild(date);
//         }
//     });
// });



// $(".btn").click(function() {
//     var servicename=$('#ServiceName').val().length;
//     var loginname=$('#LoginName').val().length;
//     if(servicename==''&& loginname=='') {
//         alert("服务商不存在");
//         $("#ServiceName").focus();
//         return false;
//     }else if (servicename<3&&loginname<4) {
//         alert("用户名不存在");
//         $("#ServiceName").focus();
//         return false;
//     }else{
//         $.ajax({
//             url:Action("Login", "User"),
//             type:"post",
//             data: {
//                     FacilitatorName:$('#ServiceName').val(),
//                     LoginName:$('#LoginName').val(),
//                     Password:$('#Password').val()
//                 },
//             success:function (data) {
//                 if(data.msg==1){
//                     alert("123");
//                 }else{
//                     alert(data.msg);
//                 }
//             }
//         })
//     }

// });

        // var yes = {};
        // $(".oldman-name,.oldman-id").blur(function() {
        //     if($(this).val() != "") {
        //         yes.user = true;
        //     }
        // });

        // //密码验证
        // $(".pswd").focus(function() {
        //     yes.pswd = false;
        //     $(".pswd_tit").css("opacity", "1");
        //     $(this).on("input", function() {
        //         if($(this).val().length >= 6) {
        //             $(".pswd_tit").css("color", "#ABABAB");
        //         }
        //     });
        // });
        // $(".pswd").blur(function() {
        //     if($(this).val().length >= 6 && $(this).val() != "" && /[a-zA-Z]/.test($(this).val())) {
        //         $(".pswd_tit").css("color", "#ABABAB");
        //         $(".pswd_tit").css("opacity", "0");
        //         yes.pswd = true;
        //     } else if($(this).val() != "") {
        //         $(".pswd_tit").css("opacity", "1");
        //         $(".pswd_tit").css("color", "red");
        //         $(".pswd_tit").html("密码中必须含有英文字符，长度在6-20个字符之间");
        //     } else {
        //         $(".pswd_tit").css("opacity", "0");
        //         $(".pswd_tit").css("color", "#ABABAB");
        //     }
        //     if($(this).val() == $(".pswd2").val()) {
        //         $(".pswd2_tit").css("opacity", "0");
        //         $(".pswd2_tit").css("color", "ABABAB");
        //         yes.pswd = true;
        //     } else if($(".pswd2").val() != "") {
        //         $(".pswd2_tit").html("两次输入密码不一致");
        //         $(".pswd2_tit").css("opacity", "1");
        //         $(".pswd2_tit").css("color", "red");
        //     }
        // });
        // //再次输入密码的判断
        // $(".pswd2").focus(function() {
        //     yes.pswd2 = false;
        //     if($(this).val() == "") {
        //         $(".pswd2_tit").html("请再次输入密码");
        //         $(".pswd2_tit").css("opacity", "1");
        //     }
        // })
        // $(".pswd2").blur(function() {
        //     if($(this).val() != "" && $(this).val() != $(".pswd").val()) {
        //         $(".pswd2_tit").html("两次输入密码不一致");
        //         $(".pswd2_tit").css("opacity", "1");
        //         $(".pswd2_tit").css("color", "red");
        //     } else {
        //         $(".pswd2_tit").css("opacity", "0");
        //         $(".pswd2_tit").css("color", "ABABAB");
        //         yes.pswd2 = true;
        //     }
        // })