# pull official base image
FROM node:12

# set working directory
WORKDIR /src

# Copy the rest of source files to build the service.
COPY ["src/WeatherGrabber.Scripts", "src/WeatherGrabber.Scripts/"]

WORKDIR "/src/src/WeatherGrabber.Scripts"
RUN npm install

# start app
CMD ["npm", "start"] 