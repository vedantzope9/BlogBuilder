document.addEventListener("DOMContentLoaded", function () {
    const username = localStorage.getItem("username") || "";
    const jwtToken = localStorage.getItem("jwtToken");

    document.getElementById("username").textContent = username;

    // Write Blog Button click
    document.getElementById("writeBlogBtn").addEventListener("click", function () {
        if (jwtToken && username !== "") {
            window.location.href = "/Blog/CreateBlog";
        } else {
            alert("You must be logged in to write a blog.");
            window.location.href = "/User/LoginUser";
        }
    });

    // My Blogs Button click
    document.getElementById("myBlogsBtn").addEventListener("click", function () {
        if (jwtToken && username !== "") {
            window.location.href = "/Blog/MyBlogs";
        } else {
            alert("You must be logged in to view your blogs.");
            window.location.href = "/User/LoginUser";
        }
    });

    fetch("/User/GetAllUsernames")
        .then(res => res.json())
        .then(userMap => {
            document.querySelectorAll("[data-userid]").forEach(el => {
                const uid = el.getAttribute("data-userid");
                const name = userMap[uid] || "User";
                const parts = el.innerText.split("•");
                el.innerText = `Author: ${name} • ${parts[1]}`;
            });
        })
        .catch(err => console.error("Error fetching usernames:", err));
});


//search dropdown
document.getElementById("searchBlogs").addEventListener("keyup", async function () {
    const query = this.value.trim();
    const resultsDiv = document.getElementById("searchResults");

    if (query.length < 2) {
        resultsDiv.style.display = "none";
        return;
    }

    try {
        const response = await fetch(`/Blog/SearchBlogs/${encodeURIComponent(query)}`);
        if (!response.ok) throw new Error("Failed to fetch");

        const blogs = await response.json();

        if (blogs.length === 0) {
            resultsDiv.innerHTML = "<div class='dropdown-item text-muted'>No results found</div>";
        } else {
            resultsDiv.innerHTML = blogs.map(b =>
                `<a href="/Blog/GetBlogById/${b.blogID}" class="dropdown-item">
                                <strong>${b.blog_NAME}</strong> <small>(${b.topic_NAME})</small>
                            </a>`
            ).join("");


        }
        resultsDiv.style.display = "block";
    } catch (err) {
        console.error("Error fetching blogs:", err);
        resultsDiv.style.display = "none";
    }
});