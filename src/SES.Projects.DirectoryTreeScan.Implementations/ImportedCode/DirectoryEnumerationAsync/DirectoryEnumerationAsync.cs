// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryEnumerationAsync.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// This is a refactored and "embedded not related functionality" striped version of:
	/// https://github.com/GregAskew/DirectoryEnumeratorAsync
	/// </summary>
	public class DirectoryEnumerationAsync
	{
		/// <summary>
		/// Directories to exclude from enumeration.
		/// </summary>
		private static IReadOnlyList<string> DirectoryExclusions { get; set; }

		private static int TasksCompleted { get; set; }
		private static int TasksCreated { get; set; }
		private static readonly object TasksCreatedCompletedLockObject = new object();

		public static async Task<ConcurrentDictionary<string, FileInfo>> GetFileSystemEntriesAsync(
			FileInfo pathFileInfo,
			ConcurrentBag<Task> tasks,
			IReadOnlyList<string> directoryExclusions = null,
			ConcurrentDictionary<string, FileInfo> fileSystemEntries = null,
			bool continueOnUnauthorizedAccessExceptions = true,
			bool continueOnPathTooLongExceptions = true)
		{
			if (directoryExclusions == null)
			{
				directoryExclusions = new List<string>();
			}

			if (fileSystemEntries == null)
			{
				fileSystemEntries = new ConcurrentDictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);
			}

			if (pathFileInfo == null)
			{
				throw new ArgumentNullException(nameof(pathFileInfo));
			}

			try
			{
				await Task.Run(() =>
				{
					try
					{
						lock (TasksCreatedCompletedLockObject)
						{
							TasksCreated++;
						}

						if (!pathFileInfo.Attributes.HasFlag(FileAttributes.Directory))
						{
							throw new ArgumentException($"Path is not a directory: {pathFileInfo.FullName}");
						}

						foreach (var fileSystemEntryPath in Directory.EnumerateFileSystemEntries(
							pathFileInfo.FullName, "*", SearchOption.AllDirectories))
						{
							var childFileInfo = new FileInfo(fileSystemEntryPath);
							FileInfo placeHolder = null;
							fileSystemEntries.AddOrUpdate(childFileInfo.FullName, childFileInfo, (TKey, TOldValue) => placeHolder);

							if (childFileInfo.Attributes.HasFlag(FileAttributes.Directory)
							    && !childFileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
							{
								if (directoryExclusions.Any(x => childFileInfo.FullName.IndexOf(x, StringComparison.OrdinalIgnoreCase) > -1))
								{
									continue;
								}

								tasks.Add(Task.Run(async () =>
								{
									await GetFileSystemEntriesAsync(
										childFileInfo,
										tasks,
										directoryExclusions,
										fileSystemEntries,
										continueOnUnauthorizedAccessExceptions,
										continueOnPathTooLongExceptions);
								}));
							}
						}
					}
					finally
					{
						lock (TasksCreatedCompletedLockObject)
						{
							TasksCompleted++;
						}
					}
				});
			}
			catch (UnauthorizedAccessException e)
			{
				if (!continueOnUnauthorizedAccessExceptions)
				{
					throw;
				}
			}
			catch (PathTooLongException e)
			{
				if (!continueOnPathTooLongExceptions)
				{
					throw;
				}
			}

			return fileSystemEntries;
		}
	}
}