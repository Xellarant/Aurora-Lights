window.auroraDownloads = {
    async downloadFile(fileName, contentType, base64Content) {
        const bytes = Uint8Array.from(atob(base64Content), c => c.charCodeAt(0));
        const blob = new Blob([bytes], { type: contentType });

        // Prefer the File System Access API save picker when available (Chromium browsers).
        // This always shows a native "Save As" dialog regardless of the browser's default
        // download location setting. Falls back to the blob+anchor path on other browsers,
        // which respects the browser's own "ask where to save" preference.
        if (typeof window.showSaveFilePicker === "function") {
            try {
                const ext = fileName.includes(".") ? fileName.substring(fileName.lastIndexOf(".")) : "";
                const options = {
                    suggestedName: fileName,
                };
                if (ext) {
                    options.types = [{
                        description: contentType || "File",
                        accept: { [contentType || "application/octet-stream"]: [ext] },
                    }];
                }
                const handle = await window.showSaveFilePicker(options);
                const writable = await handle.createWritable();
                await writable.write(blob);
                await writable.close();
                return;
            } catch (err) {
                // AbortError = user cancelled the picker. Swallow silently.
                if (err && err.name === "AbortError") return;
                // Any other error (permission denied, unsupported filesystem) falls through
                // to the anchor-based download below.
            }
        }

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
