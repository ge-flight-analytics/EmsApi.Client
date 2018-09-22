# Summary

This project generates DTO objects for the EMS API. It uses the NSwag library, along with the EMS API swagger specification, to produce some code files (csharp and typescript) and a .NET assembly.

The appveyor builds automatically download a new version of the swagger spec from the API (specified in the `EmsApiTestEndpoint`, `EmsApiTestUser`, and `EmsApiTestPassword` environment variables) before building.

## Steps

When this project is built, the following steps occur:
* An MSBuild step written in the EmsApi.Dto.csproj file runs `nswag swagger2csclient /input:ems-api.json ...`
* NSwag reads the swagger specification out of ems-api.json and creates the `Generated\EmsApi.Dto.V2.cs`
* MSBuild builds `EmsApi.Dto.dll` using the included files under `Generated`

In order to update the cached EMS API swagger specification manually:
* Run `.\Update-EmsApiSwaggerSpec.ps1`