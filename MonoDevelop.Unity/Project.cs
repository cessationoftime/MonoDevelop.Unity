//
// MonoDevelop.Unity/Project.cs
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
using System.Collections.Generic;
using System.Xml;
using System.IO;

using MonoDevelop.Core;
using MonoDevelop.Core.ProgressMonitoring;
using MonoDevelop.Core.Serialization;
using MonoDevelop.Projects;

namespace MonoDevelop.Unity
{
	
	
	public class Project : DotNetProject
	{
		
		public Project ()
			: base ()
		{
			Init ();
		}
		
		public Project (string languageName)
			: base (languageName)
		{
			Init ();
		}
		
		public Project (string languageName, ProjectCreateInformation info, XmlElement projectOptions)
			: base (languageName, info, projectOptions)
		{
			Init ();
		}
		
		void Init ()
		{
			
		}
		
		public override SolutionItemConfiguration CreateConfiguration (string name)
		{
			DotNetProjectConfiguration conf = (DotNetProjectConfiguration) base.CreateConfiguration (name);
			//TODO add environment variable conf.CompilationParameters			
			return conf;
		}
		
		public override ClrVersion[] SupportedClrVersions {
			get {
				return new ClrVersion[] { ClrVersion.Clr_2_1 };
			}
		}
		
		public override string ProjectType {
			get { return "Unity"; }
		}
		
		public override bool IsLibraryBasedProjectType {
			get { return true; }
		}
		
		protected override bool OnGetCanExecute (MonoDevelop.Projects.ExecutionContext context, string configuration)
		{
			return false;
		}
		
		protected override void DoExecute (IProgressMonitor monitor, ExecutionContext context, string configuration)
		{
		}
	}
}