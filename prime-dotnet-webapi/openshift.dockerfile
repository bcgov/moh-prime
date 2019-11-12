FROM docker-registry.default.svc:5000/dqszvc-${OC_APP}/dotnet-22-rhel7 AS build
WORKDIR /opt/app-root/app
SHELL [ "/bin/bash" , "-c" ]
ENV PATH "$PATH:/opt/rh/rh-dotnet22/root/usr/lib64/dotnet"
ENV ASPNETCORE_ENVIRONMENT Development
COPY *.csproj /opt/app-root/app
RUN find /opt -type f -name dotnet
RUN dotnet restore 
COPY . /opt/app-root/app/
RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
FROM docker-registry.default.svc:5000/dqszvc-${OC_APP}/dotnet-22-runtime-rhel7 AS runtime
WORKDIR /opt/app-root/app
ENV PATH "$PATH:/opt/rh/rh-dotnet22/root/usr/lib64/dotnet"
COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001
RUN find /opt -type f -name dotnet
ENTRYPOINT ["/opt/rh/rh-dotnet22/root/usr/lib64/dotnet/dotnet", "prime.dll"]
