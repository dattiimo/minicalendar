/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./**/*.{razor,html,cshtml}'],
  theme: {
    extend: {},
  },
  plugins: [
    function({ addVariant }) {
      addVariant('vport', '.view-portrait &');
      addVariant('vland', '.view-landscape &');
      addVariant('expand', '.expanded &');
      addVariant('collapse', '.collapsed &');
    }
  ],
  safelist: [
    { 
      pattern: /bg-(green|purple|cyan|indigo|yellow|red)-(200|400|500)/, // generated colours
    },
    {
      pattern: /border-(green|purple|cyan|indigo|yellow|red)-(200|400|500)/, // generated colours
    },
    {
      pattern: /border-(l|r|t|b|s|e)-(green|purple|cyan|indigo|yellow|red)-(200|400|500)/, // generated colours
    },
    {
      pattern: /ring-(green|purple|cyan|indigo|yellow|red)-(200|400|500)/, // generated colours
    },
    { pattern: /border-white/ },
    { pattern: /ring-white/ }
  ]
}

