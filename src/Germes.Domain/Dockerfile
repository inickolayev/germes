FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Germes.Domain/Germes.Domain.csproj", "Germes.Domain/"]
RUN dotnet restore "src/Germes.Domain/Germes.Domain.csproj"
COPY . .
WORKDIR "/src/Germes.Domain"
RUN dotnet build "Germes.Domain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Germes.Domain.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Germes.Domain.dll"]
