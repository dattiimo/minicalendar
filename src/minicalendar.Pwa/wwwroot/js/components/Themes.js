function applyTheme(theme) {
    if (theme === 'auto') {
        document.documentElement.setAttribute('data-bs-theme', getPreferredTheme())
    } else {
        document.documentElement.setAttribute('data-bs-theme', theme)
    }
}

function getPreferredTheme() {
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
}