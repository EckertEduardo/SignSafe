#!/bin/bash

CERT_DIR="certificates"
CERT_PATH="$(pwd)/$CERT_DIR/signsafe.pfx"
CERT_PASSWORD="signSafePassword"

# If a file (not a directory) named 'certificates' exists, remove it
if [ -f "$CERT_DIR" ]; then
  echo "Warning: '$CERT_DIR' exists as a file. Removing it so a directory can be created..."
  rm "$CERT_DIR"
fi

# Ensure the cert directory exists
echo "Ensuring certificate directory exists at $CERT_DIR..."
mkdir -p "$CERT_DIR"

# Generate cert if not found
if [ ! -f "$CERT_PATH" ]; then
  echo "Generating self-signed development certificate..."
  dotnet dev-certs https -ep "$CERT_PATH" -p "$CERT_PASSWORD"
else
  echo "Certificate already exists at $CERT_PATH"
fi

echo -e "\nStarting Docker Compose..."
docker compose up --build