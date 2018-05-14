$majorVersion=1
$minorVersion=1
$version="$majorVersion.$minorVersion"+"."+(Get-Date -Format yy)+(Get-Date).DayOfYear+"."+(Get-Date -Format HHmm)
$csproj="Ntreev.Library.Psd\Ntreev.Library.Psd.csproj"
$assemblyInfo = "Ntreev.Library.Psd.AssemblyInfo\AssemblyInfo.cs"
Set-Content version.txt $version
(Get-Content $csproj) -replace "(<Version>)(.*)(</Version>)", "`${1}$version`$3" -replace "(<FileVersion>)(.*)(</FileVersion>)", "`${1}$version`$3" -replace "(<AssemblyVersion>)(.*)(</AssemblyVersion>)", "`${1}$majorVersion.$minorVersion.0.0`$3" | Set-Content $csproj
(Get-Content $assemblyInfo) -replace "(AssemblyVersion[(]`").+(`"[)]])", "`${1}$version`$2" -replace "(AssemblyFileVersion[(]`").+(`"[)]])", "`${1}$fileVersion`$2" -replace "(AssemblyInformationalVersion[(]`").+(`"[)]])", "`${1}$fileVersion`$2" | Set-Content $assemblyInfo