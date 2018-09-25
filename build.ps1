param(
    [Parameter()]
    [string] $Version,

    [Parameter()]
    [string] $Configuration
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

dotnet build /p:Configuration=$Configuration /p:Version=$Version