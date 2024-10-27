FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

RUN apt-get update && \
  apt-get install -y wget curl apt-transport-https

# .NET Core 2.2 SDK インストール
RUN wget https://dotnetcli.azureedge.net/dotnet/Sdk/2.2.207/dotnet-sdk-2.2.207-linux-x64.tar.gz -O dotnet-sdk-2.2.207.tar.gz && \
  mkdir -p /usr/share/dotnet/sdk && \
  tar -zxf dotnet-sdk-2.2.207.tar.gz -C /usr/share/dotnet && \
  rm dotnet-sdk-2.2.207.tar.gz

# .NET Core 3.1 SDK インストール
RUN wget https://dotnetcli.azureedge.net/dotnet/Sdk/3.1.426/dotnet-sdk-3.1.426-linux-x64.tar.gz -O dotnet-sdk-3.1.426.tar.gz && \
  mkdir -p /usr/share/dotnet/sdk && \
  tar -zxf dotnet-sdk-3.1.426.tar.gz -C /usr/share/dotnet && \
  rm dotnet-sdk-3.1.426.tar.gz

ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="${PATH}:/usr/share/dotnet"

# sqlcmdのインストール
RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
  curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
  apt-get update && \
  ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools && \
  apt-get clean && \
  rm -rf /var/lib/apt/lists/*

ENV PATH="$PATH:/opt/mssql-tools/bin"
