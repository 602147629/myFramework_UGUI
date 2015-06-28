@echo off
set /a num = 0
for /r %%i in (*.lua)  do (set /a num += 1 & luajit -b %%~fsi %%~fsi & echo %%~fsi)
echo ±‡“ÎΩ≈±æ ˝¡ø£∫%num%
pause