#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DocumentBroker/DocumentBroker.csproj", "DocumentBroker/"]
RUN dotnet restore "DocumentBroker/DocumentBroker.csproj"
COPY . .
WORKDIR "/src/DocumentBroker"
RUN dotnet build "DocumentBroker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocumentBroker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentBroker.dll"]

FROM mcr.microsoft.com/mssql/server

RUN mkdir -p /usr/src/app

WORKDIR /usr/src/app
COPY . /usr/src/app

RUN chmod +x /usr/src/app/entrypoint.sh

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=MyVerySecurePassword123

COPY ./data /

ENTRYPOINT [ "/bin/bash", "entrypoint.sh" ]
CMD [ "/opt/mssql/bin/sqlservr" ]
