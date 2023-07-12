function handleImageUpload(fileInput) {
    const file = fileInput.files[0];

    const formData = new FormData();
    if (file) {
        formData.append("image", file);
    } else {
        formData.append("image", 0); // Sets to 0 if no file is uploaded
    }

    formData.append("workplaceId", document.querySelector('#workplaceSelect').value);
    formData.append("date", document.querySelector('#DateInput').value);
    formData.append("hours", document.querySelector('#HoursInput').value);
    formData.append("info", document.querySelector('#InfoInput').value);

    const tcs = new Promise((resolve, reject) => {
        fetch("/api/create", {
            method: "POST",
            body: formData,
        })
            .then((response) => response.json())
            .then((data) => {
                if (Object.keys(data).length !== 0) {
                    resolve(data);
                } else {
                    reject(new Error("Empty response"));
                }
            })
            .catch((error) => {
                reject(error);
            });
    });

    return tcs;
}

window.handleImageUpload = handleImageUpload;
