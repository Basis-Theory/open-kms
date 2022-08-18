#!/bin/bash
set -e

current_directory="$PWD"

cd $(dirname $0)/..

yarn install --frozen-lockfile
yarn release

result=$?

cd "$current_directory"

exit $result
