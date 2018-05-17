// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SizeParameterHelper.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System;

	public class SizeParameterHelper
	{
		public string GetValue(SizeParameter parameter)
		{
			switch (parameter)
			{
				case SizeParameter.Small:
					return @"c:\Program Files\Common Files";
				case SizeParameter.Medium:
					return @"C:\Source\SES.Projects.Sanitarium";
				case SizeParameter.Large:
					return @"c:\Program Files";
				default:
					throw new ArgumentOutOfRangeException(nameof(parameter), parameter, null);
			}
		}
	}
}