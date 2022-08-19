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

OpenKMS is an encryption abstraction based on the [Json Web Encryption (JWE)](https://datatracker.ietf.org/doc/html/rfc7516), 
[Json Web Algorithm (JWA)](https://datatracker.ietf.org/doc/html/rfc7518), and [Json Web Key (JWK)](https://datatracker.ietf.org/doc/html/rfc7517) specifications.
The [IEncryptionService](./src/OpenKMS/Abstractions/IEncryptionService.cs) interface exposes methods for encrypt and decrypt operations.
```csharp
public interface IEncryptionService
{
    Task<JsonWebEncryption> EncryptAsync(byte[] plaintext, string? scheme, CancellationToken cancellationToken = default);
    Task<JsonWebEncryption> EncryptAsync(string plaintext, string? scheme, CancellationToken cancellationToken = default);

    Task<byte[]> DecryptAsync(JsonWebEncryption encryption, CancellationToken cancellationToken = default);
    Task<string> DecryptStringAsync(JsonWebEncryption encryption, CancellationToken cancellationToken = default);
}
```

[Encryption schemes](./src/OpenKMS/EncryptionScheme.cs) are used to register encryption handlers and pre-configure options (e.g. KeyType, KeySize, Algorithm) used when calling `EncryptAsync`.
```csharp
// IServiceCollection services;

services.AddEncryption(o =>
{
    o.DefaultScheme = "default";
}).AddScheme<AesEncryptionOptions, AesEncryptionHandler, AzureKeyVaultEncryptionOptions, AzureKeyVaultEncryptionHandler>("default", 
    contentEncryptionOptions => {
        contentEncryptionOptions.EncryptionAlgorithm = EncryptionAlgorithm.A256CBC_HS512;
        contentEncryptionOptions.KeySize = 256;
        contentEncryptionOptions.KeyType = KeyType.OCT;
    },
    keyEncrptionOptions => {
        keyEncrptionOptions.KeySize = 4096;
        keyEncrptionOptions.KeyType = KeyType.RSA;
        keyEncrptionOptions.KeyName = "<key_name>";
        keyEncrptionOptions.EncryptionAlgorithm = EncryptionAlgorithm.RSA_OAEP;
    }
);
```

To derive a new encryption handler implementation, extend the [EncryptionHandler<TOptions>](./src/OpenKMS/EncryptionHandler.cs)
and [EncryptionHandlerOptions](./src/OpenKMS/EncryptionHandlerOptions.cs) abstract classes. 

Provide handler implementations for:
- `Task<EncryptResult> EncryptAsync(byte[], byte[]?, CancellationToken)`
- `Task<byte[]> DecryptAsync(JsonWebKey, byte[], byte[]?, byte[]?, byte[]?, CancellationToken)`
- `bool CanDecrypt(JsonWebKey)`

Provide options implementations for:
- `IList<EncryptionAlgorithm> ValidEncryptionAlgorithms`
- `Dictionary<KeyType, int?[]> ValidKeyTypeSizes`
- `EncryptionAlgorithm EncryptionAlgorithm`
- `KeyType KeyType`
- `int? KeySize`

## Usage

### Dependency Injection

To configure an instance of `IEncryptionService` that can be used throughout your project, call `AddEncryption` when registering services:
```csharp
// program.cs

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEncryption()
    .AddScheme<TContentEncryptionOptions, TContentHander, TKeyEncryptionOptions, TKeyHandler>("<scheme_name>",
        contentEncrptionOptions => { ... },
        keyEncryptionOptions => { ... }
    ); // calls can be chained to .AddScheme<> to register more encryption schemes!
```

To inject an instance of `IEncryptionHandler` into your class for data encryption and/or decryption, 
add a constructor dependency for `IEncryptionService`.
```csharp
public class MyAwesomeService() {
    private readonly IEncryptionService _encryptionService;
    private readonly IPersonRepository _repository;
    
    public MyAwesomeService(IEncryptionService encryptionService, IPersonRepository repository) {
        _encryptionService = encryptionService;
        _repository = repository;
    }
    
    public async Task SavePerson(Person person, CancellationToken cancellationToken = default) {
        JsonWebEncryption encryptedName = await _encryptionService.EncryptAsync(
            person.Name,
            "<scheme_name>",
            cancellationToken);
        
        person.Name = null;
        person.NameEncrypted = encryptedName.ToCompactSerializationFormat();
        
        await _repository.SavePersonAsync(person, cancellationToken);
    }
    
    public async Task<Person> GetPersonById(string personId, CancellationToken cancellationToken = default) {
        var foundPerson = await _repository.GetPersonAsync(personId, cancellationToken);
        
        JsonWebEncryption encryptedName = JsonWebEncryption.FromCompactSerializationFormat(person.NameEncrypted);
        var decryptedName = await _encryptionService.DecryptAsync(encryptedName, cancellationToken);
        
        person.Name = Encoding.UTF8.GetString(decryptedName);
        person.NameEncrypted = null;
    
        return person;
    }
}
```

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
