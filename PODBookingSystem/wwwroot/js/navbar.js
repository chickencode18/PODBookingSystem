document.addEventListener('DOMContentLoaded', function () {
    const user = JSON.parse(localStorage.getItem('user'));
    if (user) {
        document.getElementById("profile-username").textContent = user.username;
        document.getElementById("profile-image").src = user.image;
        document.getElementById("profile-title").textContent = user.title;
        document.getElementById("profile-role").textContent = user.role;
        document.getElementById("profile-balance").textContent = user.balance;
    }

    document.getElementById("profileForm").addEventListener('submit', function (event) {
        event.preventDefault();
        const updatedUser = {
            ...user,
            username: document.getElementById("username").value,
            image: document.getElementById("image").value,
            title: document.getElementById("title").value,
        };
        localStorage.setItem('user', JSON.stringify(updatedUser));
        document.getElementById("profile-username").textContent = updatedUser.username;
        document.getElementById("profile-image").src = updatedUser.image;
        document.getElementById("profile-title").textContent = updatedUser.title;
    });

    document.getElementById("logout").addEventListener('click', function () {
        localStorage.removeItem('user');
        window.location.href = 'login.html';
    });
});

const menuBtn = document.querySelector(".menuBtn");
const navBar = document.querySelector(".navBar");
menuBtn.addEventListener("click", navToggle);

function navToggle() {
  menuBtn.classList.toggle("openmenu");
  navBar.classList.toggle("open");
  if (navBar.classList.contains("open")) {
    navBar.style.maxHeight = navBar.scrollHeight + "px";
  } else {
    navBar.removeAttribute("style");
  }
}

$(document).ready(function () {
    $(".menuBtn").click(function () {
        $(".navBar").toggleClass("show");
    });

    $("#searchButton").click(function () {
        const searchQuery = $("#searchInput").val().toLowerCase();
        if (searchQuery) {
            if (searchQuery === "văn phòng trong ngày") {
                window.location.href = "gameDetails.html";
            } else if (searchQuery === "phòng họp") {
                window.location.href = "gameDetails1.html";
            } else if (searchQuery === "không gian làm việc chung") {
                window.location.href = "gameDetails6.html";
            } else if (searchQuery === "văn phòng ảo") {
                window.location.href = "gameDetails7.html";
            } else {
                window.location.href = `searchResults.html?search=${encodeURIComponent(searchQuery)}`;
            }
        }
    });

    // Hiển thị thông tin bình luận
    function displayComments() {
        const comments = JSON.parse(localStorage.getItem('comments')) || [];
        const commentsContainer = $('#comments-container');
        commentsContainer.empty();
        comments.forEach(comment => {
            const commentElement = `<div class="comment"><strong>${comment.username}</strong>: ${comment.text}</div>`;
            commentsContainer.append(commentElement);
        });
    }

    // Lưu bình luận
    $('#submit-comment').click(function () {
        const user = JSON.parse(localStorage.getItem('user'));
        if (!user) {
            alert('Bạn cần đăng nhập để bình luận.');
            return;
        }
        const commentText = $('#comment-text').val();
        if (commentText.trim() === '') {
            alert('Bình luận không được để trống.');
            return;
        }

        const comments = JSON.parse(localStorage.getItem('comments')) || [];
        comments.push({ username: user.username, text: commentText });
        localStorage.setItem('comments', JSON.stringify(comments));
        $('#comment-text').val('');
        displayComments();
    });

    // Hiển thị bình luận khi tải trang
    displayComments();
});

