#
# .SYNOPSIS
# Updates the EMS API swagger spec (stored in ems-api.json) that is used to generate DTO classes.
#
param
(
    [Parameter( Mandatory = $true )]
    [string] $Endpoint,

    [Parameter( Mandatory = $true )]
    [string] $UserName,

    [Parameter( Mandatory = $true )]
    [SecureString] $Password
)

$ErrorActionPreference = "Stop"
Push-Location $PSScriptRoot
try
{
    & .\Get-EmsApiSwaggerSpec.ps1 @PSBoundParameters -OutFile "ems-api.json" | Out-Null
}
finally
{
    Pop-Location
}