# mini calendar

A minimilistic calendar to aid visualisation of repeated activities across the span of a year.

PWA app available at [my.minicalendar.app](https://my.minicalendar.app). Full details at [minicalendar.app](https://minicalendar.app).


## Table of Contents
- [Getting Started](#getting-started)
- [Tests](#tests)
- [Contributing](#contributing)
- [License](#licence)


## Getting Started ⚙️

Requires [Microsoft .NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

### PWA

Clone the project to a local directory, switch to the PWA project directory, `src\minicalendar.Pwa`, and then just `dotnet run`. 

From the root directory:

```powershell
cd src\minicalendar.Pwa
dotnet run
```

For local development, use

```powershell
dotnet watch run
```

### Tailwind

To build tailwind styles

```powershell
# from the repository root directory
npx tailwindcss -i ./src/minicalendar.Pwa/styles/tailwind.css -o ./src/minicalendar.Pwa/wwwroot/css/tailwind.css --watch
```


## Tests

Tests are located in `tests` directory for each project.

```powershell
cd tests\minicalendar.Pwa.Tests
dotnet test
```


## Contributing ✏️

Additions, improvements and feedback are all welcome.


## Licence ⚖️

This project is licensed under the [MIT Licence](https://github.com/dattiimo/minicalendar/blob/main/LICENSE)
