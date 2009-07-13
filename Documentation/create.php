<?php

//
// Documentation/create.php
//
// Author(s):
//   Matthew Davey <matthew.davey@dotbunny.com>
//   Matthew Wegner <matthew@flashbangstudios.com>
//
// Copyright (c) 2009 dotBunny Inc. (http://www.dotbunny.com)
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// NOTE: 	I took Matt's wonderful work and adapted it a bit to fit
// 			what we needed for MonoDevelop. Cheers to him for making
//			this process easier on everyone.
// 
// REQS:	You need to have PHP 5+ installed, as well as the mdoc tool
//			set from Mono. It's available from their SVN trunk, under
//			scripts (just grab the whole folder).
//

// Internal Settings
define("DOC_SINCE", "2.5.1"); // Change this to the newest Unity version.
define("DOC_EMPTY", "To be added.");
define("DOC_OVERWRITE", true);

// Our Paths
define("SOURCE_PATH", "Source/");
define("STUBS_PATH", "Stubs/");
define("LOG_PATH", "Logs/");
define("RELEASE_PATH", "Release/");

// System Paths
define("MONODOC_PATH", "/Users/Reaper/SVN/MonoProject/mono/scripts/");
define("FRAMEWORKS_PATH", "/Applications/Unity/Unity.app/Contents/Frameworks/");
define("SCRIPTREFERENCE_PATH","/Applications/Unity/Documentation/ScriptReference/");

// MonoDocer Parsing Command
$monodoc_command = 	MONODOC_PATH . "monodocer -assembly:" . FRAMEWORKS_PATH . 
					"UnityEngine.dll " . FRAMEWORKS_PATH . 
					"UnityEditor.dll -path:" . SOURCE_PATH . 
					" -pretty -since:" . DOC_SINCE . " > " . LOG_PATH . 
					"monodocer.log";

// MonoDoc Assembler Command				
$mdassembler_command = 	MONODOC_PATH . "mdassembler --ecma " . SOURCE_PATH . 
						" --out " . RELEASE_PATH . "Unity > " . LOG_PATH . 
						"mdassembler.log";

// MonoDoc to VS
$mdocexport_command = MONODOC_PATH . "mdoc-export-msxoc " . SOURCE_PATH;

$monodoc_source_file = '<?xml version="1.0"?>
<monodoc>
  <node label="Unity" name="Unity" parent="libraries" />
  <source provider="ecma" basefile="Unity" path="Unity"/>
</monodoc>';

// =================================================================================
//            No Settings Below This Line --- Change At Your Own Risk
// =================================================================================

// Create Directories
if (!is_dir(SOURCE_PATH) ) { mkdir(SOURCE_PATH); }
if (!is_dir(STUBS_PATH) ) { mkdir(STUBS_PATH); }
if (!is_dir(LOG_PATH) ) { mkdir(LOG_PATH); }
if (!is_dir(RELEASE_PATH) ) { mkdir(RELEASE_PATH); }

// Check System Locations
if (!is_dir(MONODOC_PATH)) { die("MonoDoc Not Found"); }
if (!is_dir(FRAMEWORKS_PATH)) { die("Unity Framework Files Not Found"); }
if (!is_dir(SCRIPTREFERENCE_PATH)) { die("Unity Script Reference Not Found"); }

// Remove Old Logs & Misc
//@unlink(LOG_PATH . "monodocer.log");
//@unlink(LOG_PATH . "mdassembler.log");
//@unlink(RELEASE_PATH . "Unity.source");

// Execute Parse/Update of Actual Libraries
//exec($monodoc_command);

// Generate Content from Documentation
updateDocumentationSource();

// Combine documentation
//exec($mdassembler_command);		

// Create Source File
//file_put_contents(RELEASE_PATH . "Unity.source", $monodoc_source_file);
				
				
				
			


// =================================================================================
//                     Functions --- Oh aren't they pretty?
// =================================================================================
function updateDocumentationSource()
{
	$XML = simplexml_load_file(SOURCE_PATH . "index.xml");

	foreach( $XML->Types->Namespace as $NamespaceObject)
	{
		//$NamespaceObject['Name'];
		foreach ($NamespaceObject->Type as $TypeObject)
		{		
			// Because each type of file is a bit different, lets just make specific
			// calls to update each one differently.
			switch($TypeObject['Kind'])
			{
				case "Class":
					updateDocumentationSourceClass($NamespaceObject['Name'], $TypeObject['Name']);
					break;
				case "Enumeration":
					updateDocumentationSourceEnumeration($NamespaceObject['Name'], $TypeObject['Name']);
					break;
				case "Delegate":
					break;
				case "Structure":
					break;
			}
		}
	}
}

function updateDocumentationSourceClass($namespace, $type)
{
	$objectXML = simplexml_load_file(SOURCE_PATH . $namespace . "/" . $type . ".xml");
	
	//$type.$member.html
}

