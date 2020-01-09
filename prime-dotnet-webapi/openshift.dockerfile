ENV OC_APP "${OC_APP}"
FROM docker-registry.default.svc:5000/dqszvc-$OC_APP/dotnet-22-rhel7 AS build
WORKDIR /opt/app-root/app
SHELL [ "/bin/bash" , "-c" ]
ENV PATH "$PATH:/opt/rh/rh-dotnet22/root/usr/lib64/dotnet"
ENV ASPNETCORE_ENVIRONMENT "${ASPNETCORE_ENVIRONMENT}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
ENV SUFFIX "${SUFFIX}"
ENV DB_HOST "$DB_HOST"
ARG DB_CONNECTION_STRING="host=postgresql${SUFFIX};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_PASSWORD}"
RUN printenv | sort 
COPY *.csproj /opt/app-root/app
RUN dotnet restore
COPY . /opt/app-root/app/
RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
FROM docker-registry.default.svc:5000/dqszvc-$OC_APP/dotnet-22-runtime-rhel7 AS runtime
WORKDIR /opt/app-root/app
ENV PATH "$PATH:/opt/rh/rh-dotnet22/root/usr/lib64/dotnet"
COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001 1025
ENV DB_HOST ${DB_HOST}
ENV DB_CONNECTION_STRING="host=postgresql${SUFFIX};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_ADMIN_PASSWORD}"
ENTRYPOINT echo "Running .NET..."; export DB_CONNECTION_STRING="host=${DB_HOST};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_ADMIN_PASSWORD}"; /opt/rh/rh-dotnet22/root/usr/lib64/dotnet/dotnet prime.dll
