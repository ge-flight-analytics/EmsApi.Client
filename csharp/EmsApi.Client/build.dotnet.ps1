Push-Location $PSScriptRoot

dotnet restore "EmsApi.Client.sln"
dotnet build "EmsApi.Client.sln"

$packArgs = @( "pack", ".\EmsApi.Client.nuspec", "-OutputDirectory .\bin" )
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

Pop-Location