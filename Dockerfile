# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY StarWarsWebApi/*.csproj ./StarWarsWebApi/


# copy everything else and build app
COPY StarWarsWebApi/. ./StarWarsWebApi/
WORKDIR /source/StarWarsWebApi
RUN dotnet nuget locals all --clear
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

RUN mkdir -p app/certificates
COPY Certificates/aspnetapp.pfx /app/certificates/

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "StarWarsWebApi.dll"]