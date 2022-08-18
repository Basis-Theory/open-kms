#!/bin/bash

echo "Checking Docker..."

EXPECTED_DOCKER="(18|19|20)\.[0-9]+\.[0-9]*"
DISPLAY_DOCKER_REGEX=$(sed -e 's|\\\([.+?*()]\)|\1|g' -e 's|[.+?]\*|*|g' <<<${EXPECTED_DOCKER})
version=$(docker version --format '{{.Client.Version}}' 2>&1)

if [[ $version == *"docker.sock"* ]]; then
    echo "Your user does not have permission to run docker commands, and we don't want you to run this with sudo permissions. Go here and read: http://askubuntu.com/questions/477551/how-can-i-use-docker-without-sudo"
    echo "Probably your user should be a member of the 'docker' UNIX group. Run \`sudo adduser $(whoami) docker\` and then fully log out and log back in."
    exit 1
fi

if [[ "$version" =~ $EXPECTED_DOCKER ]]; then
    echo "Docker is OK"
else
    echo "Please Install Docker $DISPLAY_DOCKER_REGEX via Docker Desktop; https://www.docker.com/products/docker-desktop"
    exit 1
fi