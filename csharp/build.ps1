#
# .SYNOPSIS
# Builds all projects under the csharp directory.
#

[CmdletBinding()]
param
(
    [Parameter()]
    [switch] $Release
)

function Find-MsBuild
{
    $msBuild = Get-Command "msbuild.exe" -ErrorAction SilentlyContinue
    if( $msBuild -eq $null )
    {
        # Get the path from the registry, if possible. Enumerate the tools versions and take the highest one.
        $versions = Get-ChildItem "HKLM:\SOFTWARE\Microsoft\MSBuild\ToolsVersions"
        $highestVersion = $versions | Sort-Object @{ expression = { [int](Split-Path $_.Name -Leaf) }; Descending = $false } | Select-Object -Last 1
        $buildToolsDir = $highestVersion.GetValue( "MsBuildToolsPath" )

        $msBuildPath = Join-Path $buildToolsDir "msbuild.exe"
        if( -not ( Test-Path $msBuildPath ) )
        {
            throw "Could not locate msbuild.exe on the PATH or using the MSBuildToolsPath in HKLM:\SOFTWARE\Microsoft\MSBuild\ToolsVersions"
        }

        $msBuild = Get-Command $msBuildPath
    }

    return $msBuild
}

$config = "Debug"
if( $Release.IsPresent )
{
    $config = "Release"
}

$msBuild = Find-MsBuild
Write-Host "Using msbuild at '$msBuild'" -ForegroundColor Yellow

# Build the library and tests.
$building = "$PSScriptRoot\EmsApi.sln"
Write-Host "Building $building with $config" -ForegroundColor Yellow
& $msBuild "/p:Configuration=$config" "/verbosity:minimal" $building

# Build the examples.
$building = "$PSScriptRoot\Examples\EmsApi.Example.sln"
Write-Host "Building $building with $config" -ForegroundColor Yellow
& $msBuild "/p:Configuration=$config" "/verbosity:minimal" $building