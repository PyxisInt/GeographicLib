# Update-NuspecVersion.ps1
param (
    [string]$nuspecPath,
    [string]$newVersion
)

[xml]$nuspec = Get-Content $nuspecPath
$nuspec.package.metadata.version = $newVersion
$nuspec.Save($nuspecPath)