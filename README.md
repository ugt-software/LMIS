# LMIS
https://tenders.procurement.gov.ge/public/?go=190216&amp;lang=ge

# Instruction

1. Run *iis.config.bat* using Run As Administrator
2. Add MSBuild in External Tools of Visual Studio,
  1. Open Visual Studio,
  2. Open *Tools->External Tools*
  3. Click Add button
  4. Enter in Titile - MSBuild
  5. Enter in Command - *C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe*
  6. Enter in Arguments - *$(ItemPath)*
  7. Check Use Output window checkbox
  8. Click OK button

3. In Solution Explorer select each *CITI.EVO.CommonData.MSBuild.xml* file after click on *Tools->MSBuild*
4. Do same as step 3 for each **.MSBuild.xml* file





