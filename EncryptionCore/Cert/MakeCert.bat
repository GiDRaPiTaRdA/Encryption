cd C:\Program Files (x86)\Windows Kits\8.1\bin\x64
set /p certName="Enter certificate name: "
set /p months="Enter trial in months: "
set /p password="Enter password: "
set pass=%~dp0Certificates
set root=%pass%\%certName%
md %root%

makecert.exe ^
-n "CN=%certName%" ^
-r ^
-pe ^
-a sha1 ^
-m %months% ^
-sky exchange ^
-len 4096 ^
-cy authority ^
-sv %root%\%certName%.pvk ^
%root%\%certName%.cer

pvk2pfx.exe ^
-pvk %root%\%certName%.pvk ^
-spc %root%\%certName%.cer ^
-pfx %root%\%certName%.pfx ^
-po %password%

pause

explorer.exe %root%

