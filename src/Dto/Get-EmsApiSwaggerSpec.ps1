#
# .SYNOPSIS
# Downloads the swagger specification from the EMS API and writes it to a file (or stdout).
#
param
(
    [Parameter( Mandatory = $true )]
    [string] $Endpoint,

    [Parameter( Mandatory = $true )]
    [string] $UserName,

    [Parameter( Mandatory = $true )]
    [SecureString] $Password,

    [Parameter()]
    [string] $OutFile
)

# Authenticate
$tokenUri = "{0}/token" -f $Endpoint
$passConverted = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($Password))
$requestBody = "grant_type=password&username={0}&password={1}" -f $UserName, $passConverted
$token = Invoke-RestMethod -Uri $tokenUri -Method Post -Body $requestBody

try
{
    # Return the raw swagger json (we write it to a file to make sure Powershell
    # doesn't try to parse it into a PSObject).
    $auth = @{ Authorization = "Bearer $($token.access_token)" }
    $swaggerUri = "{0}/v2/swagger" -f $Endpoint

    $tempFile = $null
    if( [string]::IsNullOrEmpty( $OutFile ) )
    {
        $OutFile = [System.IO.Path]::GetTempFileName()
        $tempFile = $OutFile
    }

    Invoke-RestMethod -Uri $swaggerUri -Method Get -Headers $auth -OutFile $OutFile
    Get-Content $OutFile -Raw
}
finally
{
    if( $tempFile -ne $null )
    {
        Remove-Item $tempFile -Force
    }
}