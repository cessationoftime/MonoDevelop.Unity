<?php

//
// Documentation/create.php
//
// Author:
//   Matthew Davey <matthew.davey@dotbunny.com>
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
// REQS:	You need to have PHP 5+ installed, as well as the mdoc tool
//			set from Mono. It's available from their SVN trunk, under
//			scripts (just grab the whole folder).
//

// Internal Settings
define("DOC_SINCE", "2.5.1"); // Change this to the newest Unity version.
define("DOC_EMPTY", "To be added.");
define("DOC_OVERWRITE", true);
define("SHOW_WARNINGS", false);

// Our Paths
define("SOURCE_PATH", "Source/");
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

$allowed_tags = array('see');
$hot_links = array (
	'character controller' => "UnityEngine.CharacterController",
	'rigidbody' => "UnityEngine.Rigidbody",
	'rigidbodies' => "UnityEngine.Rigidbody",
	);
$external_links = array (
	
	);
$remove_links = array ('GUI Scripting Guide', 'Character animation examples', 'Character Controller component');



// =================================================================================
//            No Settings Below This Line --- Change At Your Own Risk
// =================================================================================

// Counters
$warnings['enumeration'] = 0;
$warnings['structure'] = 0;
$errors['enumeration'] = 0;
$errors['structure'] = 0;
$errors['class'] = 0;
$warnings['class'] = 0;


// Create Directories
if (!is_dir(SOURCE_PATH) ) { mkdir(SOURCE_PATH); }
if (!is_dir(LOG_PATH) ) { mkdir(LOG_PATH); }
if (!is_dir(RELEASE_PATH) ) { mkdir(RELEASE_PATH); }

// Check System Locations
if (!is_dir(MONODOC_PATH)) { die("MonoDoc Not Found"); }
if (!is_dir(FRAMEWORKS_PATH)) { die("Unity Framework Files Not Found"); }
if (!is_dir(SCRIPTREFERENCE_PATH)) { die("Unity Script Reference Not Found"); }

// Remove Old Logs & Misc
@unlink(LOG_PATH . "monodocer.log");
@unlink(LOG_PATH . "mdassembler.log");
@unlink(LOG_PATH . "updateDocumentation.log");
@unlink(RELEASE_PATH . "Unity.source");

// Execute Parse/Update of Actual Libraries
exec($monodoc_command);

// Generate Content from Documentation
updateDocumentationSource();

// Combine documentation
exec($mdassembler_command);		

// Create Source File
file_put_contents(RELEASE_PATH . "Unity.source", $monodoc_source_file);
print "\nErrors\n";
print_r($errors);
print "\nWarnings\n";
print_r($warnings);
print "\n";		
				
				
			


// =================================================================================
//                     First Pass Functions --- Oh aren't they pretty?
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
					updateDocumentationSourceDynamic($NamespaceObject['Name'], $TypeObject['Name'], "class");
					break;
				case "Enumeration":
					updateDocumentationSourceEnumeration($NamespaceObject['Name'], $TypeObject['Name']);
					break;
				case "Delegate":
					break;
				case "Structure":
					updateDocumentationSourceDynamic($NamespaceObject['Name'], $TypeObject['Name'], "structure");
					break;
			}
		}
	}
}

