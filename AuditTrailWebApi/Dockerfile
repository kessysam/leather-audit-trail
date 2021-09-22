#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AuditTrailWebApi/AuditTrailWebApi.csproj", "AuditTrailWebApi/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["ApplicationServices/ApplicationServices.csproj", "ApplicationServices/"]
COPY ["MessagingBus/MessagingBus.csproj", "MessagingBus/"]
RUN dotnet restore "AuditTrailWebApi/AuditTrailWebApi.csproj"
COPY . .
WORKDIR "/src/AuditTrailWebApi"
RUN dotnet build "AuditTrailWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuditTrailWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuditTrailWebApi.dll"]