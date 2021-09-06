FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["EmployeeWebAPI.csproj", "."]
RUN dotnet restore "./EmployeeWebAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish "EmployeeWebAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EmployeeWebAPI.dll"]