Push-Location $PSScriptRoot

nuget.exe restore .\EmsApi.Client.Vs2015.sln
msbuild.exe /p:Configuration=Release .\EmsApi.Client.Vs2015.sln

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