FROM microsoft/dotnet:2.0-sdk AS builder

WORKDIR /app

COPY POI.Contracts /POI.Contracts

COPY POI.Service /app
RUN dotnet restore POI.Service.csproj
RUN dotnet publish --configuration Release --output ./out


FROM microsoft/aspnetcore:2.0
LABEL maintainer "frank@pommerening-online.de"
ENV REFRESHED_AT 2018-03-28

ENV ASPNETCORE_URLS http://0.0.0.0:8080
EXPOSE 8080

WORKDIR /app/
COPY --from=builder /app/out/* ./

ENTRYPOINT ["dotnet", "POI.Service.dll"]