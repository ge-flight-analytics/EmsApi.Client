$msBuild = Get-Command "msbuild" -ErrorAction SilentlyContinue

if( $null -eq $msBuild ) {
    if( $IsWindows -eq $false ) {
        # This is powershell core, and we're not on windows.
        return $null
    }

    $vsInfo = & $PSScriptRoot\vswhere.exe -latest -format json | ConvertFrom-Json
    if( $vsInfo ) {
        $msbuildDir = Join-Path $vsInfo.installationPath 'MSBuild'
        $exe = Get-ChildItem $msbuildDir -Recurse -Force -Filter msbuild.exe | Select-Object -First 1 -ExpandProperty FullName
        if( $exe ) {
            $msBuild = Get-Command $exe
        }
    }

    if( $null -eq $msBuild ) {
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
}

return $msBuild