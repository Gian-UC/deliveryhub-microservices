# ====== STAGE 1: build ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o csproj primeiro (melhora cache)
COPY *.csproj ./
RUN dotnet restore

# Copia o restante do código
COPY . ./

# Publica em modo Release
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ====== STAGE 2: runtime ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Porta interna padrão do container
EXPOSE 8080

# variável para o Kestrel ouvir em 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DeliveryHub.Pedidos.Api.dll"]
