# Используем официальный образ .NET 7 SDK
#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["BookCatalog/Server/BookCatalog.Server.csproj", "BookCatalog/Server/"]
#RUN dotnet restore "BookCatalog/Server/BookCatalog.Server.csproj"
#COPY . .
#WORKDIR "/src/Server"
#RUN dotnet build "BookCatalog.Server.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "BookCatalog.Server.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "BookCatalog.Server.dll"]


# Используем образ .NET SDK для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Копируем csproj файл и восстанавливаем зависимости
COPY ["BookCatalog/Server/BookCatalog.Server.csproj", "BookCatalog/Server/"]
RUN dotnet restore "BookCatalog/Server/BookCatalog.Server.csproj"
COPY . .

# Сборка проекта
RUN dotnet build "BookCatalog/Server/BookCatalog.Server.csproj" -c Release -o /app/build

# Публикуем проект
FROM build AS publish
RUN dotnet publish "BookCatalog/Server/BookCatalog.Server.csproj" -c Release -o /app/publish

# Запускаем с использованием базового образа .NET ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Копируем опубликованные файлы из предыдущего шага
COPY --from=publish /app/publish .

# Точка входа для приложения
ENTRYPOINT ["dotnet", "BookCatalog.Server.dll"]
