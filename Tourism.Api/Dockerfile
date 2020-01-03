#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Tourism.Api/Tourism.Api.csproj", "Tourism.Api/"]
COPY ["Tourism.Model/Tourism.Model.csproj", "Tourism.Model/"]
COPY ["Tourism.IServer/Tourism.IServer.csproj", "Tourism.IServer/"]
COPY ["Tourism.QueryModel/Tourism.QueryModel.csproj", "Tourism.QueryModel/"]
COPY ["Tourism.Util/Tourism.Util.csproj", "Tourism.Util/"]
COPY ["Tourism.Eums/Tourism.Eums.csproj", "Tourism.Eums/"]
COPY ["Tourism.Server/Tourism.Server.csproj", "Tourism.Server/"]
COPY ["Tourism.Repository/Tourism.Repository.csproj", "Tourism.Repository/"]
COPY ["Tourism.DbContext/Tourism.DBContext.csproj", "Tourism.DbContext/"]
RUN dotnet restore "Tourism.Api/Tourism.Api.csproj"
COPY . .
WORKDIR "/src/Tourism.Api"
RUN dotnet build "Tourism.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tourism.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tourism.Api.dll"]