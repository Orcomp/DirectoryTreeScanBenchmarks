// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDirectoryScanner.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations.Interfaces
{
	using System.Collections.Generic;

	/// <summary>
	/// Main common interface to have a "common denominator" for all implementations.
	/// Because some implementations may return with not FileInfo,
	/// instead some other but equally useful information content class,
	/// the interface allows this by utilizing a generic type parameter.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDirectoryScanner<out T>
	{
		/// <summary>
		/// Enumerates the specified files.
		/// Wrapper implementations should prefer defered execution whenever it is possible by
		/// the underlying real implementation
		/// </summary>
		/// <param name="path">The starting root path.</param>
		/// <param name="searchPattern">The search pattern.</param>
		/// <returns>FileInfo enumerable or similar</returns>
		IEnumerable<T> EnumerateFiles(string path, string searchPattern);
	}
}