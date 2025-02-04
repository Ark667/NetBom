#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NetBom.Cli/NetBom.Cli.csproj", "NetBom.Cli/"]
COPY ["NetBom.Core/NetBom.Core.csproj", "NetBom.Core/"]
RUN dotnet restore "NetBom.Cli/NetBom.Cli.csproj"
COPY . .
WORKDIR "/src/NetBom.Cli"
RUN dotnet build "NetBom.Cli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetBom.Cli.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetBom.Cli.dll"]