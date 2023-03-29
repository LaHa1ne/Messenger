function AddServerErrorAlert(StatusCode, ErrorDescription) {
    $('#alerts').append(
        '<div class="alert alert-danger alert-dismissible fade show" role="alert">' +
        '<h4 class="alert-heading" name="StatusCode">' + StatusCode + '</h4 > ' +
        '<hr><p class="mb-0">' +
        '<i class="bi bi-exclamation-triangle-fill me-2" role="img" aria-label="Danger:"></i>' +
        '<span name="ErrorDescription">' + ErrorDescription + '</span ></p>' +
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
        '</div>'
    );
}

function AddSuccessAlert(Message) {
    $('#alerts').append(
        '<div class="alert alert-success alert-dismissible fade show" role="alert">' +
        '<i class="bi bi-check-circle-fill me-2" role="img" aria-label="Success:"></i>' +
        '<span name="TextMessage">' + Message + '</span ></p>' +
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
        '</div>'
    );
}

function AddWarningAlert(Message) {
    $('#alerts').append(
        '<div class="alert alert-warning alert-dismissible fade show" role="alert">' +
        '<i class="bi bi-dash-circle-fill me-2" role="img" aria-label="Warning:"></i>' +
        '<span name="TextMessage">' + Message + '</span ></p>' +
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
        '</div>'
    );
}

function AutocloseAlert() {
    setTimeout(function () {
        $('.alert').alert('close');
    }, 5000);
}
