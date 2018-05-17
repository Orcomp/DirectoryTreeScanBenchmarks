// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryEnumerationAsyncScanner.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;
	using Interfaces;

	public class DirectoryEnumerationAsyncScanner : IDirectoryScanner<FileInfo>
	{
		public IEnumerable<FileInfo> EnumerateFiles(string path, string searchPattern)
		{
			// Note: We must wait to finish to start enumerate on the results,
			// because the implementation has no built in parallel support
			// it just multithreaded.

			return DirectoryEnumerationAsync.GetFileSystemEntriesAsync(
				new FileInfo(path),
				new ConcurrentBag<Task>()).Result.Values;
		}
	}
}