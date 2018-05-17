// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImplementationProvider.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Console
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using BenchmarkDotNet.Attributes;
	using Implementations;
	using Implementations.Interfaces;

	internal interface IImplementationProvider
	{
		Dictionary<string, Type> GetImplementations();
	}

	/// <summary>
	/// Gets the possible implementation types from a benchmark class "Implementations"
	/// property (benchmark parameter)
	/// </summary>
	/// <seealso cref="SES.Projects.DirectoryTreeScan.Console.IImplementationProvider" />
	internal class BenchmarkBasedImplementationProvider : IImplementationProvider
	{
		private readonly object _benchmark;

		public BenchmarkBasedImplementationProvider(object benchmark)
		{
			_benchmark = benchmark;
		}

		public Dictionary<string, Type> GetImplementations()
		{
			var result = new Dictionary<string, Type>();
			var paramsAttribute = (ParamsAttribute) _benchmark
				.GetType()
				.GetProperty("Implementation")
				?.GetCustomAttributes(typeof(ParamsAttribute), false)
				.FirstOrDefault();

			if (paramsAttribute == null)
			{
				return result;
			}

			foreach (string key in paramsAttribute.Values)
			{
				result.Add(key, typeof(IDirectoryScanner<object>).Assembly.GetType($"{typeof(DefaultNamespace).Namespace}.{key}") ?? throw new InvalidOperationException());
			}

			return result;
		}
	}
}