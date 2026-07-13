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


$tokenUri = "{0}/api/token" -f $Endpoint  
$passConverted = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($Password))  
$body = @{ grant_type = "password"; username = $UserName; password = $passConverted }
$response = Invoke-RestMethod -Uri $tokenUri -Method Post -Body $body
$token = $response.access_token

try
{
    # Return the raw swagger json (we write it to a file to make sure Powershell
    # doesn't try to parse it into a PSObject).
    $auth = @{ Authorization = "Bearer $($token)" }
    $swaggerUri = "{0}/api/v2/swagger" -f $Endpoint

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