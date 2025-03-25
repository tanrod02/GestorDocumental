window.seleccionarCarpeta = async function () {
    return new Promise((resolve, reject) => {
        let input = document.createElement('input');
        input.type = 'file';
        input.webkitdirectory = true;
        input.multiple = true;

        input.onchange = (event) => {
            let files = event.target.files;
            if (files.length > 0) {
                let rutaCompleta = files[0].webkitRelativePath; // 📂 Obtiene el path
                let nombreCarpeta = rutaCompleta.split('/')[0]; // 🟢 Extrae solo el nombre

                let archivos = Array.from(files).map(file => file.name);

                resolve({ nombreCarpeta, archivos });
            } else {
                resolve(null);
            }
        };

        input.click();
    });
};
