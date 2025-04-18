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
