function avisoAguarde() {
    if (document.getElementById('divProcessando')) {
        document.getElementById('divProcessando').style.display = '';
        return;
    }
    oDiv = document.createElement("div");
    oDiv.id = "divProcessando";
    document.body.appendChild(oDiv);
}
function RetirarAvisoAguarde() {
    if (document.getElementById('divProcessando')) {
        document.getElementById('divProcessando').style.display = 'none';
        return;
    }
}