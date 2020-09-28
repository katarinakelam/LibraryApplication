$(document).ready(function () {
    // 2. configure Microblink SDK

    /*
     * Use this method if you are using country specific recognizers
     * Multiple recognizers are allowed here
     */
    // Microblink.SDK.SetRecognizers(['MRTD']);

    /*
     * This method allows you to set the endpoint of the API service
     * If you are using Self-hosted API, set this to the address where your service is available
     */
    Microblink.SDK.SetEndpoint('http://0.0.0.0:80');

    /*
     * This methods allows you to use our improved recognizers, or to test how our Cloud API works
     * Set request parameters for desired recognizer
     * This method will override Microblink.SDK.SetRecognizers() method
     * This way, you can use only one recognizer per request
     * Available recognizers are (BLINK_ID, ID_BARCODE, MRTD, PASSPORT, VISA, MRZ_ID)
     *
     * Examples:
     * Microblink.SDK.SetupRecognizerRequest('BLINK_ID');
     * or
     * Microblink.SDK.SetupRecognizerRequest('ID_BARCODE');
     * or
     * Microblink.SDK.SetupRecognizerRequest('PASSPORT');
     * ...
     */
    Microblink.SDK.SetupRecognizerRequest('MRTD');

    // 2.3. disable persisting uploaded data (this option is ignored if Authorization header is set)
    Microblink.SDK.SetIsDataPersistingEnabled(false);

    // 2.4. setup export images
    Microblink.SDK.SetExportImages(true);

    // 2.5. default listeners (API's callbacks) are defined inside of the microblink-js UI component, but it is possible to configure global listeners to implement custom success/error handlers
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

    // 3. better UI, this removes microblink-js flash effect because microblink-js UI component (like any other HTML5 web component) loads asynchronusly and component's custom slots will look ugly until it's CSS/JS is loaded propertly (rendering speed depends on the CPU power of the machine where browser is used, 1 second if enough to cover any average machine).
    setTimeout(function () {
        document.querySelectorAll('.hide-until-component-is-loaded').forEach(function (element) {
            element.classList.remove('hide-until-component-is-loaded');
        })
    }, 1000);

});// 1. initialize Firebase application before Microblink SDK to enable feature desktop-to-mobile
