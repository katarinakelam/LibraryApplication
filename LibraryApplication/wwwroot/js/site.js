$(document).ready(function () {

    //Setup Microblink API
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
                results = data.result.data.result;
            }

            for (let i = 0; i < results.length; i++) {
                if (results[i].result == null) {
                    results.splice(i, 1);
                }
            }
            if (results.length < 1) {
                $('.modal-body').html('<p>Scanning is finished, but we could not extract the data. Please check if you uploaded the right document type.</p>');
            } else {
                {
                    //Check data validity. 
                    checkDigits(results[0].result.rawMRZString);

                    //Fill up user form with scanned data.
                    fillUpFormWithUserData(results[0].result);

                    //Show user form filled with data.
                    $(".microblink-ui-component-wrapper").hide();
                    $("#manual-data-enter").show();
                }
            }
            $('.modal-title').text("Scan success");
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
    $("#manual-data-enter").hide();

    //Hide Microblink component on manual input option chosen event
    $("#manual-enter-link").click(function (event) {
        $(".microblink-ui-component-wrapper").hide();
        $("#manual-data-enter").show();
    });

    //Add new input field for user contacts on button click
    $(document).on('click', '.btn-add', function (e) {
        e.preventDefault();

        var controlForm = $('.user-contacts-input form:first'),
            currentEntry = $(this).parents('.entry:first'),
            newEntry = $(currentEntry.clone()).appendTo(controlForm);

        newEntry.find('input').val('');
        controlForm.find('.entry:not(:last) .btn-add')
            .removeClass('btn-add').addClass('btn-remove')
            .removeClass('btn-success').addClass('btn-danger')
            .html('<svg width="2em" height="1.5em" viewBox="0 0 16 16" class="bi bi-dash" fill="currentColor" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8z"/></svg>');
    }).on('click', '.btn-remove', function (e) {
        $(this).parents('.entry:first').remove();

        e.preventDefault();
        return false;
    });

    //Send user data to server on submit button click
    $("#submit-button").click(function () {
        sendApiRequest();
    });

    function sendApiRequest() {
        let date = new Date($('#dob').val());

        //Gather and serialize data.
        let data = JSON.stringify({
            'firstName': $("#firstName").val(),
            'lastName': $("#lastName").val(),
            'dateOfBirth': date,
            'userContacts': $('input[name^=fields]').map(function (idx, elem) {
                return $(elem).val();
            }).toArray(),
            'isValid': $("#isValid").val()
        });

        $.ajax('/api/users', {
            type: 'POST',
            data: data,
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                alert("User created!");
                setTimeout(2000);
                window.location.reload();
            },
            error: function (jqXhr, textStatus, errorMessage) {
                alert("Error! " + errorMessage);
            }
        });
    }

    //Fill up user form with data from OCR scanning
    function fillUpFormWithUserData(results) {
        $("#firstName").val(results.secondaryID);
        $("#lastName").val(results.primaryID);
        var dateOfBirth = results.dateOfBirth;

        if (dateOfBirth.month.toString().length == 1)
            dateOfBirth.month = "0" + dateOfBirth.month;

        if (dateOfBirth.day.toString().length == 1)
            dateOfBirth.day = "0" + dateOfBirth.day;

        $('#dob').val(dateOfBirth.year + "-" + dateOfBirth.month + "-" + dateOfBirth.day);
    }

    //Check date checksum digit validity
    function checkDigits(mrzString) {
        var dateAndCheckDigit = mrzString.split("\n")[1].slice(0, 7);
        var dateStr = dateAndCheckDigit.slice(0, 6);
        var checkDigit = parseInt(dateAndCheckDigit.slice(6, 7));

        var checkSum = 0;
        var multipliers = [7, 3, 1];

        for (var i = 0; i < dateStr.length; i++) {
            checkSum += parseInt(dateStr.charAt(i)) * multipliers[i % 3];
        }

        $("#isValid").val(checkSum % 10 == checkDigit);
    }
});
