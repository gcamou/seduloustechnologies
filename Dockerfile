# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore BankingDemo/BankingDemo.csproj --disable-parallel
RUN dotnet publish BankingDemo/BankingDemo.csproj -c release -o /app

# Server Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT [ "dotnet", "BankingDemo.dll"]