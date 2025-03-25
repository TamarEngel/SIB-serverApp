#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApi/Web.Api.csproj", "WebApi/"]
COPY ["web.Core/web.Core.csproj", "web.Core/"]
COPY ["web.Data/web.Data.csproj", "web.Data/"]
COPY ["web.Service/web.Service.csproj", "web.Service/"]
RUN dotnet restore "WebApi/Web.Api.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.dll"]