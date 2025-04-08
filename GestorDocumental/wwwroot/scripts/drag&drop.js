window.addEventListener("DOMContentLoaded", (event) => {
    let draggedItem = null;

    window.handleDragStart = (e, id) => {
        draggedItem = id;
        e.dataTransfer.setData("text", id.toString());  // Convertir a string
    };


    // Función para permitir el drop
    window.handleDragOver = (e) => {
        e.preventDefault();
    };

    window.handleDrop = (e, dropTargetId) => {
        e.preventDefault();
        const archivoId = parseInt(e.dataTransfer.getData("text"));  // Convertir de vuelta a int

        if (!isNaN(archivoId)) {
            // Llamar a la función de Blazor para cambiar la carpeta del archivo
            DotNet.invokeMethodAsync('TuNombreDeProyecto', 'MoverArchivo', archivoId, dropTargetId)
                .then(result => {
                    console.log("Archivo movido correctamente");
                }).catch(error => {
                    console.error("Error moviendo archivo", error);
                });
        }
    };

});
