# League of Legends Web App

A League of Legends web application built with .NET as part of a university group project.

The application uses the official Riot Games API to retrieve League of Legends data and display information about summoners, ranked leagues, matches, champion mastery, Clash, tournaments, and champions.

## Project Overview

This repository contains a multi-project .NET solution with a Web API, Blazor Server frontend, database projects, a background job project, and tests.

The project was made for a .NET university class and is kept as a portfolio project to show experience with C#, ASP.NET Core Web API, external API integration, database modelling, and Blazor.

## My Main Contributions

My main contributions were focused on the API part of the project and the champion database.

I worked especially on:

* Building several ASP.NET Core Web API controllers
* Connecting the backend to the official Riot Games API
* Creating API routes for summoner, league, match, champion mastery, Clash, player, and tournament data
* Handling HTTP requests and responses from Riot API endpoints
* Mapping Riot API responses into DTO models
* Adding basic error/response handling for unsuccessful API calls
* Using in-memory caching for champion mastery requests
* Creating the champion database structure
* Modelling champion data such as names, IDs, titles, descriptions, images, tags, info values, and detailed stats
* Setting up the database model structure for storing League of Legends champion data

## Features

### Riot API Integration

The Web API communicates with Riot API endpoints to retrieve data such as:

* Summoner information by summoner name
* Ranked league entries
* Champion mastery for a summoner
* Champion mastery for a specific champion
* Match data by match ID
* Match ID lists by PUUID
* Clash player data
* Clash team data
* Clash tournament data
* Tournament data
* Player data

### Champion Database

The project includes a champion database structure for storing detailed League of Legends champion information.

The champion data includes:

* Champion name
* Champion ID and key
* Version
* Title
* Description/blurb
* Tags
* Resource type
* Image information
* Attack, defence, magic, and difficulty values
* Stats such as health, mana, movement speed, armour, attack damage, attack speed, regeneration, and scaling values

### Blazor Server Frontend

The solution also includes a Blazor Server project for the web application frontend.

### Background Job Project

The repository includes a background job project intended for automated or scheduled work related to API/database updates.

### Swagger

The Web API is configured with Swagger/OpenAPI support during development, making it easier to inspect and test the API routes.

## Tech Stack

* C#
* .NET 6
* ASP.NET Core Web API
* Blazor Server
* Entity Framework Core
* SQL Server
* Riot Games API
* Swagger / OpenAPI
* In-memory caching
* Azure Pipelines
* MIT License

## Solution Structure

```text id="cq7i8u"
LeagueOfLegendsWebsite/
│
├── HIOF.Net.Gruppe9.LeagueWebAPI/
│   └── ASP.NET Core Web API for Riot API integration
│
├── HIOF.Net.Gruppe9.BlazorServer/
│   └── Blazor Server frontend
│
├── HIOF.Gruppe9.LeagueChampionDB/
│   └── Champion database project
│
├── HIOF.Gruppe9.LeagueChampionDB.Data/
│   └── Champion database data models and context
│
├── HIOF.Net.Gruppe9.LeaugeVersionDB/
│   └── League version database project
│
├── HIOF.Net.Gruppe9.LeaugeVersionDB.Data/
│   └── League version database data models and context
│
├── HIOF.Net.Gruppe9.LeagueWebAPI.BackgroundJob/
│   └── Background job project
│
├── HIOF.Net.Gruppe9.LeagueWebAPI.Test/
│   └── Web API test project
│
└── HIOF.Net.Gruppe9.sln
```

## Example API Routes

Some of the API routes used in the project include:

```http id="8702d2"
GET /api/1.0/Summoner/{summonerName}
GET /api/1.0/League/{summonerName}
GET /api/1.0/ChampionMastery/{summonerName}
GET /api/1.0/ChampionMastery/{summonerName}/{championId}
GET /api/1.0/Match/{matchId}
GET /api/1.0/MatchIdListByPuuid/{puuid}
GET /api/1.0/ClashTeam/Player/{summonerName}
GET /api/1.0/ClashTeam/Team/{teamId}
GET /api/1.0/ClashTeam/Tournament/{tournamentId}
GET /api/1.0/Tournament/{tournamentId}
GET /api/1.0/Players/{summonerId}
```

## API Key / Configuration Note

This project uses the Riot Games API, which requires an API key to make requests.

In the current version of the project, the API key is represented directly in the controller code as a string placeholder, for example:

```csharp id="4vsled"
client.DefaultRequestHeaders.Add("X-Riot-Token", "apiKey");
```

This was a simple solution used during the university project. A better solution would be to move the API key into configuration instead of keeping it directly in the source code.

A more production-ready approach would be to store the key using:

* `appsettings.Development.json`
* .NET user secrets
* Environment variables
* A deployment secret store

The controller could then read the key from configuration and attach it to the Riot API request header.

## Getting Started

### Prerequisites

To run the project locally, you need:

* Visual Studio 2022 or newer
* .NET 6 SDK
* SQL Server or SQL Server LocalDB
* A Riot Games API key

### Setup

Clone the repository:

```bash id="fh2332"
git clone https://github.com/Bri4nHoll4nd/LeagueOfLegendsWebsite.git
cd LeagueOfLegendsWebsite
```

Restore dependencies:

```bash id="thz1k6"
dotnet restore
```

Build the solution:

```bash id="g0nkna"
dotnet build
```

Run the Web API project:

```bash id="pf1rf8"
dotnet run --project HIOF.Net.Gruppe9.LeagueWebAPI
```

When running in development mode, Swagger can be used to inspect and test the available API endpoints.

## Documentation

A separate document called `documentation` will give a more detailed explanation of the whole project, including the project structure, API work, database work, and how the different parts of the application fit together.

## What I Learned

This project gave practical experience with:

* Building APIs with ASP.NET Core
* Working with external REST APIs
* Using `HttpClient` through `IHttpClientFactory`
* Structuring API controllers and DTOs
* Handling API responses and errors
* Working with League of Legends data from the Riot API
* Creating database models for real external data
* Using Entity Framework Core for database modelling
* Working in a multi-project .NET solution
* Connecting backend API work with a Blazor frontend
* Understanding why API keys and secrets should be moved into configuration instead of being stored directly in source code

## Project Status

This project was created as part of a university course and is no longer actively developed as a production application.

It is kept as a portfolio project to show experience with:

* C#
* .NET
* ASP.NET Core Web API
* External API integration
* Database modelling
* Blazor
* Structured multi-project solutions
