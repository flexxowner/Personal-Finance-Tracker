FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]

COPY ["FinanceTracker.Api/FinanceTracker.Api.csproj", "FinanceTracker.Api/"]
COPY ["FinanceTracker.Application/FinanceTracker.Application.csproj", "FinanceTracker.Application/"]
COPY ["FinanceTracker.Domain/FinanceTracker.Domain.csproj", "FinanceTracker.Domain/"]
COPY ["FinanceTracker.Infrastructure/FinanceTracker.Infrastructure.csproj", "FinanceTracker.Infrastructure/"]

RUN dotnet restore "FinanceTracker.Api/FinanceTracker.Api.csproj"

COPY . .

WORKDIR "/src/FinanceTracker.Api"
RUN dotnet publish "FinanceTracker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FinanceTracker.Api.dll"]