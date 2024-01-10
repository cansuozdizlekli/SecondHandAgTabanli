//document.getElementById('image').addEventListener('change', function () {
//    var input = this;
//    var image = document.getElementById('selected-image');
//    var label = document.querySelector('.file-input-label');

//    if (input.files && input.files[0]) {
//        var reader = new FileReader();

//        reader.onload = function (e) {
//            image.src = e.target.result;
//            image.style.display = 'block';
//            label.style.display = 'none'; // Gösterilen fotoğraf varsa, label'ı gizle
//        };

//        reader.readAsDataURL(input.files[0]);
//    }
//});

//document.getElementById("logoutButton").addEventListener("click", function () {
//    // Cookie'yi silmek için JavaScript kullanabilirsiniz
//    document.cookie = "tokenString=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

//    // Sayfayı yeniden yönlendirme (örneğin, login sayfasına)
//    window.location.href = "/Home/Login";
//});

