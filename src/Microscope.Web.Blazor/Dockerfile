FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

WORKDIR /src

COPY ["src/Microscope.Admin/Microscope.Admin.csproj", "src/Microscope.Admin/"]
COPY ["src/Microscope.Domain/Microscope.Domain.csproj", "src/Microscope.Domain/"]

RUN dotnet restore "src/Microscope.Admin/Microscope.Admin.csproj"

COPY . .

WORKDIR "/src/src/Microscope.Admin"

RUN dotnet build "Microscope.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microscope.Admin.csproj" -c Release -o /app/publish

FROM nginx:alpine

WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY src/Microscope.Admin/nginx.conf /etc/nginx/nginx.conf
