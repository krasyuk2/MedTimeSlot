FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5080


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MedTimeSlot.Api/MedTimeSlot.Api.csproj", "MedTimeSlot.Api/"]
COPY ["MedTimeSlot.Application/MedTimeSlot.Application.csproj", "MedTimeSlot.Application/"]
COPY ["MedTimeSlot.Core/MedTimeSlot.Core.csproj", "MedTimeSlot.Core/"]
COPY ["MedTimeSlot.DataAccess/MedTimeSlot.DataAccess.csproj", "MedTimeSlot.DataAccess/"]
RUN dotnet restore "MedTimeSlot.Api/MedTimeSlot.Api.csproj"
COPY . .
WORKDIR /src/MedTimeSlot.Api
RUN dotnet build "MedTimeSlot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MedTimeSlot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedTimeSlot.Api.dll"]
