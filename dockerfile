FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["MemberManagement.Web/MemberManagement.Web.csproj", "MemberManagement.Web/"]
COPY ["ManagementSystem.Data/ManagementSystem.Data.csproj", "ManagementSystem.Data/"]
RUN dotnet restore "MemberManagement.Web/MemberManagement.Web.csproj"
COPY . .
WORKDIR "/src/MemberManagement.Web"
RUN dotnet build "MemberManagement.Web.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "MemberManagement.Web.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet MemberManagement.Web.dll