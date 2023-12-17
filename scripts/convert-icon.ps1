Add-Type -AssemblyName System.Drawing

$pngPath = Resolve-Path "assets/logo.png"
$icoPath = Join-Path (Get-Location) "src/AirlineApp/Resources/app_icon.ico"

$img = [System.Drawing.Image]::FromFile($pngPath)
$bmp = New-Object System.Drawing.Bitmap($img, [System.Drawing.Size]::new(256, 256))
$hIcon = $bmp.GetHicon()
$icon = [System.Drawing.Icon]::FromHandle($hIcon)

$stream = [System.IO.File]::Create($icoPath)
$icon.Save($stream)
$stream.Close()

$img.Dispose()
$bmp.Dispose()

Write-Host "Created $icoPath successfully!" -ForegroundColor Green
