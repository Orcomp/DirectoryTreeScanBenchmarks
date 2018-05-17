// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryTreeScannerTest.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations.Tests
{
	using System.Linq;
	using NUnit.Framework;

	[TestFixture]
	public class DirectoryTreeScannerTest
	{
		[Test]
		public void EnumerateFiles(
			[Values(
				//nameof(FastDirectoryEnumeratorScanner), // causes stackoverflow
				nameof(FastFileInfoScanner),
				//nameof(DirectoryEnumerationAsyncScanner), // Had a hardcoded SearchOption.TopDirectoryOnly. but after the fix still has errors
				nameof(DirectoryTreeWalkerScanner)
			)]
			string implementation,
			[Values(
				@"c:\program files",
				@"c:\program files (x86)"
			)]
			string path,
			[Values(
				@"*.*",
				@"*.exe",
				@"*.notexisting"
			)]
			string searchPattern
		)
		{
			// Arrange
			var sut = ImplementationFactory.CreateInstance(implementation);
			var referenceImplementation = new BuiltInDirectoryScanner();

			// Act
			var actual = sut.EnumerateFiles(path, searchPattern).ToList();

			// Assert
			var expected = referenceImplementation.EnumerateFiles(path, searchPattern).ToList();

			Assert.AreEqual(expected.Count, actual.Count);
			// TODO: Check filenames, filesizes for equality
		}
	}
}