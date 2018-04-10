@echo off
cls

.\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

packages\FAKE.Core\tools\FAKE.exe build.fsx %*
