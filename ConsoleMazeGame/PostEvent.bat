rd /s /q  "$(TargetDir)\Resources"
mkdir "$(TargetDir)\Resources"
xcopy /S /E /Y /H "$(ProjectDir)\Resources" "$(TargetDir)\Resources"



