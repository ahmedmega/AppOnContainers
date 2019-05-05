FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY WebCoreMongo/WebCoreMongo.csproj WebCoreMongo/
RUN dotnet restore WebCoreMongo/WebCoreMongo.csproj
COPY . .
WORKDIR /src/WebCoreMongo
RUN dotnet build WebCoreMongo.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebCoreMongo.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebCoreMongo.dll"]
