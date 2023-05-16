FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["turtle-bot.csproj", "./"]
RUN dotnet restore "turtle-bot.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "turtle-bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "turtle-bot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "turtle-bot.dll"]
