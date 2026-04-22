param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../../"
$appFolder = Join-Path $slnFolder "BlzaorBookStore"


Write-Host "********* BUILDING Application *********" -ForegroundColor Green

$hostFolder = Join-Path $slnFolder "BlzaorBookStore.Host"
Set-Location $hostFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t blzaorbookstore:$version .

### ALL COMPLETED
Write-Host "********* COMPLETED *********" -ForegroundColor Green
Set-Location $currentFolder
exit $LASTEXITCODE