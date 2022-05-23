#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["power-monitor-ui/power-monitor-ui.csproj", "power-monitor-ui/"]
RUN dotnet restore "power-monitor-ui/power-monitor-ui.csproj"
COPY . .
WORKDIR "/src/power-monitor-ui"
RUN dotnet build "power-monitor-ui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "power-monitor-ui.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "power-monitor-ui.dll"]