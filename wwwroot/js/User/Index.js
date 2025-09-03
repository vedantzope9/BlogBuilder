
// Handle Get Started button click
document.getElementById("startBtn").addEventListener("click", function (e) {
    e.preventDefault();
    const token = localStorage.getItem("jwtToken");

    if (token) {
        window.location.href = "/Blog/Index";
    } else {
        window.location.href = "/User/LoginUser";
    }
});