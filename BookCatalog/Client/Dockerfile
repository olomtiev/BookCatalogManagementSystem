
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["BookCatalog/Client/BookCatalog.Client.csproj", "BookCatalog/Client/"]
RUN dotnet restore "BookCatalog/Client/BookCatalog.Client.csproj"
COPY . .

RUN dotnet build "BookCatalog/Client/BookCatalog.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookCatalog/Client/BookCatalog.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY BookCatalog/Client/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]


