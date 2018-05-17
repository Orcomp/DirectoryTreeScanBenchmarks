// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuiltInDirectoryScanner.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System.Collections.Generic;
	using System.IO;
	using Interfaces;

	public class BuiltInDirectoryScanner : IDirectoryScanner<FileInfo>
	{
		public IEnumerable<FileInfo> EnumerateFiles(string path, string searchPattern)
		{
			return new DirectoryInfo(path).GetFiles(searchPattern, SearchOption.AllDirectories);
		}
	}
}