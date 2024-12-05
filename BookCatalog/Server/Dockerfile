FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["BookCatalog/Server/BookCatalog.Server.csproj", "BookCatalog/Server/"]
RUN dotnet restore "BookCatalog/Server/BookCatalog.Server.csproj"
COPY . .

RUN dotnet build "BookCatalog/Server/BookCatalog.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookCatalog/Server/BookCatalog.Server.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BookCatalog.Server.dll"]
