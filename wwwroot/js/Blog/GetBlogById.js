document.addEventListener("DOMContentLoaded", () => {
    // Theme setting
    if (localStorage.getItem("theme") === "dark") {
        document.body.classList.add("dark-mode");
    }


    // Unique user IDs (for author and comments)
    const userIds = [...new Set(Array.from(document.querySelectorAll("[data-userid]"))
        .map(el => el.getAttribute("data-userid")))];

    // Fetch all usernames in one go
    fetch("/User/GetAllUsernames")
        .then(res => res.json())
        .then(userMap => {
            // Update Author name
            const blogMeta = document.getElementById("blog-meta");
            const uid = blogMeta?.getAttribute("data-userid");
            if (uid && userMap[uid]) {
                const parts = blogMeta.innerText.split("•");
                blogMeta.innerText = `Author: ${userMap[uid]} • ${parts[1]}`;
            }

            // Update each comment username
            document.querySelectorAll("[data-userid]").forEach(el => {
                const uid = el.getAttribute("data-userid");
                if (uid && userMap[uid] && el.id !== "blog-meta") {
                    el.textContent = userMap[uid];
                }
            });
        })
        .catch(err => console.error("Error fetching usernames:", err));
});

document.addEventListener("DOMContentLoaded", () => {
    const submitBtn = document.getElementById("submit-comment");
    const cancelBtn = document.getElementById("cancel-comment");
    const commentBox = document.getElementById("comment-text");

    const blogId = @Model.BLOGID;
    const userId = localStorage.getItem("userid");
    const token = localStorage.getItem("jwtToken");

    //console.log(userId +" "+token);

    // Cancel clears textarea
    cancelBtn.addEventListener("click", () => {
        commentBox.value = "";
    });

    submitBtn.addEventListener("click", () => {
        const comment = commentBox.value.trim();
        if (!token || !userId) {
            alert("You must be logged in to comment.");
            window.location.href = "/User/LoginUser";
            return;
        }

        if (comment === "") {
            alert("Comment cannot be empty.");
            return;
        }

        const data = {
            COMMENT: comment,
            BLOGID: blogId,
            USERID: parseInt(userId)
        };

        // console.log(data);

        fetch("/Comments/AddComment", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(data)
        })
            .then(res => {
                if (!res.ok) throw new Error("Failed to add comment");
                return res.text(); // assuming view returns something or just 200
            })
            .then(() => {
                // Optionally: Reload comments
                location.reload(); // or dynamically append comment
            })
            .catch(err => {
                alert("Error adding comment.");
                console.error(err);
            });
    });
});