//window.setTimeout(function () {
//    $(".alert").fadeTo(500, O).slideUp(500, function () {
//        $(this).remove();
//    });    
//}, 4000);

 //Esperar un momento antes de ocultar el mensaje
//setTimeout(function () {
//    var alertDiv = document.querySelector(".fade-out");
//    alertDiv.style.opacity = "0";
//    setTimeout(function () {
//        alertDiv.style.display = "none";
//    }, 1000); // Puedes ajustar el tiempo del desvanecimiento aquí
//}, 2000); // Puedes ajustar el tiempo antes de iniciar el desvanecimiento aquí

setTimeout(function () {
    var alertDiv = document.querySelector(".fade-out");
    if (alertDiv) {
        alertDiv.style.opacity = "0";
        setTimeout(function () {
            alertDiv.style.display = "none";
        }, 1200); // Puedes ajustar el tiempo del desvanecimiento aquí
    }
}, 4000); // Puedes ajustar el tiempo antes de iniciar el desvanecimiento aquí

//setTimeout(function () {
//    var alert = document.getElementById('autoCloseAlert');
//    if (alert) {
//        alert.classList.add('fade');
//        alert.classList.remove('show');
//    }
//}, 5000);
