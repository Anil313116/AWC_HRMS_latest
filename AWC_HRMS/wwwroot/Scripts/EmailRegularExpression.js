
    //function EmailValidate() {
    //    var email = document.getElementById("txtEmail").value;
    //var spanmess = document.getElementById("spanmess");
    //spanmess.innerHTML = "";
    //var expr = /^([\w-\.]+)@((\[[0-9]{1, 3}\.[0-9]{1, 3}\.[0-9]{1, 3}\.)|(([\w-]+\.)+))([a-zA-Z]{2, 4}|[0-9]{1, 3})(\]?)$/;
    //if (!expr.test(email)) {
    //    spanmess.innerHTML = "Invalid Email Id";
    //    }
    //}

function ValidateEmail() {
    var email = document.getElementById("txtEmail").value;
    var spanmess = document.getElementById("spanmess");
    spanmess.innerHTML = "";
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (!expr.test(email)) {
        spanmess.innerHTML = "Invalid email address.";
    }
}