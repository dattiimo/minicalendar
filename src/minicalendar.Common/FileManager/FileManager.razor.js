function decodeBase64(bytesBase64) {
    const binary = atob(bytesBase64);
    const bytes = new Uint8Array(binary.length);

    for (let index = 0; index < binary.length; index++) {
        bytes[index] = binary.charCodeAt(index);
    }

    return bytes;
}

function isMobileOrStandalone() {
    const isStandalone = window.matchMedia('(display-mode: standalone)').matches
        || window.navigator.standalone === true;
    const isMobile = window.navigator.userAgentData?.mobile === true
        || /Android|iPhone|iPad|iPod/i.test(window.navigator.userAgent)
        || (/Macintosh/i.test(window.navigator.userAgent) && window.navigator.maxTouchPoints > 1);

    return isStandalone || isMobile;
}

export async function common_fileManager_saveAsFile(filename, bytesBase64, contentType) {
    const bytes = decodeBase64(bytesBase64);
    const file = new File([bytes], filename, { type: contentType });
    const shareData = { files: [file] };

    if (isMobileOrStandalone() && navigator.share && navigator.canShare?.(shareData)) {
        try {
            await navigator.share(shareData);
            return;
        } catch (error) {
            // Cancelling the native share sheet is an intentional end to the action.
            if (error?.name === 'AbortError') {
                return;
            }

            // Fall back to a browser download when sharing fails for another reason.
        }
    }

    const objectUrl = URL.createObjectURL(file);
    const link = document.createElement('a');
    link.download = filename;
    link.href = objectUrl;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
    setTimeout(() => URL.revokeObjectURL(objectUrl), 1000);
}
