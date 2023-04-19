$(document).ready(function () {
    $('#ChooseImg').change(function (e) {

       let url = $('#ChooseImg').val();
        let ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
        if (ChooseImg.files && ChooseImg.files[0] && (ext == "gif" || ext == "jpg" || ext == "jfif" || ext == "png" || ext == "bmp")) {
            let reader = new FileReader();
            reader.onload = function () {
                let output = document.getElementById('PrevImg');
                output.src = reader.result;
            }
            reader.readAsDataURL(e.target.files[0]);
        }
    });
});

