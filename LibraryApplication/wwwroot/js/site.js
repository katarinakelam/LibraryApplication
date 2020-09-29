$(document).ready(function () {

    Microblink.SDK.SetEndpoint('http://localhost:8081');
    Microblink.SDK.SetRecognizers('MRTD');
    Microblink.SDK.SetIsDataPersistingEnabled(false);
    Microblink.SDK.SetExportImages(true);

    Microblink.SDK.RegisterListener({
        onScanSuccess: (data) => {
            let results = null;
            if (data.result.data) {
                results = data.result.data instanceof Array ? data.result.data : [data.result.data];
            }
            else {
                results = data.result.result;
            }

            for (let i = 0; i < results.length; i++) {
                if (results[i].result == null) {
                    results.splice(i, 1);
                }
            }
            if (results.length < 1) {
                $('.modal-body').html('<p>Scanning is finished, but we could not extract the data. Please check if you uploaded the right document type.</p>');
            } else {
                $('.modal-body').html(colorJson(results));
            }
            $('.modal-title').text("Scan success");
            //$('.modal').modal('show');
        },
        onScanError: (error) => {
            $('.modal-title').text("Error occured");
            $('.modal-body').html(colorJson(error));
            $('.modal').modal('show');
        }
    });

    setTimeout(function () {
        document.querySelectorAll('.hide-until-component-is-loaded').forEach(function (element) {
            element.classList.remove('hide-until-component-is-loaded');
        })
    }, 1000);

    $(".error-container").removeClass("show");

});
