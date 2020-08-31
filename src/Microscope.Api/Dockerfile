FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Microscope.Api/Microscope.Api.csproj", "src/Microscope.Api/"]
COPY ["src/Microscope.EntityFramework.Shared/Microscope.EntityFramework.Shared.csproj", "src/Microscope.EntityFramework.Shared/"]
COPY ["src/Microscope.EntityFramework.SqlServer/Microscope.EntityFramework.SqlServer.csproj", "src/Microscope.EntityFramework.SqlServer/"]
COPY ["src/Microscope.EntityFramework.PostgreSQL/Microscope.EntityFramework.PostgreSQL.csproj", "src/Microscope.EntityFramework.PostgreSQL/"]
COPY ["src/Microscope.EntityFramework.MySql/Microscope.EntityFramework.MySql.csproj", "src/Microscope.EntityFramework.MySql/"]
RUN dotnet restore "src/Microscope.Api/Microscope.Api.csproj"
COPY . .
WORKDIR "/src/src/Microscope.Api"
RUN dotnet build "Microscope.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microscope.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Microscope.Api.dll"]