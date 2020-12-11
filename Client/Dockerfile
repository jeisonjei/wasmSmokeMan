#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Client/wasmSmokeMan.Client.csproj", "Client/"]
COPY ["Shared/wasmSmokeMan.Shared.csproj", "Shared/"]
RUN dotnet restore "Client/wasmSmokeMan.Client.csproj"
COPY . .
WORKDIR "/src/Client"
RUN dotnet build "wasmSmokeMan.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wasmSmokeMan.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wasmSmokeMan.Client.dll"]