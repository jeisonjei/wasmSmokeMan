#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Server/wasmSmokeMan.Server.csproj", "Server/"]
COPY ["Shared/wasmSmokeMan.Shared.csproj", "Shared/"]
COPY ["Client/wasmSmokeMan.Client.csproj", "Client/"]
RUN dotnet restore "Server/wasmSmokeMan.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "wasmSmokeMan.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wasmSmokeMan.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wasmSmokeMan.Server.dll"]