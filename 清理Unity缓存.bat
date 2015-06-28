@echo off echo 正在清除Unity3D临时垃圾文件，请稍等......
rd /s /q C:\Users\%username%\AppData\LocalLow\Unity\WebPlayer\Cache
echo 清除完成！ 
echo. & pause