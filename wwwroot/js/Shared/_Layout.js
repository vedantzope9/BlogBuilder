const themeToggle = document.getElementById("themeToggle");
const body = document.body;

// Load theme on page load
const isDark = localStorage.getItem("theme") === "dark";
if (isDark) {
    body.classList.add("dark-mode");
    themeToggle.classList.remove("fa-moon");
    themeToggle.classList.add("fa-sun");
}

themeToggle.addEventListener("click", () => {
    body.classList.toggle("dark-mode");
    const isDarkNow = body.classList.contains("dark-mode");
    localStorage.setItem("theme", isDarkNow ? "dark" : "light");
    themeToggle.classList.toggle("fa-moon");
    themeToggle.classList.toggle("fa-sun");
});



const currentVersion = '@Context.Items["AppVersion"]';
const storedVersion = localStorage.getItem("appVersion");

//console.log(currentVersion  +" "+storedVersion);

if (currentVersion !== storedVersion) {
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("username");
    localStorage.removeItem("userid");

    localStorage.setItem("appVersion", currentVersion);
}