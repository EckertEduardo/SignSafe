$certDir = "certificates"
$certPath = "$certDir\signsafe.pfx"
$certPassword = "signSafePassword"
$logFile = "setup-log.txt"

# Start logging
Start-Transcript -Path $logFile -Append

Write-Host "`n--- Script started at $(Get-Date) ---`n" -ForegroundColor Cyan

# Check for dotnet
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host "'dotnet' is not installed or not in PATH." -ForegroundColor Red
    Stop-Transcript
    exit 1
}

# Check for docker
if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
    Write-Host "'docker' is not installed or not in PATH." -ForegroundColor Red
    Stop-Transcript
    exit 1
}

# Ensure the cert directory exists
if (-Not (Test-Path $certDir)) {
    Write-Host "Creating directory for certificate..." -ForegroundColor Cyan
    try {
        New-Item -Path $certDir -ItemType Directory | Out-Null
        Write-Host "Directory created at '$certDir'" -ForegroundColor Green
    }
    catch {
        Write-Host "Failed to create directory '$certDir': $_" -ForegroundColor Red
        Stop-Transcript
        exit 1
    }
}

# Generate cert if not found
Write-Host "Generating self-signed development certificate..." -ForegroundColor Cyan
if (-Not (Test-Path $certPath)) {
    try {
        dotnet dev-certs https -ep $certPath -p $certPassword
        Write-Host "Certificate generated at '$certPath'" -ForegroundColor Green
    }
    catch {
        Write-Host "Failed to generate certificate: $_" -ForegroundColor Red
        Stop-Transcript
        exit 1
    }
}
else {
    Write-Host "Certificate already exists at '$certPath'" -ForegroundColor Yellow
}

# Trust the certificate
Write-Host "Trusting the development certificate..." -ForegroundColor Cyan
dotnet dev-certs https --trust
Write-Host "Certificate trusted successfully." -ForegroundColor Green

Write-Host "--------------------------------------------//--------------------------------------------"

Write-Host "Starting Docker Compose" -ForegroundColor Cyan
try {
    # Starting docker compose up
    Write-Host "Starting command docker compose up at $(Get-Date)" -ForegroundColor Cyan

    Write-Host ""
    Write-Host " -----------------------------------------------------------" -ForegroundColor Yellow
    Write-Host "|  WARNING: Do NOT close this window!                       |" -ForegroundColor Yellow
    Write-Host "|  This window runs Docker Compose and keeps it alive.      |" -ForegroundColor Yellow
    Write-Host "|  If you close it, all containers will STOP.               |" -ForegroundColor Yellow
    Write-Host " -----------------------------------------------------------" -ForegroundColor Yellow
    Write-Host ""


    docker compose up --build

}
catch {
    Write-Host "Failed to launch Docker Compose script: $_" -ForegroundColor Red
    Stop-Transcript
    exit 1
}

# End logging
Write-Host "`n--- Script completed at $(Get-Date) ---`n" -ForegroundColor Cyan
Stop-Transcript

Write-Host "You can close this window now!"
