FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /source

COPY ["../TaskManager.Presentation/*.csproj", "./TaskManager.Presentation/"]
COPY ["../TaskManager.Application/*.csproj", "./TaskManager.Application/"]
COPY ["../TaskManager.Domain/*.csproj", "./TaskManager.Domain/"]
COPY ["../TaskManager.Infrastructure/*.csproj", "./TaskManager.Infrastructure/"]

RUN dotnet restore TaskManager.Presentation/TaskManager.Presentation.csproj

COPY ../ .
RUN dotnet publish TaskManager.Presentation/TaskManager.Presentation.csproj -c $BUILD_CONFIGURATION -o /release

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
WORKDIR /app
COPY --from=build /release .
ENTRYPOINT ["dotnet", "TaskManager.Presentation.dll"]