//
// MonoDevelop.Unity/BuildExtension.cs
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
using System.IO;
using System.Collections.Generic;
using MonoDevelop.Core;
using MonoDevelop.Projects;
using MonoDevelop.Core.ProgressMonitoring;
using System.Xml;
using System.Text;

namespace MonoDevelop.Unity
{
	
	
	public class BuildExtension : ProjectServiceExtension
	{
		protected override BuildResult Compile (IProgressMonitor monitor, SolutionEntityItem item, BuildData buildData)
		{
			Project proj = item as Project;
			if (proj == null)
			{
				return base.Compile (monitor, item, buildData);
			}
			else
			{
				return null;
			}
		}
		
		protected override void Clean (IProgressMonitor monitor, SolutionEntityItem item, string configuration)
		{
			Project proj = item as Project;
			if (proj == null)
			{
				base.Clean(monitor, item, configuration);
			}
		}
		
		protected override bool GetNeedsBuilding (SolutionEntityItem item, string configuration)
		{
			Project proj = item as Project;
			if (proj == null)
			{
				return base.GetNeedsBuilding(item, configuration);
			}
			else
			{
				return false;
			}
		}		
		
		protected override BuildResult Build (IProgressMonitor monitor, SolutionEntityItem item, string configuration)
		{
			Project proj = item as Project;
			if (proj == null)
			{
				return base.Build(monitor, item, configuration);
			}
			else
			{
				return null;
			}
		}

	}
}
