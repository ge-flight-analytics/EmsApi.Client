# Summary

This project generates DTO objects for the EMS API. It uses the NSwag library, along with the EMS API swagger specification, to produce some code files (csharp and typescript) and a .NET assembly.

## Steps

When this project is built, the following steps occur:
* An MSBuild step written in the EmsApi.Dto.csproj file runs `nswag swagger2csclient /input:ems-api.json ...`
* NSwag reads the swagger specification out of ems-api.json and creates the `Generated\EmsApi.Dto.V2.cs`
* MSBuild builds `EmsApi.Dto.dll` using the included files under `Generated`

In order to update the cached EMS API swagger specification manually:
* From this directory, run:
  ```powershell
  $secureString = Read-Host "EMS API password" -AsSecureString
  .\Update-EmsApiSwaggerSpec.ps1 -Endpoint "https://your-ems-host/api" -UserName "your-user" -Password $secureString
  ```
  Bare hostnames are also accepted and default to HTTPS. Use HTTPS directly to
  avoid HTTP redirects that can discard the token request's POST body.