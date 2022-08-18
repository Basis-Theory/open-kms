# Open KMS SDK

[![NuGet](https://img.shields.io/nuget/v/OpenKMS.svg)](https://www.nuget.org/packages/OpenKMS/)
[![Verify](https://github.com/Basis-Theory/open-kms/actions/workflows/verify.yml/badge.svg)](https://github.com/Basis-Theory/open-kms/actions/workflows/verify.yml)

The OpenKMS .NET SDK for .NET 6+.

## Installation

Using the [.NET Core command-line interface (CLI) tools](https://docs.microsoft.com/en-us/dotnet/core/tools/):

```sh
dotnet add package OpenKMS
```

Using the [NuGet Command Line Interface (CLI)](https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference):

```sh
nuget install OpenKMS
```

Using the [Package Manager Console](https://docs.microsoft.com/en-us/nuget/tools/package-manager-console):

```powershell
Install-Package OpenKMS
```

## Documentation

Coming soon!

## Usage

Coming soon!

## Development

The provided scripts with the SDK will check for all dependencies, start docker, build the solution, and run all tests.

### Dependencies
- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://www.docker.com/products/docker-desktop)
- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

### Build the SDK and run Tests

Run the following command from the root of the project:

```sh
make verify
```
