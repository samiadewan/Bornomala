$(document).ready(function () {
    $('#chooseImg').change(function (e) {

        let url = $('#chooseImg').val();
        let ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
        if (chooseImg.files && chooseImg.files[0] && (ext == "gif" || ext == "jpg" || ext == "jfif" || ext == "png" || ext == "bmp")) {
            let reader = new FileReader();
            reader.onload = function () {
                let output = document.getElementById('prevImg');
                output.src = reader.result;
            }
            reader.readAsDataURL(e.target.files[0]);
        }
    });
});