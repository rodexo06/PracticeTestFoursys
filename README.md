<p align="center">
  <img src="https://cdn-icons-png.flaticon.com/512/6295/6295417.png" width="100" />
</p>
<p align="center">
    <h1 align="center">PRACTICE TEST FOURSYS</h1>
</p>
<p align="center">
    <em>Teste para vaga de .NET Developer</em>
</p>
<p align="center">
	<img src="https://img.shields.io/github/license/rodexo06/PracticeTestFoursys?style=flat&color=0080ff" alt="license">
	<img src="https://img.shields.io/github/last-commit/rodexo06/PracticeTestFoursys?style=flat&logo=git&logoColor=white&color=0080ff" alt="last-commit">
	<img src="https://img.shields.io/github/languages/top/rodexo06/PracticeTestFoursys?style=flat&color=0080ff" alt="repo-top-language">
	<img src="https://img.shields.io/github/languages/count/rodexo06/PracticeTestFoursys?style=flat&color=0080ff" alt="repo-language-count">
<p>
<p align="center">
		<em>Developed with the software and tools below.</em>
</p>
<p align="center">
	<img src="https://img.shields.io/badge/EditorConfig-FEFEFE.svg?style=flat&logo=EditorConfig&logoColor=black" alt="EditorConfig">
	<img src="https://img.shields.io/badge/Docker-2496ED.svg?style=flat&logo=Docker&logoColor=white" alt="Docker">
	<img src="https://img.shields.io/badge/JSON-000000.svg?style=flat&logo=JSON&logoColor=white" alt="JSON">
</p>
<hr>

##  Quick Links

> - [ Overview](#-overview)
> - [ Features](#-features)
> - [ Repository Structure](#-repository-structure)
> - [ Modules](#-modules)
> - [ Getting Started](#-getting-started)
>   - [ Installation](#-installation)
>   - [ Running PracticeTestFoursys](#-running-PracticeTestFoursys)
>   - [ Tests](#-tests)
> - [ Project Roadmap](#-project-roadmap)
> - [ Contributing](#-contributing)
> - [ License](#-license)
> - [ Acknowledgments](#-acknowledgments)

---

##  Overview

HTTP error 401 for prompt `overview`

---

##  Features

HTTP error 401 for prompt `features`

---

##  Repository Structure

```sh
└── PracticeTestFoursys/
    ├── README.md
    └── Solution
        ├── .dockerignore
        ├── .editorconfig
        ├── PracticeTestFoursys.Api
        │   ├── Controllers
        │   │   ├── WeatherForecastController.cs
        │   │   └── _Base
        │   │       └── ApiControllerBase.cs
        │   ├── Dockerfile
        │   ├── PracticeTestFoursys.Api.csproj
        │   ├── PracticeTestFoursys.Api.http
        │   ├── Program.cs
        │   ├── Properties
        │   │   └── launchSettings.json
        │   ├── WeatherForecast.cs
        │   ├── appsettings.Development.json
        │   └── appsettings.json
        ├── PracticeTestFoursys.Application
        │   ├── Commands
        │   │   └── _Base
        │   │       ├── CommandHandlerBase.cs
        │   │       ├── IResponseState.cs
        │   │       └── ResponseState.cs
        │   ├── DependenciesInjections
        │   │   ├── AllowMultipleInjectionAttribute.cs
        │   │   ├── IServiceCollectionExtensions.cs
        │   │   ├── InjectableAttribute.cs
        │   │   ├── ScopedLifetimeAttribute.cs
        │   │   ├── SingletonLifetimeAttribute.cs
        │   │   └── TransientLifetimeAttribute.cs
        │   ├── Mapping
        │   │   └── MappingProfile.cs
        │   ├── PracticeTestFoursys.Application.csproj
        │   ├── Repositories
        │   │   ├── DbParameter.cs
        │   │   └── IBaseRepository.cs
        │   └── ViewModels
        │       └── _Base
        │           ├── RequestResult.cs
        │           └── RequestResultErrorItem.cs
        ├── PracticeTestFoursys.Domain
        │   └── PracticeTestFoursys.Domain.csproj
        ├── PracticeTestFoursys.Infra
        │   ├── Context
        │   │   └── PracticeTestFoursysContext.cs
        │   ├── DependencyInjection.cs
        │   ├── PracticeTestFoursys.Infra.csproj
        │   ├── Repositories
        │   │   └── BaseRepository.cs
        │   └── Utils
        │       └── DbParameter.cs
        └── PracticeTestFoursys.sln
```

---

##  Getting Started

***Requirements***

Ensure you have the following dependencies installed on your system:

* **.NET CORE**: `version 8.0`

###  Installation

1. Clone the PracticeTestFoursys repository:

```sh
git clone https://github.com/rodexo06/PracticeTestFoursys
```

2. Change to the project directory:

```sh
cd PracticeTestFoursys
```

3. Install the dependencies:

```sh
dotnet build
```

###  Running PracticeTestFoursys

Use the following command to run PracticeTestFoursys:

```sh
docker-compose up -d
```

---
