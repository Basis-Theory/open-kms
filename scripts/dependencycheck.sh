#!/bin/bash
set -e

# ensure that your machine is set up to build software
my_directory=$(dirname $0)

for filename in $my_directory/checks/*; do
  chmod +x $filename
  $filename
done