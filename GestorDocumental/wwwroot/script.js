window.abrirSelectorCarpeta = async () => {
    try {
        const handle = await window.showDirectoryPicker();
        const nombreCarpeta = handle.name;
        const archivos = [];

        for await (const entry of handle.values()) {
            if (entry.kind === "file") {
                const file = await entry.getFile();
                const contenido = await file.arrayBuffer();
                archivos.push({
                    nombre: file.name,
                    tipo: file.type,
                    contenido: Array.from(new Uint8Array(contenido)),
                    tamaño: file.size
                });
            }
        }

        console.log("Carpeta seleccionada:", nombreCarpeta);
        console.log("Archivos encontrados:", archivos.length);
        return { nombreCarpeta, archivos };

    } catch (error) {
        console.error("Error al seleccionar la carpeta:", error);
        return null;
    }
};
