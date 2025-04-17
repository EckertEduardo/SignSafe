# setup.ps1

$certDir = "certificates"
$certPath = "$certDir\signsafe.pfx"
$certPassword = "signSafePassword"

# Ensure the cert directory exists
if (-Not (Test-Path $certDir)) {
    Write-Host "Creating directory for certificate..."
    New-Item -Path $certDir -ItemType Directory
}

# Generate cert if not found
if (-Not (Test-Path $certPath)) {
    Write-Host "Generating self-signed development certificate..."
    dotnet dev-certs https -ep $certPath -p $certPassword
} else {
    Write-Host "Certificate already exists at $certPath"
}

Write-Host "`Starting Docker Compose..."
docker compose up --build
