#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
#添加一些常用到的测试性linux命令，根据你的需要添加，你也可以部署后去容器添加
#RUN apt-get update
#RUN apt-get -y install telnet
#RUN apt-get -y install iputils-ping
#RUN apt-get -y install vim
#RUN apt-get install net-tools
COPY . .

ENTRYPOINT ["dotnet", "Tourism.Api.dll"]