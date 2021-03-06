FROM microsoft/dotnet:1.1.1-runtime
COPY . /app
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "FlexDockerAppSample.dll"]