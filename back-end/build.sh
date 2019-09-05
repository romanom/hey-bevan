#!/bin/bash

#install zip on debian OS, since microsoft/dotnet container doesn't have zip by default
if [ -f /etc/debian_version ]
then
  apt -qq update
  apt -qq -y install zip
fi

dotnet restore
dotnet lambda package --configuration release --framework netcoreapp2.1 --output-package bin/release/netcoreapp2.1/addBevan.zip
dotnet lambda package --configuration release --framework netcoreapp2.1 --output-package bin/release/netcoreapp2.1/challenge.zip
dotnet lambda package --configuration release --framework netcoreapp2.1 --output-package bin/release/netcoreapp2.1/GetAll.zip
dotnet lambda package --configuration release --framework netcoreapp2.1 --output-package bin/release/netcoreapp2.1/GetById.zip