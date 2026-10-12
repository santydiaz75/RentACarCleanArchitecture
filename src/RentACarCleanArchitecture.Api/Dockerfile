FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /
COPY ["src/RentACarCleanArchitecture.Domain/RentACarCleanArchitecture.Domain.csproj", "src/RentACarCleanArchitecture.Domain/"]
COPY ["src/RentACarCleanArchitecture.Application/RentACarCleanArchitecture.Application.csproj", "src/RentACarCleanArchitecture.Application/"]
COPY ["src/RentACarCleanArchitecture.Infrastructure/RentACarCleanArchitecture.Infrastructure.csproj", "src/RentACarCleanArchitecture.Infrastructure/"]
COPY ["src/RentACarCleanArchitecture.Api/RentACarCleanArchitecture.Api.csproj", "src/RentACarCleanArchitecture.Api/"]
RUN dotnet restore "src/RentACarCleanArchitecture.Api/RentACarCleanArchitecture.Api.csproj"
COPY . .
WORKDIR "/src/RentACarCleanArchitecture.Api"
RUN dotnet build "RentACarCleanArchitecture.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "RentACarCleanArchitecture.Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentACarCleanArchitecture.Api.dll"]
