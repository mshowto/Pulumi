FROM ubuntu:latest
RUN apt-get update -y && apt-get upgrade -y

#Dependencies for .NET Core installation
RUN apt-get install -y \
    libunwind8 \
    libicu60 \
    ca-certificates \
    software-properties-common

#.NET Core SDK installation
ADD https://packages.microsoft.com/config/ubuntu/19.10/packages-microsoft-prod.deb /tmp
RUN cd /tmp && dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb 
RUN add-apt-repository universe
RUN apt-get install -y apt-transport-https
RUN apt-get update
RUN apt-get install -y dotnet-sdk-3.1

#Create a new ASP.NET Core app
RUN mkdir mshowto && mkdir web
RUN cd mshowto/ && dotnet new mvc && dotnet publish -c Release -o ReleaseOutput
RUN cp -R /mshowto/ReleaseOutput/. web/

#Remove source files
RUN rm -r /mshowto

WORKDIR /web
EXPOSE 5001 
ENTRYPOINT ["dotnet", "mshowto.dll","--urls", "https://0.0.0.0:5001"]