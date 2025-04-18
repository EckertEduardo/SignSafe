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
} else {
    Write-Host "Certificate already exists at '$certPath'" -ForegroundColor Yellow
}

# Trust the certificate
Write-Host "Trusting the development certificate..." -ForegroundColor Cyan
dotnet dev-certs https --trust
Write-Host "Certificate trusted successfully." -ForegroundColor Green

Write-Host "--------------------------------------------//--------------------------------------------"

# Start Docker Compose in another window
Write-Host "Starting Docker Compose in a new window..." -ForegroundColor Cyan
try {
    Start-Process powershell -ArgumentList "-NoExit", "-ExecutionPolicy Bypass", "-File docker-compose-up-setup.ps1"
    Write-Host "Docker Compose is running in a separate window." -ForegroundColor Green
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
