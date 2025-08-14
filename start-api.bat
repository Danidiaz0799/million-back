@echo off
echo Starting Real Estate API...
echo.
echo Make sure MongoDB is running on localhost:27017
echo.
echo Swagger will be available at: https://localhost:5064/swagger
echo API Health check: https://localhost:5064/api/health
echo.
dotnet run --project RealEstate.Api