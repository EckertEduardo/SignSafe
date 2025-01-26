#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SignSafe.Presentation/SignSafe.Presentation.csproj", "SignSafe.Presentation/"]
COPY ["SignSafe.Ioc/SignSafe.Ioc.csproj", "SignSafe.Ioc/"]
COPY ["SignSafe.Application/SignSafe.Application.csproj", "SignSafe.Application/"]
COPY ["SignSafe.Data/SignSafe.Data.csproj", "SignSafe.Data/"]
COPY ["SignSafe.Domain/SignSafe.Domain.csproj", "SignSafe.Domain/"]
RUN dotnet restore "./SignSafe.Presentation/SignSafe.Presentation.csproj"
COPY . .
WORKDIR "/src/SignSafe.Presentation"
RUN dotnet build "./SignSafe.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SignSafe.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignSafe.Presentation.dll"]