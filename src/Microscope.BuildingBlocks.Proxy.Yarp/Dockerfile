FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Microscope.BuildingBlocks.Proxy.Yarp/Microscope.BuildingBlocks.Proxy.Yarp.csproj", "src/Microscope.BuildingBlocks.Proxy.Yarp/"]
RUN dotnet restore "src/Microscope.BuildingBlocks.Proxy.Yarp/Microscope.BuildingBlocks.Proxy.Yarp.csproj"
COPY . .
WORKDIR "/src/src/Microscope.BuildingBlocks.Proxy.Yarp"
RUN dotnet build "Microscope.BuildingBlocks.Proxy.Yarp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microscope.BuildingBlocks.Proxy.Yarp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microscope.BuildingBlocks.Proxy.Yarp.dll"]
