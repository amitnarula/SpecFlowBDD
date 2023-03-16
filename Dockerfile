#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CalculatorSelenium.Specs/CalculatorSelenium.Specs.csproj", "CalculatorSelenium.Specs/"]
RUN dotnet restore "CalculatorSelenium.Specs/CalculatorSelenium.Specs.csproj"
COPY . .
WORKDIR "/src/CalculatorSelenium.Specs"
RUN dotnet build "CalculatorSelenium.Specs.csproj" -c Release -o /app/build

# Install Chrome
RUN apt-get update && \
    wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb && \
    apt-get install -y ./google-chrome-stable_current_amd64.deb

RUN dotnet test --logger:trx --no-restore