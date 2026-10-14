FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ShopFlowAPI.csproj ./
RUN dotnet restore ShopFlowAPI.csproj
COPY . .
RUN dotnet publish ShopFlowAPI.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
# Render expects the app on port 10000 by default
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "ShopFlowAPI.dll"]
