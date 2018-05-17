// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryTreeWalkerScanner.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System.Collections.Generic;
	using System.IO;
	using Interfaces;

	public class DirectoryTreeWalkerScanner : IDirectoryScanner<FileInfo>
	{
		public IEnumerable<FileInfo> EnumerateFiles(string path, string searchPattern)
		{
			return DirectoryTreeWalker.Walk(new DirectoryInfo(path), searchPattern);
		}
	}
}