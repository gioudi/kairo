
function mascara(o, f) {
    //console.log('Ingresa a mascara');
    v_obj = o;
    v_fun = f;
    setTimeout("ejecutarMascara()", 1);
}

function ejecutarMascara() {
    v_obj.value = v_fun(v_obj.value);
}

function validador(v) {
    //console.log('Ingresa a validador');
    v = v.replace(/([^0-9\.]+)/g, '');
    v = v.replace(/^[\.]/, '');
    v = v.replace(/[\.][\.]/g, '');
    v = v.replace(/\.(\d)(\d)(\d)/g, '.$1$2');
    v = v.replace(/\.(\d{1,2})\./g, '.$1');
    v = v.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,');
    v = v.split('').reverse().join('').replace(/^[\,]/, '');
    return v;
}

function validadorSinDecimal(v) {
    //console.log('Ingresa a validadorSinDecimal');
    v = v.replace(/([^0-9\.]+)/g, '');
    v = v.replace(/^[\.]/, '');
    v = v.replace(/[\.]/g, '');
    v = v.replace(/\.(\d)(\d)(\d)/g, '.$1$2');
    v = v.replace(/\.(\d{1,2})\./g, '.$1');
    v = v.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,');
    v = v.split('').reverse().join('').replace(/^[\,]/, '');
    return v;
}


function dateCaptureAforo() {

    $('#tanque-fechaAforo').formatter({
        'pattern': '{{99}}/{{99}}/{{9999}}',
    });
};

function characterCounterObs() {

    $('textarea#tanque-observaciones').characterCounter();
};

