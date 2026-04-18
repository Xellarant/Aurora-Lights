window.auroraDownloads = {
    downloadFile(fileName, contentType, base64Content) {
        const bytes = Uint8Array.from(atob(base64Content), c => c.charCodeAt(0));
        const blob = new Blob([bytes], { type: contentType });
        const url = URL.createObjectURL(blob);
        const link = document.createElement("a");
        link.href = url;
        link.download = fileName;
        document.body.appendChild(link);
        link.click();
        link.remove();
        URL.revokeObjectURL(url);
    }
};
