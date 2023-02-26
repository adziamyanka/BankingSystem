#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BankingSystemWebApi.csproj", "."]
RUN dotnet restore "./BankingSystemWebApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BankingSystemWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankingSystemWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingSystemWebApi.dll"]
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /source
#
## copy csproj and restore as distinct layers
#COPY *.sln .
#COPY aspnetapp/*.csproj ./aspnetapp/
#RUN dotnet restore
#
## copy everything else and build app
#COPY aspnetapp/. ./aspnetapp/
#WORKDIR /source/aspnetapp
#RUN dotnet publish -c release -o /app --no-restore
#
## final stage/image
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /app
#COPY --from=build /app ./
#ENTRYPOINT ["dotnet", "BankingSystemWebApi.dll"]