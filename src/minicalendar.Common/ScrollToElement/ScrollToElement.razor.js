export function scrollToElement(elementId) {
    const elm = document.getElementById(elementId);
    if (elm) {
        console.info("Scrolling to " + elementId)
        elm.scrollIntoView({behavior: 'smooth'});
    }
}