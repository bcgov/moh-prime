FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-30-rhel7 AS build
USER 0
SHELL [ "/bin/bash" , "-c" ]
WORKDIR /opt/app-root/app
ENV WORKDIR /opt/app-root/app
ENV ASPNETCORE_ENVIRONMENT Development
ENV PATH $PATH:/opt/rh/rh-dotnet30/root/usr/lib64/dotnet
#RUN find / -type f -name dotnet
# Copy csproj and restore as distinct layers
#RUN echo `pwd`
#RUN mkdir -p /opt/app-root/app
COPY *.csproj /opt/app-root/app
RUN dotnet restore
# Copy everything else and build
COPY . /opt/app-root/app
RUN dotnet publish -c Release -o out
# Build runtime image
FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-30-runtime-rhel7 AS runtime
COPY --from=build /opt/app-root/app .
EXPOSE 8080 5001
USER 1001
ENV PATH $PATH:/opt/rh/rh-dotnet30/root/usr/lib64/dotnet
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["/opt/rh/rh-dotnet30/root/usr/lib64/dotnet/dotnet", "prime.dll"]