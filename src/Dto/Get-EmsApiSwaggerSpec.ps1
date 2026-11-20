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

$apiEndpoint = $Endpoint.TrimEnd( "/" )
if( $apiEndpoint -notmatch "^https?://" )
{
    $apiEndpoint = "https://{0}" -f $apiEndpoint
}
if( -not $apiEndpoint.EndsWith( "/api" ) )
{
    $apiEndpoint = "{0}/api" -f $apiEndpoint
}

$tokenUri = "{0}/token" -f $apiEndpoint
$passwordPointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($Password)
try
{
    $body = @{
        grant_type = "password"
        username = $UserName
        password = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($passwordPointer)
    }
    $response = Invoke-RestMethod -Uri $tokenUri -Method Post -Body $body -ContentType "application/x-www-form-urlencoded" -ErrorAction Stop
}
finally
{
    [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($passwordPointer)
    if( $null -ne $body )
    {
        $body.Clear()
    }
}
$token = $response.access_token
if( [string]::IsNullOrWhiteSpace( $token ) )
{
    throw "The token response did not contain an access_token."
}

try
{
    # Return the raw swagger json (we write it to a file to make sure Powershell
    # doesn't try to parse it into a PSObject).
    $auth = @{ Authorization = ( "Bearer " + $token ) }
    $swaggerUri = "{0}/v2/swagger" -f $apiEndpoint

    $tempFile = $null
    if( [string]::IsNullOrEmpty( $OutFile ) )
    {
        $OutFile = [System.IO.Path]::GetTempFileName()
        $tempFile = $OutFile
    }

    Invoke-RestMethod -Uri $swaggerUri -Method Get -Headers $auth -OutFile $OutFile -ErrorAction Stop
    Get-Content $OutFile -Raw
}
finally
{
    if( $tempFile -ne $null )
    {
        Remove-Item $tempFile -Force
    }
}
