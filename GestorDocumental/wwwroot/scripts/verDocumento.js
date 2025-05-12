function abrirPdf(base64Data, dotNetHelper = null) {
    const byteCharacters = atob(base64Data);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const file = new Blob([byteArray], { type: 'application/pdf' });
    const fileURL = URL.createObjectURL(file);
    const win = window.open(fileURL, '_blank');

    if (dotNetHelper) {
        console.log("DotNetHelper recibido, iniciando monitoreo de cierre...");
        monitorCierreVentana(win, dotNetHelper);
    }
}

function base64ToBlob(base64, mime) {
    const byteCharacters = atob(base64);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray], { type: mime });
}

window.mostrarTexto = function (texto, dotNetHelper = null) {
    var newWindow = window.open('', '_blank');
    newWindow.document.write('<pre>' + texto + '</pre>');

    if (dotNetHelper) {
        monitorCierreVentana(newWindow, dotNetHelper);
    }
};

window.mostrarImagen = function (base64Data, dotNetHelper = null) {
    var fileURL = "data:image/png;base64," + base64Data;
    var newWindow = window.open('', '_blank');
    newWindow.document.write('<img src="' + fileURL + '" alt="Imagen" />');

    if (dotNetHelper) {
        monitorCierreVentana(newWindow, dotNetHelper);
    }
};

// Descarga de archivo
window.descargarArchivo = function (base64Data, mime, fileName) {
    var file = base64ToBlob(base64Data, mime);
    var fileURL = URL.createObjectURL(file);
    var a = document.createElement('a');
    a.href = fileURL;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};

function monitorCierreVentana(win, dotNetHelper) {
    const interval = setInterval(() => {
        if (win.closed) {
            clearInterval(interval);
            dotNetHelper.invokeMethodAsync("OnVisorCerrado")
                .then(() => console.log("OnVisorCerrado invocado correctamente"))
                .catch(err => console.error("Error llamando a OnVisorCerrado:", err));
        }
    }, 1000);
}


