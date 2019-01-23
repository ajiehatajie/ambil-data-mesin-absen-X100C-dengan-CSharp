You'll have to force your VB.NET and C# app to run in 32-bit mode so the ActiveX control can work.  Project + Properties, Compile tab, scroll down, Advanced Compile Options, set Target CPU to x86.
64 bit SDK register issus£º
Copy all these dll. into c:\windows\syswow64 folder,
then use administrator account to run cmd.exe
enter the following command to reg the control.
cd %windir%\syswow64      [press enter]
regsvr32 zkemkeeper.dll   [press enter]
*notice:
only 64 bit OS need to do this operation!
