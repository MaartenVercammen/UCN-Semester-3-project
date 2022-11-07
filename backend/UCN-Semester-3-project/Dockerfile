#build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal as build
WORKDIR /source
COPY . .
RUN dotnet restore "./RecipeRestService/RecipeRestService.csproj" --disable-parallel
RUN dotnet publish  "./RecipeRestService/RecipeRestService.csproj" -c release -o /app --no-restore

#serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "RecipeRestService.dll"]