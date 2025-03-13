document.addEventListener("DOMContentLoaded", function () {
    var uploadUrl = $("#uploadTempImageUrl").val(); // Lấy URL

    document.getElementById("ProfilePictureFile").addEventListener("change", function (event) {
        var file = event.target.files[0]; // Lấy file
        if (file) {
            var reader = new FileReader(); // Đọc file
            reader.onload = function (e) {
                var img = document.getElementById("previewImage");
                img.src = e.target.result; // Hiển thị ảnh
                img.style.display = "block"; // Hiển thị ảnh nếu nó đang ẩn
            };
            reader.readAsDataURL(file); // Đọc file

            // upload
            var formData = new FormData();
            formData.append("file", file);

            $.ajax({
                url: $("#uploadTempImageUrl").val(), 
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.success) {
                        console.log("写真は暫定保存をしました。!");
                        $("#SavedFilePath").val(response.filePath); // Gán đường dẫn tạm vào input ẩn
                    } else {
                        alert("Lỗi khi tải ảnh lên.");
                    }
                }
            });
        }
    });
});
