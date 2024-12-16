export function scrollToElement(elementId) {
    const elm = document.getElementById(elementId);
    if (elm) {
        console.debug("Auto scrolling to " + elementId)
        elm.scrollIntoView({behavior: 'smooth'});
    }
}