
$("#submit").click(function(){
    var servicename=$('#servicename').val();
    var username=$('#username').val();
    var password=$('#password').val();
    if(servicename=='') {
        $(".err-msg").text("服務商名不能為空");
        $(".error-item").css("display","block");
        $("#servicename").focus();
        return false;
    }else if (username=='') {
        $(".err-msg").text("用戶名不能為空");
        $(".error-item").css("display","block");
        $("#username").focus();
        return false;
    }else if (password=='') {
        $(".err-msg").text("密碼不能為空");
        $(".error-item").css("display","block");
        $("#password").focus();
        return false;
    }
    else{
        $(".error-item").css("display","none");
        $.ajax({
            type: 'post',
            url: '/User/Login',
            data:{
                FacilitatorName : servicename,
                LoginName       : username,    
                Password        : password
            },
            dataType: 'json',
            success: function (d) {
                if (!d.flag) {
                    $(".err-msg").text(d.msg);
                    $(".error-item").css("display", "block");
                }
                else {
                    window.location.href = "../Adm/Control/index";
                }
            }
        })
     }
});