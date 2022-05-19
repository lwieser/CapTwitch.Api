#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CapTwitch.Api/CapTwitch.Api.csproj", "CapTwitch.Api/"]
COPY ["CapTwitch.Services/CapTwitch.Services.csproj", "CapTwitch.Services/"]
COPY ["CapTwitch.Model/CapTwitch.Model.csproj", "CapTwitch.Model/"]
RUN dotnet restore "CapTwitch.Api/CapTwitch.Api.csproj"
COPY . .
WORKDIR "/src/CapTwitch.Api"
RUN dotnet build "CapTwitch.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CapTwitch.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CapTwitch.Api.dll"]