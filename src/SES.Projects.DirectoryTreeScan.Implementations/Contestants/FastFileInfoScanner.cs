// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FastFileInfoScanner.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System.Collections.Generic;
	using System.IO;
	using Interfaces;
	using Opulos.Core.IO;

	public class FastFileInfoScanner : IDirectoryScanner<FastFileInfo>
	{
		IEnumerable<FastFileInfo> IDirectoryScanner<FastFileInfo>.EnumerateFiles(string path, string searchPattern)
		{
			return FastFileInfo.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);
		}
	}
}