FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY RockSchool.Domain/RockSchool.Domain.csproj RockSchool.Domain/
COPY RockSchool.Data/RockSchool.Data.csproj RockSchool.Data/
COPY RockSchool.BL/RockSchool.BL.csproj RockSchool.BL/
COPY RockSchool.WebApi/RockSchool.WebApi.csproj RockSchool.WebApi/

RUN dotnet restore RockSchool.WebApi/RockSchool.WebApi.csproj

COPY . .

RUN dotnet publish RockSchool.WebApi/RockSchool.WebApi.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "RockSchool.WebApi.dll"]
