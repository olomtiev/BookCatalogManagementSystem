# BookCatalog Management System

## Description
This project is a book catalog management system built using Blazor and Docker for containerization. 
The system provides functionality for importing book data, displaying the book list, and managing metadata.

## Project Structure

1. **BookCatalog.Client** - The Blazor client-side application that interacts with the server and displays data to the user.
2. **BookCatalog.Server** - The server-side application that handles requests and manages data.
3. **Docker** - Used for containerizing the entire application, including both the client and server sides.

## Requirements

Before starting with the project, make sure you have the following tools installed:

- **.NET SDK** (version 7.0) 
- **Docker** 
- **Visual Studio** 

## Setup and Running

### Local Running without Docker

1. Clone the repository:

    ```bash
    git clone <your_repository_url>
    cd BookCatalogManagementSystem
    ```

2. Open the project in Visual Studio or via the command line and run the following commands to restore dependencies and build the project:

    ```bash
    dotnet restore
    dotnet build
    ```

3. Run the server and client:

    To run the server:

    ```bash
    dotnet run --project BookCatalog.Server
    ```

    To run the client:

    ```bash
    dotnet run --project BookCatalog.Client
    ```

### Build and Run with Docker Compose

To build and run the application using Docker Compose, execute the following command:

    ```bash 
    docker-compose up --build
    ```

## Deployment

To deploy on a server with Docker:

1. Ensure Docker is installed on the server.
2. Transfer the built Docker image or publish the application and run the necessary commands to start the container.
