FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 23927

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/Gadget.Api/Gadget.Api.csproj src/Gadget.Api/
RUN dotnet restore src/Gadget.Api/Gadget.Api.csproj
COPY . .
WORKDIR /src/src/Gadget.Api
RUN dotnet build Gadget.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Gadget.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gadget.Api.dll"]
