FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Hanabi/Hanabi.csproj", "Hanabi/"]
RUN dotnet restore "Hanabi/Hanabi.csproj"
COPY . .
WORKDIR "/src/Hanabi"
RUN dotnet build "Hanabi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hanabi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hanabi.dll"]
