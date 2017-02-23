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

function New-Package( [string] $nuspecFile )
{
    $packArgs = @( "pack", $nuspecFile, "-OutputDirectory .\bin" )    

    # Include appveyor build info if present.
    $version = $env:APPVEYOR_BUILD_VERSION
    if( $version ) { $packArgs += "-Version $version" }

    $branch = $env:APPVEYOR_REPO_BRANCH
    if( $branch -and $branch -ne 'master' ) 
    {
        $packArgs += "-Suffix $branch"
    }

    $packExp = "nuget.exe $packArgs"
    $packExp
    Invoke-Expression $packExp
}

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

function Build-Project( [string] $csProj )
{
    $config = "Debug"
    if( $Release.IsPresent )
    {
        $config = "Release"
    }

    # Run the build.
    $msBuild = Find-MsBuild
    Write-Host "Using msbuild at '$msBuild'" -ForegroundColor Yellow
    Write-Host "Building $csProj with $config" -ForegroundColor Yellow
    & $msBuild "/p:Configuration=$config" $csProj

    # Find the nuspec file in the same directory
    $csProjDir = Split-Path  ( Resolve-Path $csProj | Select-Object -Expand Path )
    $nuSpec = Get-ChildItem $csProjDir -Filter "*.nuspec" | Select-Object -First 1
    if( $nuSpec -eq $null )
    {
        throw "Could not find *.nuspec file in $csProjDir"
    }

    # Generate the package.
    New-Package $nuSpec.FullName
}

try
{
    Push-Location $PSScriptRoot
    Build-Project .\EmsApi.Dto\EmsApi.Dto.csproj
    Build-Project .\EmsApi.Client\EmsApi.Client.Vs2015.csproj
}
finally
{
    Pop-Location
}