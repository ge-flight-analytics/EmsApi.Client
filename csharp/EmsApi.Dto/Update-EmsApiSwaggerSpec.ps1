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

Push-Location $PSScriptRoot
& .\Get-EmsApiSwaggerSpec.ps1 @PSBoundParameters -OutFile "ems-api.json" | Out-Null
Pop-Location