// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryScannerBenchMark.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;
	using BenchmarkDotNet.Attributes;
	using Interfaces;

	/// <summary>
	/// Benchmark .NET Benchmark class
	/// </summary>
	public class DirectoryScannerBenchMark
	{
		[Params(
			nameof(BuiltInDirectoryScanner),
			//nameof(FastDirectoryEnumeratorScanner), // causes stackoverflow
			nameof(FastFileInfoScanner),
			//nameof(DirectoryEnumerationAsyncScanner), // Had a hardcoded SearchOption.TopDirectoryOnly. but after the fix still has errors
			nameof(DirectoryTreeWalkerScanner)
		)]
		public string Implementation { get; set; }

		[Params(
			//SizeParameter.Small
			SizeParameter.Medium
			// SizeParameter.Large
		)]
		public SizeParameter Size { get; set; }

		[Params(
			// SelectivityParameter.Low
			//, SelectivityParameter.Average
			SelectivityParameter.High
		)]
		public SelectivityParameter Selectivity { get; set; }

		private IDirectoryScanner<object> _contestant;
		private string _path;
		private string _searchPattern;

		[GlobalSetup]
		public void Setup()
		{
			_path = new SizeParameterHelper().GetValue(Size);
			_searchPattern = new SelectivityParameterHelper().GetValue(Selectivity);

			_contestant = ImplementationFactory.CreateInstance(Implementation);
		}

		[Benchmark]
		public void GetFileInfo()
		{
			Enumerate(_contestant.EnumerateFiles(_path, _searchPattern));
		}

		/// <summary>
		/// Enumerates on specified enumerable.
		/// Note: Benchmark.NET forces to use Release build.
		/// Must check if this method is not optimized out
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable">The enumerable.</param>
		[MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
		public void Enumerate<T>(IEnumerable<T> enumerable)
		{
			foreach (var item in enumerable)
			{
			}
		}
	}
}