function updateForLinks($namespace, $type, $text, $include_type = false)
{
	global $allowed_tags;
	global $remove_links;
	global $hot_links;
	
	while (preg_match("/<a(.*)<\/a>/U", $text, $matches))
	{
		preg_match("/class=\"(.*)\"/U", $matches[0], $matches_class);
		preg_match("/>(.*)\<\/a>/U", $matches[0], $matches_item);

		if ( in_array( $matches_item[1], $remove_links ))
		{
			$text = str_replace($matches[0], "", $text);
		} 
		
		
		// TODO : ADD BETTER LINKING
		
		// What type of link are we looking at
		switch ($matches_class[1])
		{
			// TODO : Check if file exists? 
			case "classlink":
				if ( $include_type)
				{
					$text = str_replace($matches[0], "DOTBUNNY<see cref=\"T:" . $namespace . "." . 
						$type . "." . $matches_item[1] . "\" />DOTBUNNY", $text);
				}
				else
				{
					$text = str_replace($matches[0], "DOTBUNNY<see cref=\"T:" . $namespace . "." . 
						$matches_item[1] . "\" />DOTBUNNY", $text);
				}
				break;
			case "itemlink":				
				if ( $include_type)
				{
					$text = str_replace($matches[0], "DOTBUNNY<see cref=\"P:" . $namespace . "." . 
						$type . "." . $matches_item[1] . "\" />DOTBUNNY", $text);
				}
				else
				{
					$text = str_replace($matches[0], "DOTBUNNY<see cref=\"P:" . $namespace . "." . 
						$matches_item[1] . "\" />DOTBUNNY", $text);
				}
				break;
			default:
				print("No class for: " . $matches_class[1] . " in " . $matches_class[0] . " from " . $text . "\n");
				break;
		}
	}
	
	// Remove
	foreach ( $remove_links as $key)
	{
		$text = str_replace("DOTBUNNY<see cref=\"T:" . $namespace . "." . $key. "\" />DOTBUNNY", "", $text);
		$text = str_replace("DOTBUNNY<see cref=\"P:" . $namespace . "." . $key. "\" />DOTBUNNY", "", $text);
	}
	
	// Hotlinks
	foreach ( $hot_links as $key => $url )
	{
		$text = str_replace($key, "DOTBUNNY<see cref=\"T:" . $url . "\" />DOTBUNNY", $text);
	}
	// Left overs from removing items
	$text = str_replace(", .", ".", $text);
	$text = str_replace(", ,", ",", $text);
	$text = str_replace(",,", ",", $text);
	$text = str_replace("</span>", "", $text);
	$text = str_replace("<span class=\"note\">", "", $text);
	$text = str_replace("<span class=\"variable\">", "", $text);
	$text = str_replace("<tt>", "", $text);
	$text = str_replace("</tt>", "", $text);
	
	if ( trim($text) == "See Also:  and" )
	{
		$text = "";
	}
	
	if ( trim($text) == "See Also:") 
	{
		$text = "";
	}
	
	// USE file:///Applications/Unity/Documentation/ScriptReference/EventType.html as a test bed
	/*
	    * N:MyLibrary (to link to a namespace)
	    * T:MyLibrary.MyType (to link to a type)
	    * C:MyLibrary.MyType(System.String) (to link to a constructor)
	          o Constructor links may also be written as: M:MyLibrary.MyType.#ctor(System.String) 
	    * M:MyLibrary.MyType.MethodName(System.String,MyLibray.MyOtherType) (to link to a method; for ref and out parameters, add an & to the end of the type name)
	    * P:MyLibrary.MyType.IsDefined (to link to a property)
	    * F:MyLibrary.MyType.COUNTER (to link to a field)
	    * E:MyLibrary.MyType.OnChange (to link to an event)
	*/
	

	return trim(real_strip_tags($text, $allowed_tags));
}

