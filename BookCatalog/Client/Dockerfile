
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Копируем csproj файл и восстанавливаем зависимости
COPY ["BookCatalog/Client/BookCatalog.Client.csproj", "BookCatalog/Client/"]
RUN dotnet restore "BookCatalog/Client/BookCatalog.Client.csproj"
COPY . .

# Сборка проекта
RUN dotnet build "BookCatalog/Client/BookCatalog.Client.csproj" -c Release -o /app/build

# Публикуем проект
FROM build AS publish
RUN dotnet publish "BookCatalog/Client/BookCatalog.Client.csproj" -c Release -o /app/publish

# Используем nginx для обслуживания статических файлов
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY BookCatalog/Client/nginx.conf /etc/nginx/conf.d/default.conf

# Expose порт 80 для nginx
EXPOSE 80

# Запускаем nginx
CMD ["nginx", "-g", "daemon off;"]


