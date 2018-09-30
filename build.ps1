param(
    [Parameter()]
    [string] $Version,

    [Parameter()]
    [string] $Configuration,

    [Parameter()]
    [switch] $ForceDotnetCore
)

if( -not $Version ) {
    if( $env:APPVEYOR_REPO_TAG -eq 'true' ) {
        $tag = $env:APPVEYOR_REPO_TAG_NAME
        $tag = $tag.TrimStart( 'v' )
        $Version = $tag
    }
    else {
        # Even when running under appveyor we grab the version from the yml. We don't care about the build number appveyor assigns,
        # we just want to store the canonical version there, but each build in appveyor must have a unique version number.
        $baseVersion = (Get-Content "$PSScriptRoot/appveyor.yml" | Select-Object -First 1).Replace( 'version: ', '' ).Replace( '.{build}', '' )
        $stamp = (Get-Date).ToUniversalTime().ToString( 'yyyyMMddTHHmmssfff' )
        $Version = "$baseVersion-rc$stamp$($env:APPVEYOR_BUILD_NUMBER)"
    }
}

if( -not $Configuration ) {
    if( $env:CONFIGURATION ) { $Configuration = $env:CONFIGURATION }
    else { $Configuration = 'Debug' }
}

if( $IsWindows -eq $false -or $ForceDotnetCore.IsPresent ) {
    dotnet build /p:Configuration=$Configuration /p:Version=$Version
}
else {
    # We have to do some special stuff until SpecFlow supports .net core. We build the specflow
    # test project under .net framework which will also rebuild the libraries in .net core.
    $msbuild = & $PSScriptRoot\Find-MsBuild.ps1
    & $msbuild $PSScriptRoot\Tests\EmsApi.Tests.csproj /target:Restore,Build /p:Configuration=$Configuration
    & $msbuild $PSScriptRoot\Tests\SpecFlow\EmsApi.Tests.SpecFlow.csproj /target:Restore,Build /p:Configuration=$Configuration
}