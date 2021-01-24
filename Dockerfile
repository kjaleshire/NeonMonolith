# For building the project dependencies
FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-alpine3.9 as build

# Build directory
WORKDIR /build

RUN mkdir /app

# Copy in the project file to be built
COPY NeonMonolith.csproj .

# Restore depencencies using the existing project file for caching
RUN dotnet restore

# Copy in the project source files
COPY . .

# Build for caching
RUN dotnet build \
    --configuration Release \
    --runtime linux-musl-x64

# Finally build and publish the self-contained app
RUN dotnet publish \
    --configuration Release \
    --runtime linux-musl-x64 \
    --output /app \
    /p:ShowLinkerSizeComparison=true
    # /p:PublishSingleFile=true

# No .NET runtime required; the app is self-contained. Only need a minimal runtime
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0.0-alpine3.9

RUN apk update && \
    apk add --update --no-cache \
    tzdata

# Copy in the published application
COPY --from=build /app .

CMD [ "./NeonMonolith" ]
