FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/Gadget.Web/Gadget.Web.csproj src/Gadget.Web/
RUN dotnet restore src/Gadget.Web/Gadget.Web.csproj
COPY . .
WORKDIR /src/src/Gadget.Web
RUN dotnet build Gadget.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Gadget.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gadget.Web.dll"]
