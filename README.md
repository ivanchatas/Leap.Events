# Leap.Events

## Overview

This project is built with a clear architectural separation, following a four-layer design to ensure maintainability, scalability, and separation of concerns. It leverages modern .NET technologies to deliver a robust and modular application.

## Architecture

The project is structured into the following four distinct layers:

* **Persistence**
* **Domain**
* **Application**
* **API**

Each layer serves a specific purpose, as detailed below.

### 1. Persistence Layer

This layer is responsible for all database connections and data access operations. It contains:

* **Base Repository (`BaseRepository`):** Implements a generic CRUD (Create, Read, Update, Delete) functionality applicable to all entities, utilizing a mix of NHibernate and LINQ.
* **Entity-Specific Repositories:** Individual repositories for each entity, inheriting from `BaseRepository`.
* **Entity Mappings:** Configuration for mapping each entity to its corresponding database table using FluentNHibernate.

### 2. Domain Layer

The Domain layer defines the core business entities (which typically correspond to database tables) and encapsulates the fundamental business rules of the application. It is also used for creating domain-specific contracts and interfaces.

### 3. Application Layer

This layer encapsulates the application's business logic and orchestrates operations between the Domain and Persistence layers. It is further divided into the following components:

* **Data (Dtos):**
    * Contains the models exposed via the API (both request and response models).
    * These models are mapped to the application's **Domain entities** using AutoMapper within each service method, ensuring a clear separation between the business logic and persistence concerns.
* **Interfaces:**
    * Defines contracts for services and repositories, promoting loose coupling.
* **Mapping:**
    * Contains AutoMapper configurations for mapping between the Data models and Domain entities.
* **Services:**
    * Provides a dedicated service for each entity, where the methods exposed by the API are developed and business rules are applied.
* **Wrapper:**
    * Includes generic objects used to standardize the response structure for all API endpoints. This ensures that API consumers consistently receive the same response format, with only the `Data` attribute varying as needed.

### 4. API Layer

The API layer is the entry point for external consumers, exposing the application's functionality through well-defined endpoints.

* Developed using **Controllers**, with typically one controller per entity.
* Each controller invokes methods from the **Services** layer to fulfill requests and returns structured responses.
* **Middleware** has been implemented for centralized exception handling, significantly reducing code replication across individual endpoint methods.

## Dependency Injection

Each of the layers (structured as separate projects) includes a dedicated `DependencyInjection` class. This class is responsible for configuring and implementing the dependency injection for that specific project's services and components.

## Getting Started

To run this project, no additional installations are required beyond a standard .NET development environment setup (.NET 8). Simply ensure your NuGet packages are updated to their latest versions.

## Key Technologies & Packages

* [**NHibernate**](https://nhibernate.info/): Object-Relational Mapper (ORM) for data persistence.
* [**FluentNHibernate**](https://fluentnhibernate.org/): A concise and fluent interface for NHibernate mapping.
* [**AutoMapper**](https://automapper.org/): A convention-based object-object mapper.
* **ASP.NET Core:** For building the web API.
* **[.NET 8](https://dotnet.microsoft.com/download/dotnet/)**: The primary framework versions used for development.

## Known Issues / Caveats

* **Event-TicketSales Mapping Conflict:** I made a mistake; I prioritized the `Event` and `TicketSales` reference over the `TicketSales.EventId` in the mapping, which is why I have a SQL query in the `GetTicketsByEventId` method (located in the `TicketSaleService`). When I did the mapping with FluentNHibernate I had several conflicts between Map and Reference, I should have done it through XML.
