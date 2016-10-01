$(document).ready(function () {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-bottom-full-width",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    if (navigator.onLine) {
        toastr.success('Estamos navegando online!', 'Status da Rede');
    } else {
        toastr.error('Estamos navegando offline!', 'Status da Rede');
    }
});
