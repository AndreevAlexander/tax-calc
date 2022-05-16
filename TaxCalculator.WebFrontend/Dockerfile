FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaxCalculator.WebFrontend/TaxCalculator.WebFrontend.csproj", "TaxCalculator.WebFrontend/"]
RUN dotnet restore "TaxCalculator.WebFrontend/TaxCalculator.WebFrontend.csproj"
COPY . .
WORKDIR "/src/TaxCalculator.WebFrontend"
RUN dotnet build "TaxCalculator.WebFrontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxCalculator.WebFrontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxCalculator.WebFrontend.dll"]
