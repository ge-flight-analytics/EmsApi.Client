#
# .SYNOPSIS
# Updates the EMS API swagger spec (stored in ems-api.json) that is used to generate DTO classes.
# This version reads the test environment variables to get an API token for retrieving the spec.
#

try
{
    Push-Location $PSScriptRoot
    $endpoint = $env:EmsApiTestEndpoint
    if( [string]::IsNullOrEmpty( $endpoint ) )
    {
        Write-Warning "Missing EmsApiTestEndpoint environment variable, cannot download swagger spec."
        return
    }

    $user = $env:EmsApiTestUsername
    $pass = $env:EmsApiTestPassword
    if( [string]::IsNullOrEmpty( $user ) -or [string]::IsNullOrEmpty( $pass ) )
    {
        Write-Warning "Missing EmsApiTestUsername or EmsApiTestPassword environment variables, cannot download swagger spec."
        return
    }

    # Convert our plain text password to a secure string, that's what the script takes.
    $passConverted = $pass | ConvertTo-SecureString -AsPlainText -Force
    Write-Host "Writing updated swagger spec to $PSScriptRoot\ems-api.json" -ForegroundColor Yellow
    & .\Get-EmsApiSwaggerSpec.ps1 -Endpoint $endpoint -UserName $user -Password $passConverted -OutFile "ems-api.json" | Out-Null
    Write-Host "Swagger update succeeded" -ForegroundColor Yellow
}
finally
{
    Pop-Location
}