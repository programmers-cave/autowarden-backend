FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AutoWarden.API/AutoWarden.csproj", "AutoWarden.API/"]
RUN dotnet restore "AutoWarden.API/AutoWarden.API.csproj"
COPY . .
WORKDIR "/src/AutoWarden.API"
RUN dotnet build "AutoWarden.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoWarden.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoWarden.API.dll"]
