document.addEventListener('DOMContentLoaded', function () {
    handleProfileImageUpload()

})

function handleProfileImageUpload() { }

try {

    let fileUploader = document.querySelector('#fileUploader');
    if (fileUploader != undefined) {

        fileUploader.addEventListener('change,' function () {

            if (this.file.length > 0) {
                this.form.submit();
            }
        }
    }
        
}
catch { }
}

