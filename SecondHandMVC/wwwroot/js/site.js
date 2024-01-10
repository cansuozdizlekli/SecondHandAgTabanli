// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function submitForm() {
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;

    var formData = new FormData();
    formData.append('username', username);
    formData.append('username', email);
    formData.append('password', password);

    // POST isteği yap
    fetch('/Home/Index', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Error:', error));

    console.log("Kullanıcı Adı: " + username);
    console.log("Şifre: " + password);
    console.log("Email: " + email);
}

if (!ViewData.ModelState.IsValid && ViewData.ModelState["Error"].Errors.Count > 0) {
    <text>
        $(document).ready(function() {
            alert('@ViewData.ModelState["Error"].Errors.First().ErrorMessage');
            });
    </text>
}

