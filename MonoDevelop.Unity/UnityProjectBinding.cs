//
// MonoDevelop.Unity/ProjectBinding.cs
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


using System;
using System.Xml;

using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;
/*
namespace MonoDevelop.Unity
{

	public class UnityProjectBinding : DotNetProjectBinding
	{
		
		public override string Name {
			get { return "Unity"; }
		}
		
		protected override DotNetProject CreateProject (string languageName, ProjectCreateInformation info, XmlElement projectOptions)
		{	
			UnityProject newUnityProject = new UnityProject(languageName, info, projectOptions);

			if ( PropertyService.Get<string>("Unity.Base.Path", "") != "" )
			{
				if ( Helpers.WhatOS() == Helpers.OS.Mac )
				{
					newUnityProject.AddReference(PropertyService.Get<string>("Unity.Base.Path", "") + Settings.MAC_UNITY_ENGINE);				
				}
				else
				{
					// Not sure if this is the intention of MonoDevelop for Win, but this is what had to get done.
					// We're removing the drive letter
					
					if ( PropertyService.Get<string>("Unity.Base.Path", "").Substring(1,1) == ":" )
					{	
						newUnityProject.AddReference(PropertyService.Get<string>("Unity.Base.Path", "").Substring(2) + Settings.WIN_UNITY_ENGINE);
					}
					else
					{
						newUnityProject.AddReference(PropertyService.Get<string>("Unity.Base.Path", "") + Settings.WIN_UNITY_ENGINE);
					}
				}	
			}
			
			return newUnityProject;
		}
	}
}
*/
