@echo off
echo ============================================
echo    WinFormsLottery Git 上传脚本
echo ============================================
echo.

cd /d "%~dp0"

echo [1/4] 添加所有文件...
git add -A
if errorlevel 1 (
    echo 错误：git add 失败
    pause
    exit /b 1
)
echo 完成！
echo.

echo [2/4] 提交更改...
git commit -m "Update WinFormsLottery project"
if errorlevel 1 (
    echo 错误：git commit 失败（可能没有更改需要提交）
    echo 尝试仅推送...
    goto push
)
echo 完成！
echo.

:push
echo [3/4] 推送到 GitHub...
git push -u origin main
if errorlevel 1 (
    echo 错误：git push 失败
    pause
    exit /b 1
)
echo 完成！
echo.

echo ============================================
echo    上传成功！🎉
echo ============================================
echo.
pause
