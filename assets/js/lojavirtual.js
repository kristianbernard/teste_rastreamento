function validadimensoes(control) {


    var txtaltura = document.getElementById("txtaltura");

    if (parseFloat(txtaltura.value).toFixed(2) < 2 || parseFloat(txtaltura.value).toFixed(2) > 105) {
        document.getElementById("imgerroaltura").style.display = "initial";
        txtaltura.style.background = "#ff0000";
        txtaltura.style.color = "#ffffff";
    } else {
        document.getElementById("imgerroaltura").style.display = "none";
        txtaltura.style.background = "#ffffff";
        txtaltura.style.color = "#000000";

    }

    var txtlargura = document.getElementById("txtlargura");

    if (parseFloat(txtlargura.value).toFixed(2) < 18 || parseFloat(txtlargura.value).toFixed(2) > 105) {
        document.getElementById("imgerolargura").style.display = "initial";
        txtlargura.style.background = "#ff0000";
        txtlargura.style.color = "#ffffff";
    } else {
        document.getElementById("imgerolargura").style.display = "none";
        txtlargura.style.background = "#ffffff";
        txtlargura.style.color = "#000000";

    }

    var txtcomprimento = document.getElementById("txtcomprimento");

    if (parseFloat(txtcomprimento.value).toFixed(2) < 11 || parseFloat(txtcomprimento.value).toFixed(2) > 105) {
        document.getElementById("imgerrocomprimento").style.display = "initial";
        txtcomprimento.style.background = "#ff0000";
        txtcomprimento.style.color = "#ffffff";
    } else {
        document.getElementById("imgerrocomprimento").style.display = "none";
        txtcomprimento.style.background = "#ffffff";
        txtcomprimento.style.color = "#000000";

    }


}