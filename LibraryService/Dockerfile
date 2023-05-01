# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#
#COPY ["ApiGateway/ApiGateway.csproj", "ApiGateway/"]
#RUN dotnet restore "ApiGateway/ApiGateway.csproj"
#
#COPY ["LibraryService/LibraryService.csproj", "LibraryService/"]
#RUN dotnet restore "LibraryService/LibraryService.csproj"
#
#COPY . .
#WORKDIR "/src/ApiGateway"
#RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build
#
#WORKDIR "/src/LibraryService"
#RUN dotnet build "LibraryService.csproj" -c Release -o /app/build2
#
#FROM build AS publish
#WORKDIR "/src/ApiGateway"
#RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#WORKDIR "/src/LibraryService"
#RUN dotnet publish "LibraryService.csproj" -c Release -o /app/publish2 /p:UseAppHost=false
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
#WORKDIR /app
#
#COPY --from=publish /app/publish .
#COPY --from=publish /app/publish2 .
#
#EXPOSE 44370
#EXPOSE 44363
#
#ENTRYPOINT ["dotnet", "ApiGateway.dll", "--urls", "https://*:44370;http://*:80"]






#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LibraryService.csproj", "./"]
COPY ["ApiGateway/ApiGateway.csproj", "ApiGateway/"]
RUN dotnet restore "./LibraryService.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "LibraryService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryService.dll"]



FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

RUN dotnet restore "ApiGateway/ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]