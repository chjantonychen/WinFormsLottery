@echo off
chcp 65001 >nul
echo ============================================
echo    WinFormsLottery 强制推送脚本
echo ============================================
echo.

cd /d "%~dp0"

echo [0/5] 当前目录: %CD%
echo.

echo [1/5] 检查 Git 状态...
git status
echo.

echo [2/5] 添加所有文件...
git add -A
if errorlevel 1 (
    echo 错误：git add 失败
    pause
    exit /b 1
)
echo 完成！
echo.

echo [3/5] 提交更改...
git commit -m "Initial commit - WinFormsLottery project"
if errorlevel 1 (
    echo 错误：git commit 失败
    pause
    exit /b 1
)
echo 完成！
echo.

echo [4/5] 强制推送到 GitHub...
git push -u origin main --force
if errorlevel 1 (
    echo 错误：git push 失败
    pause
    exit /b 1
)
echo 完成！
echo.

echo [5/5] 验证推送结果...
git log --oneline -3
echo.

echo ============================================
echo    推送完成！请检查 GitHub
echo ============================================
echo.
pause
