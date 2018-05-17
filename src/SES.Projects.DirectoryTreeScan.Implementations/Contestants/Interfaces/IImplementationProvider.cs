// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImplementationProvider.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations.Interfaces
{
	using System;
	using System.Collections.Generic;

	internal interface IImplementationProvider
	{
		Dictionary<string, Type> GetImplementations();
	}
}