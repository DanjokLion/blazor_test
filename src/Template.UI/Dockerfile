#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["NuGet.config", "."]
WORKDIR /src
COPY ["src/Template.UI/", "Template.UI/"]

RUN dotnet restore "Template.UI/Template.UI.csproj"
COPY . .
WORKDIR "/src/Template.UI"
RUN dotnet build "Template.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Template.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Template.UI.dll"]
