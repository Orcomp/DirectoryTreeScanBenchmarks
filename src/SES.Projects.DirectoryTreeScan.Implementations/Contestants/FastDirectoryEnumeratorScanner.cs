// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FastDirectoryEnumeratorScanner.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System.Collections.Generic;
	using System.IO;
	using Interfaces;

	public class FastDirectoryEnumeratorScanner : IDirectoryScanner<FileData>
	{
		IEnumerable<FileData> IDirectoryScanner<FileData>.EnumerateFiles(string path, string searchPattern)
		{
			return FastDirectoryEnumerator.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);
		}
	}
}