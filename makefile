MAKEFLAGS += --silent

verify:
	./scripts/verify.sh

build:
	./scripts/build.sh

release:
	./scripts/release.sh
