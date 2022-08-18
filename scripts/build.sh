#!/bin/bash
set -e

current_directory="$PWD"

cd $(dirname $0)/../src

dotnet restore

dotnet build OpenKMS.sln --no-restore -c Release

result=$?

cd "$current_directory"

exit $result
