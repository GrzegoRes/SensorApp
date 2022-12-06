FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /App
COPY --from=build-env /App/out .

ADD docker-entry.sh /app/docker-entry.sh
RUN chmod +x /app/docker-entry.sh

ARG DB_CONNECTIONSTRING_D="mongodb://root:student@10.40.71.55:27017"
ARG DB_NAMEDATEBASE_D="sensorTestv2"

ARG RB_HOSTNAME_D="si_180046_rabbitmq"
ARG RB_PORT_D=5672
ARG RB_USERNAME_D="guest"
ARG RB_PASSSWORD_D="guest"
ARG RB_QUEUE_D="sensor.04"

ENV DB_CONNECTIONSTRING=$DB_CONNECTIONSTRING_D
ENV DB_NAMEDATEBASE=$DB_NAMEDATEBASE_D

ENV RB_HOSTNAME=$RB_HOSTNAME_D
ENV RB_PORT=$RB_PORT
ENV RB_USERNAME=$RB_USERNAME_D
ENV RB_PASSSWORD=$RB_PASSSWORD_D
ENV RB_QUEUE=$RB_QUEUE_D

ENTRYPOINT ["/app/docker-entry.sh" ]


