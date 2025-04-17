#!/bin/bash

CERT_DIR="SignSafe.Certs"
CERT_PATH="$CERT_DIR/signsafe.pfx"
CERT_PASSWORD="signSafePassword"

# Ensure the cert directory exists
if [ ! -d "$CERT_DIR" ]; then
  echo "🔧 Creating directory for certificate..."
  mkdir "$CERT_DIR"
fi

# Generate cert if not found
if [ ! -f "$CERT_PATH" ]; then
  echo "🔐 Generating self-signed development certificate..."
  dotnet dev-certs https -ep "$CERT_PATH" -p "$CERT_PASSWORD"
else
  echo "✅ Certificate already exists at $CERT_PATH"
fi

echo -e "\n🚀 Starting Docker Compose..."
docker compose up --build
