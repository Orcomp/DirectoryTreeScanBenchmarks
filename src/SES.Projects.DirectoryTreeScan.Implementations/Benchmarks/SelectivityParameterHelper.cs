// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectivityParameterHelper.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System;

	public class SelectivityParameterHelper
	{
		public string GetValue(SelectivityParameter parameter)
		{
			switch (parameter)
			{
				case SelectivityParameter.Low:
					return "*.*";
				case SelectivityParameter.Average:
					return "*.exe";
				case SelectivityParameter.High:
					return "*.notexisting";

				default:
					throw new ArgumentOutOfRangeException(nameof(parameter), parameter, null);
			}
		}
	}
}