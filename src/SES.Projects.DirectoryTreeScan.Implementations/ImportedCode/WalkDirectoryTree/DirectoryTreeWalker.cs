// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryTreeWalker.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// This code is based what we found on:
	/// https://codereview.stackexchange.com/questions/74156/fastest-way-searching-specific-files?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
	/// Refactored for yield semantics instead of accumulating result in a list.
	/// </summary>
	public class DirectoryTreeWalker
	{
		public static IEnumerable<FileInfo> Walk(DirectoryInfo directoryInfo, string searchPattern)
		{
			// TODO: Can Parallel.ForEach somehow incorporated to yield semantics?
			foreach (var fileInfo in directoryInfo.GetFiles(searchPattern).AsParallel())
			{
				yield return fileInfo;
			}

			foreach (var di in directoryInfo.GetDirectories().AsParallel())
			{
				foreach (var fi in Walk(di, searchPattern).AsParallel())
				{
					yield return fi;
				}
			}
		}
	}
}