# Stage 1: Build (La forge)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copie des fichiers projet et restore des dépendances (Typage fort respecté ici, c'est du C# après tout)
COPY ["EfghDev/EfghDev.csproj", "EfghDev/"]
RUN dotnet restore "EfghDev/EfghDev.csproj"

# Copie du reste et build
COPY . .
WORKDIR "/src/EfghDev"
RUN dotnet build "EfghDev.csproj" -c Release -o /app/build

# Publish (Loot final)
FROM build AS publish
RUN dotnet publish "EfghDev.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime (Le moteur de jeu, léger et rapide)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EfghDev.dll"]