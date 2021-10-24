FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY ["src/Germes/Germes.csproj", "Germes/ClientBaro>
COPY ["src/", "/"]
RUN dotnet build "Germes/Germes.csproj" -c Release -o /app/bu>

FROM build AS publish
RUN dotnet publish "Germes/Germes.csproj" -c Release -o /app/>

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Germes.dll"]
