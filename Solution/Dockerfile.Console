#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

ENV ConnectionString="Host=localhost;Port=5432;Username=userNpg;Password=passNpg;Database=postgres" \
    ApiUrl="https://api.andbank.com.br"


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PracticeTestFoursys.Console/PracticeTestFoursys.Console.csproj", "PracticeTestFoursys.Console/"]
COPY ["PracticeTestFoursys.Application/PracticeTestFoursys.Application.csproj", "PracticeTestFoursys.Application/"]
COPY ["PracticeTestFoursys.Domain/PracticeTestFoursys.Domain.csproj", "PracticeTestFoursys.Domain/"]
COPY ["PracticeTestFoursys.Infra/PracticeTestFoursys.Infra.csproj", "PracticeTestFoursys.Infra/"]
RUN dotnet restore "./PracticeTestFoursys.Console/PracticeTestFoursys.Console.csproj"
COPY . .
WORKDIR "/src/PracticeTestFoursys.Console"
RUN dotnet build "./PracticeTestFoursys.Console.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PracticeTestFoursys.Console.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PracticeTestFoursys.Console.dll"]