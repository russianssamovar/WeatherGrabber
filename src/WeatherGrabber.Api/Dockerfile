#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#------------------------------------#
# Use SDK image to build the service #
#------------------------------------#

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /src

# Copy only files that's required to restore packages. This approach allows not to restore packages on every build while project files stays the same.
# All new .csproj and .props files MUST be added here.
COPY ["src/WeatherGrabber.Api", "src/WeatherGrabber.Api/"]
COPY ["src/WeatherGrabber.Domain/WeatherGrabber.Domain.csproj", "src/WeatherGrabber.Domain/"]
COPY ["src/WeatherGrabber.Infrastructure/WeatherGrabber.Infrastructure.csproj", "src/WeatherGrabber.Infrastructure/"]
COPY ["src/WeatherGrabber.Services/WeatherGrabber.Services.csproj", "src/WeatherGrabber.Services/"]
COPY ["src/WeatherGrabber.Models/WeatherGrabber.Models.csproj", "src/WeatherGrabber.Models/"]

RUN dotnet restore "src/WeatherGrabber.Api/WeatherGrabber.Api.csproj"

# Copy the rest of source files to build the service.
COPY . .

# Publish application
WORKDIR "/src/src/WeatherGrabber.Api"
ARG informational_version
RUN dotnet publish "WeatherGrabber.Api.csproj" -c Release -o /app/publish --no-restore /p:InformationalVersion="${informational_version}"

#------------------------#
# Create the final image #
#------------------------#

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final

# It's important to perform a graceful shutdown, as if Ctrl+C was pressed. The default signal (SIGTERM) stops the process abnormally.
STOPSIGNAL SIGINT

ARG app_version
ARG informational_version

ENV DOTNET_USE_POLLING_FILE_WATCHER=true
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WeatherGrabber.Api.dll"]
