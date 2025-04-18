window.registrarObjetoDotNet = function (dotNetHelper) {
    window.miObjetoDotNet = dotNetHelper;
}

let archivoArrastradoId = null;

function onDragStart(event) {
    const archivoId = event.target.getAttribute('data-archivo-id');
    console.log("data-archivo-id:", archivoId);  // Verifica el valor aquí
    archivoArrastradoId = archivoId;
    console.log("hecho el ondragstart");
}


function onDragOver(event) {
    event.preventDefault(); // Necesario para permitir el drop
    console.log("hecho el ondragover");
}

function onDrop(event) {
    event.preventDefault();
    const carpetaId = event.currentTarget.getAttribute('data-carpeta-id');
    console.log("estamos en el ondrop");
    console.log("archivoArrastradoId:", archivoArrastradoId);
    console.log("carpetaId:", carpetaId);
    console.log("miObjetoDotNet:", window.miObjetoDotNet);
    if (archivoArrastradoId && carpetaId && window.miObjetoDotNet) {
        console.log("Vamos a llamar a la funcion moverarchivoacarpeta");
        window.miObjetoDotNet.invokeMethodAsync('MoverArchivoACarpeta', parseInt(archivoArrastradoId), parseInt(carpetaId))
            .then(() => {
                console.log('Archivo movido correctamente');
            })
            .catch(err => console.error('Error al invocar método .NET', err));
    }
}

