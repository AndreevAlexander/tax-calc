FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaxCalculator/TaxCalculator.csproj", "TaxCalculator/"]
RUN dotnet restore "TaxCalculator/TaxCalculator.csproj"
COPY . .
WORKDIR "/src/TaxCalculator"
RUN dotnet build "TaxCalculator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxCalculator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxCalculator.dll"]
