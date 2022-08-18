#!/bin/bash

echo "Checking .NET..."
EXPECTED_DOTNET="6.0.x"
EXPECTED_DOTNET_REGEX="6\.0\.([0-9]*)"

while IFS= read -r line
do
    version="$(cut -d ' ' -f 1 <<< "$line")"
    if [[ $version =~ $EXPECTED_DOTNET_REGEX ]]; then
        echo ".NET is OK"
        exit 0
    fi
done <<<"$(dotnet --list-sdks)"

echo "Please Install .NET $EXPECTED_DOTNET SDK from here: https://dotnet.microsoft.com/download/dotnet/6.0"
exit 1