function updateDocumentationSourceDynamic($namespace, $type, $doc_type = "class")
{
	global $warnings;
	global $errors;
	
	$objectXML = simplexml_load_file(SOURCE_PATH . $namespace . "/" . $type . ".xml");

	// Find base summary
	$file = @file_get_contents(SCRIPTREFERENCE_PATH . $type . ".html", "r");
	if (!empty($file))
	{
		if(preg_match("/<p class=\"first\">(.*)<\/p>/U", $file, $matches))
		{
			$updated_text = updateForLinks($namespace, $type, $matches[1]);
			if ( !empty($updated_text) )
			{
				$objectXML->Docs->summary = $updated_text;
			}
		}
		else
		{
			$warnings[$doc_type]++;
			if ( SHOW_WARNINGS )
			{
				file_put_contents(LOG_PATH . "updateDocumentation.log", "WARNING: No " . $doc_type ." summary in " . 
					SCRIPTREFERENCE_PATH . $type . ".html" . "\n", FILE_APPEND | LOCK_EX);
			}
		}
		
		$matches = null;
		if (preg_match("/<span class=\"note\">(.*)<\/p>/U", $file, $matches))
		{
			$updated_text = updateForLinks($namespace, $type, $matches[1]);
			if ( !empty($updated_text) )
			{
				$objectXML->Docs->remarks = $updated_text;
			}
		}
	}
	else
	{
		$errors[$doc_type]++;
		file_put_contents(LOG_PATH . "updateDocumentation.log", "ERROR: No " . $doc_type . " file  found (" . 
			SCRIPTREFERENCE_PATH . $type . ".html)" . "\n", FILE_APPEND | LOCK_EX);
	}
	$file = null;


	// Fill out all the enumeration types and fill out their information
	foreach ($objectXML->Members->Member as $MemberObject)
	{
		switch ($MemberObject->MemberType)
		{
			case "Constructor":
				$file_name = $type . "." . $type . ".html";
				$file_type = $doc_type . " Contructor";
				break;
				
			case "Method":
				$file_name = $type . "." . $MemberObject["MemberName"] . ".html";
				
				if ( substr($MemberObject["MemberName"], 0, 3) == "op_" )
				{
					$file_name = $type . "-" . str_replace("op_", "operator_", $MemberObject["MemberName"]) . ".html";
				}
				
				// Special Cases
				$file_name = str_replace("_Addition.html", "_add.html", $file_name);
				$file_name = str_replace("_Subtraction.html", "_subtract.html", $file_name);
				$file_name = str_replace("_Division.html", "_divide.html", $file_name);
				$file_name = str_replace("_Multiply.html", "_multiply.html", $file_name);
				
				
				
				/*$file_name = str_replace("_Inequality.html", "_ne.html", $file_name);
				$file_name = str_replace("_Equality.html", "_eq.html", $file_name);								
				*/
				/*
	ERROR: No Structure Method file found (/Applications/Unity/Documentation/ScriptReference/Vector2-operator_Implicit.html
	ERROR: No Structure Method file found (/Applications/Unity/Documentation/ScriptReference/Vector2-operator_Implicit.html
	ERROR: No Structure Method file found (/Applications/Unity/Documentation/ScriptReference/Vector2-operator_UnaryNegation.html
				*/												
				$file_type = $doc_type . " Method";
				break;
				
			case "Field":
				$file_name = $type . "-" . $MemberObject["MemberName"] . ".html";
				$file_type = $doc_type . " Field";
				break;
				
			case "Property":
				$file_name = $type . "-" . $MemberObject["MemberName"] . ".html";
				$file_type = $doc_type . " Property";
				break;
				
			default: 
				die("No definition for " . $namespace . "." . $type . "." . $MemberObject['MemberName'] . "->" . $MemberObject->MemberType . "\n");
		}
		
		// Load File
		$file = @file_get_contents(SCRIPTREFERENCE_PATH . $file_name, "r");
	
		// No joy, no luv
		if(empty($file)) 
		{
			$errors[$doc_type]++;
			file_put_contents(LOG_PATH . "updateDocumentation.log", "ERROR: No " . $file_type . " file found (" . 
				SCRIPTREFERENCE_PATH . $file_name . "\n", FILE_APPEND | LOCK_EX); 
			continue; 
		}		
		
		if ( $MemberObject->Docs->summary == DOC_EMPTY || DOC_OVERWRITE)
		{			
			// Get first <p> details for the description
			if(preg_match("/<p class=\"details\">(.*)<\/p>/U", $file, $matches))
			{
				$updated_text = updateForLinks($namespace, $type, $matches[1], true);
				if ( !empty($updated_text) )
				{
					$MemberObject->Docs->summary = $updated_text;
				}
			}
			else
			{
				$warnings[$doc_type]++;
				if ( SHOW_WARNINGS )
				{
					file_put_contents(LOG_PATH . "updateDocumentation.log", "WARNING: No " . $file_type . " summary in " . 
						SCRIPTREFERENCE_PATH . $file_name . "\n", FILE_APPEND | LOCK_EX);
				}
			}	
		}
		if ( $MemberObject->Docs->remarks == DOC_EMPTY || DOC_OVERWRITE )
		{
			$matches = null;
			if (preg_match("/<span class=\"note\">(.*)<\/p>/U", $file, $matches))
			{
				$updated_text = updateForLinks($namespace, $type, $matches[1], true);
				if ( !empty($updated_text) )
				{
					$MemberObject->Docs->remarks = $updated_text;
				}
			}
		}
		
	}
	
	// Save File
	file_put_contents(SOURCE_PATH . $namespace . "/" . $type . ".xml",  simpleXMLHack(trim($objectXML->asXML())));

}

