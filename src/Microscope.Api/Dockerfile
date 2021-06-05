FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

WORKDIR /src

COPY ["src/Microscope.Api/Microscope.Api.csproj", "src/Microscope.Api/"]
COPY ["src/Microscope.Hasura/Microscope.Hasura.csproj", "src/Microscope.Hasura/"]
COPY ["src/Microscope.Domain/Microscope.Domain.csproj", "src/Microscope.Domain/"]
COPY ["src/Microscope.Infrastructure/Microscope.Infrastructure.csproj", "src/Microscope.Infrastructure/"]

RUN dotnet restore "src/Microscope.Api/Microscope.Api.csproj"

COPY . .

WORKDIR "/src/src/Microscope.Api"

RUN dotnet build "Microscope.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Microscope.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Microscope.Api.dll"]