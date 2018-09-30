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

    # Do some custom windows stuff until SpecFlow supports .net core
    if( $IsWindows -eq $false ) {
        return
    }

    $xunit = & $PSScriptRoot/Find-XunitConsole.ps1
    & $xunit ".\SpecFlow\bin\$Configuration\EmsApi.Tests.SpecFlow.dll"
}
finally {
    Pop-Location
}