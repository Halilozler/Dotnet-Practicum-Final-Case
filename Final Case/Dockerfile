#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Final Case/Final Case.csproj", "Final Case/"]
COPY ["Final.Service/Final.Service.csproj", "Final.Service/"]
COPY ["Final.Data/Final.Data.csproj", "Final.Data/"]
COPY ["Final.Base/Final.Base.csproj", "Final.Base/"]
COPY ["Final.Dto/Final.Dto.csproj", "Final.Dto/"]
RUN dotnet restore "Final Case/Final Case.csproj"
COPY . .
WORKDIR "/src/Final Case"
RUN dotnet build "Final Case.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Final Case.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Final Case.dll"]
