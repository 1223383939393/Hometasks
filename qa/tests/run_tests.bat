@echo off
chcp 65001 > nul

echo === SmartDelivery API tests ===

python api_tests.py > results.log 2>&1

echo.
echo Results are saved to tests\results.log
echo.

type results.log

pause