// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImplementationFactory.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Implementations
{
	using System;
	using Interfaces;

	public class ImplementationFactory
	{
		public static IDirectoryScanner<object> CreateInstance(string typeName)
		{
			return (IDirectoryScanner<object>) Activator.CreateInstance(typeof(IDirectoryScanner<object>).Assembly.GetType($"{typeof(DefaultNamespace).Namespace}.{typeName}") ?? throw new InvalidOperationException());
		}
	}
}