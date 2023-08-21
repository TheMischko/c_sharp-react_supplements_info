# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./SupplementsServer.API/SupplementsServer.API.csproj" --disable-parallel
RUN dotnet publish "./SupplementsServer.API/SupplementsServer.API.csproj" -c release -o /app --no-restore


# Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./
COPY ./data.csv ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "SupplementsServer.API.dll"]