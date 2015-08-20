call "%vs110comntools%vsvars32.bat"
msbuild "%cd%\Psd.sln" /p:Configuration=Release
xcopy "%cd%\bin\release\nsync" "%cd%\bin" /exclude:ignorelist.txt /y