FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Microscope.STS.Identity/Microscope.STS.Identity.csproj", "src/Microscope.STS.Identity/"]
COPY ["src/Microscope.Admin.EntityFramework.Shared/Microscope.Admin.EntityFramework.Shared.csproj", "src/Microscope.Admin.EntityFramework.Shared/"]
COPY ["src/Microscope.Admin.EntityFramework.SqlServer/Microscope.Admin.EntityFramework.SqlServer.csproj", "src/Microscope.Admin.EntityFramework.SqlServer/"]
COPY ["src/Microscope.Admin.EntityFramework.PostgreSQL/Microscope.Admin.EntityFramework.PostgreSQL.csproj", "src/Microscope.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/Microscope.Admin.EntityFramework.MySql/Microscope.Admin.EntityFramework.MySql.csproj", "src/Microscope.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/Microscope.STS.Identity/Microscope.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/Microscope.STS.Identity"
RUN dotnet build "Microscope.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microscope.STS.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Microscope.STS.Identity.dll"]