$(document).ready(function () {
    // Save
    $(".btn-save").click(function () {
        var id = $(this).data("id");
        var nationName = $("#Nationality_name_" + id).val();

        $.ajax({
            url: updateNationalityUrl, // URL cập nhật từ Razor
            type: 'POST',
            data: { id: id, nationName: nationName },
            success: function (response) {
                alert(response.message);
            },
            error: function () {
                alert("エラーが発生しました。もう一度お試しください。");
            }
        });
    });

    // Delete
    $(".btn-delete").click(function () {
        var id = $(this).data("id"); // Lấy id từ data-id

        if (confirm("このデータは本当に削除されたのでしょうか？")) {
            $.ajax({
                url: deleteNationalityUrl, // URL xóa từ Razor
                type: 'POST',
                data: { id: id },
                success: function (response) {
                    if (response.success) {
                        alert("削除完了");
                        // Xóa phần tử khỏi DOM
                        $("#row-" + id).remove();
                    } else {
                        alert("エラーが発生しました。もう一度お試しください。");
                    }
                },
                error: function () {
                    alert("エラーが発生しました。もう一度お試しください。");
                }
            });
        }
    });
});
