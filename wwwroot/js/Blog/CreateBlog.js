let quill;

document.addEventListener("DOMContentLoaded", () => {

    // Init Quill
    quill = new Quill('#editor', {
        theme: 'snow',
        placeholder: "Start writing your blog...",
        modules: {
            toolbar: [
                [{ 'font': [] }, { 'size': [] }],
                ['bold', 'italic', 'underline', 'strike'],
                [{ 'color': [] }, { 'background': [] }],
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                [{ 'align': [] }],
                ['link', 'image', 'code-block'],
                ['clean']
            ]
        }
    });

    addClientFileValidation();
    setupTopicSelect();
    setupFormSubmit();
});

function toggleTheme() {
    document.body.classList.toggle("dark-mode");
    const isDark = document.body.classList.contains("dark-mode");
    localStorage.setItem("theme", isDark ? "dark" : "light");
}

function addClientFileValidation() {
    const fileInput = document.querySelector('input[type="file"][name="image"]');
    const preview = document.getElementById('imagePreview');
    const MAX_BYTES = 5 * 1024 * 1024;

    fileInput.addEventListener('change', (e) => {
        const f = e.target.files[0];
        if (!f) {
            // No file selected or cancel pressed
            preview.style.display = 'none';
            preview.src = '';
            return;
        }

        if (!/^image\/(png|jpe?g|webp|gif)$/i.test(f.type)) {
            alert('Please upload an image (png, jpg, webp, gif).');
            e.target.value = '';
            preview.style.display = 'none';
            return;
        }

        if (f.size > MAX_BYTES) {
            alert('Image too large. Max 5MB allowed.');
            e.target.value = '';
            preview.style.display = 'none';
            return;
        }

        // Show preview
        preview.src = URL.createObjectURL(f);
        preview.style.display = 'block';
    });
}

function setupTopicSelect() {
    const topicSelect = document.getElementById("topicSelect");
    const topicOther = document.getElementById("topicOther");

    topicSelect.addEventListener("change", () => {
        if (topicSelect.value === "Other") {
            topicOther.style.display = "block";
            topicOther.setAttribute("required", "required");
        } else {
            topicOther.style.display = "none";
            topicOther.removeAttribute("required");
            topicOther.value = "";
        }
    });
}

function setupFormSubmit() {
    const form = document.getElementById("createBlogForm");
    const jwtToken = localStorage.getItem("jwtToken");
    const userId = localStorage.getItem("userid");

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        if (!jwtToken || !userId) {
            alert("Login required.");
            window.location.href = "/User/LoginUser";
            return;
        }

        const formData = new FormData(form);
        const topicSelect = document.getElementById("topicSelect");
        const topicOther = document.getElementById("topicOther");

        formData.append("BLOG_CONTENT", quill.root.innerHTML);
        formData.append("USERID", userId);
        formData.append("isUpdated", false);

        // Save proper topic
        if (topicSelect.value === "Other") {
            formData.set("TOPIC_NAME", topicOther.value);
        } else {
            formData.set("TOPIC_NAME", topicSelect.value);
        }

        fetch("/Blog/CreateBlog", {
            method: "POST",
            headers: { "Authorization": `Bearer ${jwtToken}` },
            body: formData
        })
            .then(res => {
                if (!res.ok) throw new Error("Something went wrong.");
                return res.text();
            })
            .then(() => {
                alert("Blog created!");
                window.location.href = "/Blog/Index";
            })
            .catch(err => {
                console.error(err);
                alert("Error: " + err.message);
            });
    });
}