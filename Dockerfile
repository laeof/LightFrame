FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM base AS final
WORKDIR /app
COPY ./API/bin/Release/net8.0/ .
ENTRYPOINT ["dotnet", "API.dll"]