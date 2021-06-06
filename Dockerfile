FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY src/OneMenu.Service/bin/Release/net5.0 App/

ARG connection
ENV mongoConnection=$connection
WORKDIR /App

ENTRYPOINT ["dotnet", "OneMenu.Service.dll"]