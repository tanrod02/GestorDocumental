window.registrarObjetoDotNet = function (dotNetHelper) {
    window.miObjetoDotNet = dotNetHelper;
}

let archivoArrastradoId = null;

function onDragStart(event) {
    const archivoId = event.target.getAttribute('data-archivo-id');
    archivoArrastradoId = archivoId;
}


function onDragOver(event) {
    event.preventDefault(); 
    console.log("hecho el ondragover");
}

function onDrop(event) {
    event.preventDefault();
    const carpetaId = event.currentTarget.getAttribute('data-carpeta-id');
    if (archivoArrastradoId && carpetaId && window.miObjetoDotNet) {
        window.miObjetoDotNet.invokeMethodAsync('MoverArchivoACarpeta', parseInt(archivoArrastradoId), parseInt(carpetaId))
            .then(() => {
                console.log('Archivo movido correctamente');
            })
            .catch(err => console.error('Error al invocar método .NET', err));
    }
}