function updateDocumentationSourceEnumeration($namespace, $type)
{	
	global $errors;
	global $warnings;
	
	$objectXML = simplexml_load_file(SOURCE_PATH . $namespace . "/" . $type . ".xml");
	
	// Find base summary
	$file = @file_get_contents(SCRIPTREFERENCE_PATH . $type . ".html", "r");
	if (!empty($file))
	{
		if(preg_match("/<p class=\"first\">(.*)<\/p>/U", $file, $matches))
		{
			$updated_text = updateForLinks($namespace, $type, $matches[1]);
			if ( !empty($updated_text) )
			{
				$objectXML->Docs->summary = $updated_text;
			}
		}
		else
		{
			$warnings['enumeration']++;
			if ( SHOW_WARNINGS )
			{
				file_put_contents(LOG_PATH . "updateDocumentation.log", "WARNING: No enumeration summary in " . 
					SCRIPTREFERENCE_PATH . $type . ".html" . "\n", FILE_APPEND | LOCK_EX);
			}
		}
		
		$matches = null;
		if (preg_match("/<span class=\"note\">(.*)<\/p>/U", $file, $matches))
		{
			$updated_text = updateForLinks($namespace, $type, $matches[1]);
			if ( !empty($updated_text) )
			{
				$objectXML->Docs->remarks = $updated_text;
			}		
		}
	}
	else
	{
		$errors['enumeration']++;
		file_put_contents(LOG_PATH . "updateDocumentation.log", "ERROR: No enumeration file  found (" . 
			SCRIPTREFERENCE_PATH . $type . ".html)" . "\n", FILE_APPEND | LOCK_EX);
	}
	
	// Make sure we have nothing kickin' around
	$file = null;
	$matches = null;
	
	
	// Fill out all the enumeration types and fill out their information
	foreach ($objectXML->Members->Member as $MemberObject)
	{		
		if ( $MemberObject->Docs->summary == DOC_EMPTY || DOC_OVERWRITE)
		{			
			// Open Documentation File
			$file = @file_get_contents(SCRIPTREFERENCE_PATH . $type . "." . $MemberObject["MemberName"] . ".html", "r");
			
			// No joy, no luv
			if(empty($file)) 
			{
				$errors['enumeration']++;
				file_put_contents(LOG_PATH . "updateDocumentation.log", "ERROR: No enumeration member file found (" . 
					SCRIPTREFERENCE_PATH . $type . "." . $MemberObject["MemberName"] . ".html)" . "\n", FILE_APPEND | LOCK_EX); 
				continue; 
			}
						
			// Strip Newlines
			$file = str_replace("\n", "", $file);
			
			// Get first <p> details for the description
			if(preg_match("/<p class=\"details\">(.*)<\/p>/U", $file, $matches))
			{
				$updated_text = updateForLinks($namespace, $type, $matches[1]);
				if ( !empty($updated_text) )
				{
					$MemberObject->Docs->summary = $updated_text;
				}
			}
			else
			{
				$errors['enumeration']++;
				if ( SHOW_WARNINGS )
				{
					file_put_contents(LOG_PATH . "updateDocumentation.log", "WARNING: No enumaration member summary in " . 
						SCRIPTREFERENCE_PATH . $type . "." . $MemberObject["MemberName"] . ".html" . "\n", FILE_APPEND | LOCK_EX);
				}
			}	
			
			$matches = null;
			if (preg_match("/<span class=\"note\">(.*)<\/p>/U", $file, $matches))
			{
				$updated_text = updateForLinks($namespace, $type, $matches[1]);
				if ( !empty($updated_text) )
				{
					$MemberObject->Docs->remarks = $updated_text;
				}
			}

		}
	}

	// Save File
	file_put_contents(SOURCE_PATH . $namespace . "/" . $type . ".xml",  simpleXMLHack(trim($objectXML->asXML())));
}

// =================================================================================
//                 Second Pass Functions --- Because we want it all!
// =================================================================================


// =================================================================================
//                    Helper Functions --- Mostly from PHPDoc
// =================================================================================
function simpleXMLHack($xml)
{
	$xml = str_replace("DOTBUNNY&lt;", "<", $xml);
	$xml = str_replace("&gt;DOTBUNNY", ">", $xml);
	
	return $xml;	
}

function real_strip_tags($i_html, $i_allowedtags = array(), $i_trimtext = FALSE) 
{
  if (!is_array($i_allowedtags))
    $i_allowedtags = !empty($i_allowedtags) ? array($i_allowedtags) : array();
  $tags = implode('|', $i_allowedtags);

  if (empty($tags))
    $tags = '[a-z]+';

  preg_match_all('@</?\s*(' . $tags . ')(\s+[a-z_]+=(\'[^\']+\'|"[^"]+"))*\s*/?>@i', $i_html, $matches);

  $full_tags = $matches[0];
  $tag_names = $matches[1];

  foreach ($full_tags as $i => $full_tag) {
    if (!in_array($tag_names[$i], $i_allowedtags))
      if ($i_trimtext)
        unset($full_tags[$i]);
      else
        $i_html = str_replace($full_tag, '', $i_html);
  }

  return $i_trimtext ? implode('', $full_tags) : $i_html;
}