function updateDocumentationSourceEnumeration($namespace, $type)
{	
	$objectXML = simplexml_load_file(SOURCE_PATH . $namespace . "/" . $type . ".xml");
	
	// Find base summary
	$file = @file_get_contents(SCRIPTREFERENCE_PATH . $type . ".html", "r");
	if (!empty($file))
	{
		$summary = DOC_EMPTY;
		if(preg_match("/<p class=\"first\">(.*)<\/p>/U", $file, $matches))
		{
			$summary = trim(strip_tags($matches[1]));
		}
		else
		{
			file_put_contents(LOG_PATH . "updateDocumentation.log", "WARNING: No enumeration summary in " . 
				SCRIPTREFERENCE_PATH . $type . ".html" . "\n", FILE_APPEND | LOCK_EX);
		}
		$objectXML->Docs->summary = $summary;
	}
	$file = null;
	$matches = null;
	$summary = null;
	
	
	// Fill out all the enumeration types and fill out their information
	foreach ($objectXML->Members->Member as $MemberObject)
	{		
		if ( $MemberObject->Docs->summary == DOC_EMPTY || DOC_OVERWRITE)
		{			
			// Open Documentation File
			$file = @file_get_contents(SCRIPTREFERENCE_PATH . $type . "." . $MemberObject["MemberName"] . ".html", "r");
			
			// No joy, no luv
			if(empty($file)) { continue; }
						
			// Strip Newlines
			$file = str_replace("\n", "", $file);
	
			$summary = DOC_EMPTY;
			
			// Get first <p> details for the description
			if(preg_match("/<p class=\"details\">(.*)<\/p>/U", $file, $matches))
			{
				$summary = trim(strip_tags($matches[1]));
			}
			else
			{
				file_put_contents(LOG_PATH . "updateDocumentation.log", "WARNING: No enumaration member summary in " . 
					SCRIPTREFERENCE_PATH . $type . "." . $MemberObject["MemberName"] . ".html" . "\n", FILE_APPEND | LOCK_EX);
			}	
			
			// Replace Summary
			$MemberObject->Docs->summary = $summary;
		}
	}
	
	file_put_contents(SOURCE_PATH . $namespace . "/" . $type . ".xml",  trim($objectXML->asXML()));
}
			
			
			
/// REFERENCE BELOW		
			
// Matt (fbang) stuff to use ...
function GenerateClasses()
{
	
	$fp = fopen(SCRIPTREFERENCE_PATH . '10_reference.Classes.html', "r");
	while($line = fgets($fp))
	{
		if(preg_match("/<li><a href=\"(.*)\.html\" class=\"classlink\">.*<\/a><\/li>/", $line, $matches))
		{
			$thisclass = $matches[1];
		}
	
		if(preg_match("/<li><a href=\"(.*)\" class=\"itemlink\">(.*)<\/a><\/li>/", $line, $matches))
		{
			$list[$thisclass][] = array($matches[1], $matches[2]);
		}
	}

	// iterate our class
	foreach($list as $class => $members)
	{			
		// our header
		$out =  "// intrinsic\nintrinsic class $class";
	
		// extend something?
		if($extend = checkClassInherit($class))
			$out .= " extends $extend";
		
		// start braces
		$out .= "\n{\n";
	
		// iterate all of our members
		foreach($members as $member)
		{
			$url = $member[0];
			$name = $member[1];
		
			// open up the class and extract its line
			$file = file_get_contents(SCRIPTREFERENCE_PATH . $url);
		
			// skip if file is dead
			if(empty($file))
				continue;
		
			// strip newlines
			$file = str_replace("\n", "", $file);
		
			// get first <p> details for the description
			if(preg_match("/<p class=\"details\">(.*)<\/p>/U", $file, $matches))
			{
				$out .= "\t/**\n\t* " . strip_tags($matches[1]) . "\n\t**/\n";
			}
			else
			{
				print "WARNING: No description in $url\n";
			}
		
			// signature line is in the first h3 tag
			if(preg_match("/<h3>(.*)<\/h3>/U", $file, $matches))
			//if(preg_match("/<div class=\"synopsis\">(.*)<\/div>/U", $file, $matches))
			{
				$out .= "\t" . strip_tags($matches[1]) . ";\n\n";
			}
			else
			{
				print "WARNING:  <h3> not found in $url\n";
			}
		}

		// closing brace
		$out .= "}\n";
	
		// open up for writing
		$fp = fopen(STUBS_PATH . "$class.cs", "w+");
		fwrite($fp, $out);
		fclose($fp);
	}
}

function GenerateEnums()
{	
	$fp = fopen(SCRIPTREFERENCE_PATH . '20_class_hierarchy.Enumerations.html', "r");
	while($line = fgets($fp))
	{
		if(preg_match("/<li><a href=\"(.*)\.html\" class=\"classlink\">.*<\/a><\/li>/", $line, $matches))
		{
			$enums[] = $matches[1];
		}
	}

	// iterate our class
	foreach($enums as $enum)
	{			
		// our header
		$out =  "// intrinsic (fake enum)\nintrinsic class $enum";

		// start braces
		$out .= "\n{\n";

		// open up the class and extract its line
		$file = file_get_contents(SCRIPTREFERENCE_PATH . "$enum.html");

		// skip if file is dead
		if(empty($file))
			continue;

		// strip newlines
		$file = str_replace("\n", "", $file);

		$file = preg_replace("/>\s*</", "><", $file);

		// get all of our members
		if(preg_match_all("/<tr><td class=\"class-member-list-name\"><a href=\".*\.html\">(.*)<\/a><\/td><td>\s*<p class=\"class-member-description\">(.*)<\/p><\/td><\/tr>/U", $file, $matches, PREG_SET_ORDER))
		{
			foreach($matches as $match)
			{
				$out .= "\t/**\n\t* " . strip_tags($match[2]) . "\n\t**/\n";
				$out .= "\tstatic var $match[1]:int;\n\n";
			}
		}

		// closing brace
		$out .= "}\n";

		// open up for writing
		$fp = fopen(STUBS_PATH . "$enum.cs", "w+");
		fwrite($fp, $out);
		fclose($fp);
	}	
}

// does a class inherit from something?
function checkClassInherit($class)
{
	$file = file_get_contents(SCRIPTREFERENCE_PATH . "$class.html");
	
	if(preg_match("/Class, inherits from <a href=\"(.*)\.html\" class=\"classlink\">/", $file, $matches))
		return $matches[1];
	else
		return null;
}