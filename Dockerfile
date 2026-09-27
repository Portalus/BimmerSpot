FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY BimmerSpot.slnx ./
COPY BimmerSpot/BimmerSpot.csproj BimmerSpot/

RUN dotnet restore BimmerSpot/BimmerSpot.csproj

COPY BimmerSpot/ BimmerSpot/

WORKDIR /src/BimmerSpot

RUN dotnet publish BimmerSpot.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "BimmerSpot.dll"]