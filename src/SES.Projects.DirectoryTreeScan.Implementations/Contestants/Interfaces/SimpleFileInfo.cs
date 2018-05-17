// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleFileInfo.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations.Interfaces
{
	public struct SimpleFileInfo
	{
		public SimpleFileInfo(string filename, long size)
		{
			Filename = filename;
			Size = size;
		}

		public string Filename { get; }
		public long Size { get; }
	}
}