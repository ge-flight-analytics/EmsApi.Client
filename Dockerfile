FROM microsoft/dotnet:2.1-sdk-alpine as build
WORKDIR /src
COPY ./src ./
RUN dotnet build -c Release
RUN dotnet publish -c Release

FROM microsoft/dotnet:2.1-runtime-alpine
WORKDIR /app
COPY --from=build /src/Dto/bin/Release/netstandard2.0 .
COPY --from=build /src/Client/bin/Release/netstandard2.0 .