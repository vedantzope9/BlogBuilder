$(document).ready(function () {
    // Ensure dark mode persists
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


    $('#loginForm').on('submit', function (e) {
        e.preventDefault();
        const email = $('#email').val().trim();
        const password = $('#password').val().trim();
        const $btn = $('.cta-btn');
        $btn.removeClass('clicked');
        void $btn[0].offsetWidth;
        $btn.addClass('clicked');

        $.ajax({
            url: '/User/LoginUser',
            type: 'POST',
            data: { email, password },
            success: function (response) {
                if (response.success) {
                    localStorage.setItem("jwtToken", response.token);
                    localStorage.setItem("userid", response.userid);
                    localStorage.setItem("username", response.username);
                    console.log("Login successful");
                    window.location.href = "/Blog/Index";
                } else {
                    alert(response.message || "Invalid login credentials.");
                }
            },
            error: function () {
                alert("Server error. Please try again.");
            }
        });
    });
});