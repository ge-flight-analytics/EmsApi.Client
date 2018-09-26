param(
    [Parameter()]
    [string] $Configuration
)

if( -not $Configuration ) {
    if( $env:CONFIGURATION ) { $Configuration = $env:CONFIGURATION }
    else { $Configuration = 'Debug' }
}

try {
    Push-Location $PSScriptRoot\Tests
    dotnet test --no-build -c $Configuration
}
finally {
    Pop-Location
}