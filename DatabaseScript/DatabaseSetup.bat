@echo off
sqlcmd -E -S .\SQLExpress -i Data.sql
pause
