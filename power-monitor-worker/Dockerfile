#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["power-monitor-worker/power-monitor-worker.csproj", "power-monitor-worker/"]
RUN dotnet restore "power-monitor-worker/power-monitor-worker.csproj"
COPY . .
WORKDIR "/src/power-monitor-worker"
RUN dotnet build "power-monitor-worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "power-monitor-worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "power-monitor-worker.dll"]