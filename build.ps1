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
        if( $env:APPVEYOR_BUILD_VERSION ) { 
            $baseVersion = $env:APPVEYOR_BUILD_VERSION 
        }
        else {
            $baseVersion = (Get-Content "$PSScriptRoot/appveyor.yml" | Select-Object -First 1).Replace( 'version: ', '' )
        }

        $b = $env:APPVEYOR_BUILD_NUMBER
        $stamp = (Get-Date).ToString( 'yyyyMMddTHHmmssfff' )
        $Version = "$baseVersion-rc$stamp$b"
    }
}

if( -not $Configuration ) {
    if( $env:CONFIGURATION ) { $Configuration = $env:CONFIGURATION }
    else { $Configuration = 'Debug' }
}

dotnet build /p:Configuration=$Configuration /p:Version=$Version