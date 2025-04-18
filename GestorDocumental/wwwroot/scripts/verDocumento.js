function abrirPdf(base64Data) {
    const byteCharacters = atob(base64Data);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const file = new Blob([byteArray], { type: 'application/pdf' });
    const fileURL = URL.createObjectURL(file);
    window.open(fileURL, '_blank');
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

window.mostrarTexto = function (texto) {
    var newWindow = window.open('', '_blank');
    newWindow.document.write('<pre>' + texto + '</pre>');
};

window.mostrarImagen = function (base64Data) {
    var fileURL = "data:image/png;base64," + base64Data;
    var newWindow = window.open('', '_blank');
    newWindow.document.write('<img src="' + fileURL + '" alt="Imagen" />');
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



