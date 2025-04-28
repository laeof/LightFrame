FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app

FROM base AS final
WORKDIR /app
COPY ./API/bin/Release/net9.0/ .
ENTRYPOINT ["dotnet", "API.dll"]