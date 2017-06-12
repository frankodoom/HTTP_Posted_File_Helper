$(document).ready(function () {

    $('form').on('submit', function (event) {
        event.preventDefault();

        var formData = new FormData($('form')[0]);

        $.ajax({

            xhr: function () {
                var xhr = new window.XMLHttpRequest();
             
                xhr.upload.addEventListener('progress', function (e) {
                    if (e.lengthComputable) {
                        console.log('Bytes Loaded' + e.loaded);
                        console.log('Total Size', e.total);
                        var percentageUploaded = (e.loaded / e.total);
                        console.log('Percentage Loaded', + percentageUploaded)

                        var percent = Math.round(percentageUploaded * 100);

                        $('#progressBar').attr('aria-valuenow', percent)
                            .css('width', percent + '%')
                            .text(percent + "%");
                    }
                })

                return xhr;
            },
            type: 'Post',
            url: '/Home/UploadFiles',
            data: formData,
            processData: false,
            contentType: false,
            success: function () {
                alert('File Uploaded Successfully')
            },
            onerror: function () {
                alert('Something Went Wromg')
            }
        }
        )
    })
})