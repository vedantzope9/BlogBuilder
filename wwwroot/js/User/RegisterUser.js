$(document).ready(function () {
    // Persist dark mode on this page
    const isDark = localStorage.getItem("theme") === "dark";
    if (isDark) {
        $('body').addClass('dark-mode');
        $('#themeToggle').removeClass('fa-moon').addClass('fa-sun');
    }

    $('#togglePassword').on('click', function () {
        const passwordInput = $('#password');
        const type = passwordInput.attr('type') === 'password' ? 'text' : 'password';
        passwordInput.attr('type', type);

        const icon = $(this).find('i');
        if (type === 'text') {
            icon.removeClass('fa-eye').addClass('fa-eye-slash');
        } else {
            icon.removeClass('fa-eye-slash').addClass('fa-eye');
        }
    });


    $('#registerForm').on('submit', function (e) {
        e.preventDefault();
        const username = $('#username').val().trim();
        const email = $('#email').val().trim();
        const password = $('#password').val().trim();
        const $btn = $('.cta-btn');
        $btn.removeClass('clicked');
        void $btn[0].offsetWidth;
        $btn.addClass('clicked');


        if (!username || !email || !password) {
            alert("All fields are required.");
            return;
        }

        $.ajax({
            url: '/User/RegisterUser',
            type: 'POST',
            data: { username, email, password },
            success: function (response) {
                if (response.success) {
                    alert("Registration Successful!");
                    window.location.href = "/User/LoginUser";
                } else {
                    alert(response.message || "Registration failed.");
                }
            },
            error: function () {
                alert("Server error. Please try again.");
            }
        });
    });
});