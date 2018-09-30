$xunit = Get-Command "xunit.console" -ErrorAction SilentlyContinue

if( $null -eq $xunit ) {
    if( $env:xunit20 ) {
        $xunit = Get-Command (Join-Path $env:xunit20 'xunit.console') -ErrorAction SilentlyContinue
    }

    if( $null -eq $xunit ) {
        $packages = (nuget locals global-packages -list).Replace( 'global-packages: ', '' )
        $runnerRoot = Join-Path $packages 'xunit.runner.console'
        if( -not ( Test-Path $runnerRoot ) ) {
            throw "Could not locate xunit.runner.console directory under $packages"
        }

        # There's a version directory under this one.
        $versionDir = Get-ChildItem $runnerRoot -Directory | Select -First 1 -ExpandProperty FullName
        $runnerDir = Join-Path $versionDir 'tools/net462'
        if( -not ( Test-Path $runnerDir ) ) {
            throw "Could not locate xunit runner directory at $runnerDir"
        }

        $xunit = Get-Command (Join-Path $runnerDir 'xunit.console.exe')
    }

    if( $null -eq $xunit ) {
        throw "Could not locate an xunit runner to use."
    }
}

return $xunit