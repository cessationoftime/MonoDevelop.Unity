# Parse Unity XML Documentation from the HTML Docs

Install PHP 5+
git clone git://github.com/mono/mono.git (need access to the mono scripts folder for monodocer scripts)

## Parse the Documentation

### Windows
Install cygwin (need to be able to run bash scripts)
Use the builddocs_win.php script

add to PATH environment variable:
	C:\cygwin\bin;
	C:\Program Files (x86)\PHP;
	C:\Program Files (x86)\Unity\MonoDevelop\bin;
	C:\Program Files (x86)\Unity\Editor\Data\Mono\lib\mono\2.0;
	C:\Users\cvanvranken\gits\mono\scripts;

Install assemblies to the GAC (C:\Windows\assembly) so MonoDoc will find them.  To install drag and drop into the GAC folder.
	C:\Program Files (x86)\Unity\MonoDevelop\bin\monodoc.dll
	[ICSharpCode.SharpZipLib]: http://sourceforge.net/projects/sharpdevelop/files/SharpZipLib/

### Mac\Linux\Unix
	Use the builddocs_unix.php script
## Setup the Script
change this line in the php script to the version of Unity that you are parsing from.
	define("DOC_SINCE", "3.4.0f5");

modify these three lines to point to the correct paths on your system
	define("MONODOC_PATH", "c:\\Users\\cvanvranken\\gits\\mono\\scripts\\");
	define("FRAMEWORKS_PATH", "c:\\Users\\cvanvranken\\gits\\MonoDevelop.Unity\\Documentation\\Libraries\\");
	define("SCRIPTREFERENCE_PATH","/Progra~2/Unity/Editor/Data/Documentation/Documentation/ScriptReference/");	

## Doing the Parsing
run the Documentation\builddocs.php script to parse the HTML files in SCRIPTREFERENCE_PATH.  This will create the Source\, Release\ and Logs\.
Source\ is the direct result of the parsing. Release\ is what you get when you combine the files from Source\ and Logs\ are the logs from the MonoDoc scripts.

   cd Documentation
   php builddocs.php

## Installing the Documentation
	

## Creating the msi installer

	Update the .Net solution project at the root level of this project. Get it to compile...