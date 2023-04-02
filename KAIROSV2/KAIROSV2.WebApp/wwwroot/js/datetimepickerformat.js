// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(function () {
    $('.timepicker').timepicker();
    $('.datepicker').datepicker();

    //document.addEventListener('DOMContentLoaded', function () {
    //    var elems = document.querySelectorAll('.timepicker');
    //    var instances = M.Timepicker.init(elems, {
    //        defaultTime: '13:14'

    //    });
    //});

    $('#fechaInicial').formatter({
        'pattern': '{{99}}/{{99}}/{{9999}}',
    });

    $('#horaInicial').formatter({
        'pattern': '{{99}}:{{99}}',
    });

    $('#fechaFinal').formatter({
        'pattern': '{{99}}/{{99}}/{{9999}}',
    });

    $('#horaFinal').formatter({
        'pattern': '{{99}}:{{99}}',
    });
});


// ** Insersion de fecha y hora en inputs usando datePikers y Timepickers** 

function dateCaptureIni() {
    // fechas capturadas
    var strDate = document.getElementById("datePickerCapIni").value;
    //var strDate = 0;

    document.getElementById("fechaInicial").value = strDate;
    document.getElementById("fechaInicial").focus();

    $('#fechaInicial').formatter({
        'pattern': '{{99}}/{{99}}/{{9999}}',
    });
};


function timeCaptureIni() {
    // fechas capturadas
    var strTime = document.getElementById("timePickerCapIni").value;
    //var strDate = 0;

    document.getElementById("horaInicial").value = strTime;

    $('#horaInicial').formatter({
        'pattern': '{{99}}:{{99}}',
    });
};


function dateCaptureFin() {
    // fechas capturadas
    var finDate = document.getElementById("datePickerCapFin").value;
    //var strDate = 0;
    
    document.getElementById("fechaFinal").value = finDate;

    $('#fechaFinal').formatter({
        'pattern': '{{99}}/{{99}}/{{9999}}',
    });
};

function timeCaptureFin() {
    // fechas capturadas
    var strTime = document.getElementById("timePickerCapFin").value;
    //var strDate = 0;

    document.getElementById("horaFinal").value = strTime;

    $('#horaFinal').formatter({
        'pattern': '{{99}}:{{99}}',
    });
};