FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TfgNetMvc.Web/TfgNetMvc.Web.csproj TfgNetMvc.Web/
COPY TfgNetMvc.Application/TfgNetMvc.Application.csproj TfgNetMvc.Application/
COPY TfgNetMvc.Domain/TfgNetMvc.Domain.csproj TfgNetMvc.Domain/
COPY TfgNetMvc.Infrastructure/TfgNetMvc.Infrastructure.csproj TfgNetMvc.Infrastructure/

RUN dotnet restore TfgNetMvc.Web/TfgNetMvc.Web.csproj

COPY . .

RUN dotnet publish TfgNetMvc.Web/TfgNetMvc.Web.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "TfgNetMvc.Web.dll"]
