// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Console
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using BenchmarkDotNet.Running;
	using CommandLine;
	using CommandLineParsing;
	using Implementations;

	public static class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				var options = new Options();
				var parser = new Parser(with => with.HelpWriter = Console.Error);
				if (parser.ParseArgumentsStrict(args, options, () => Environment.Exit(-1)))
				{
					Run(options);
				}
			}
			catch
			{
				Exit("", -5);
			}
		}

		private static void Run(Options options)
		{
			var consoleUI = new ConsoleUI();

			while (true)
			{
				consoleUI.DisplayMainMenu();
				switch (consoleUI.GetMenuChoice())
				{
					case 0:
						Environment.Exit(0);
						break;
					case 1:
						RunBenchMark();
						break;
					case 2:
						SearchDirectory(consoleUI);
						break;
				}
			}
		}

		private static void RunBenchMark()
		{
			var summary = BenchmarkRunner.Run<DirectoryScannerBenchMark>();
			Console.WriteLine(@"Press any key to continue");
			Console.ReadKey();
		}

		private static void SearchDirectory(ConsoleUI consoleUI)
		{
			while (true)
			{
				var benchMark = new DirectoryScannerBenchMark();
				var implementationProvider = new BenchmarkBasedImplementationProvider(benchMark);
				consoleUI.DisplayImplementationMenu(implementationProvider);
				var index = consoleUI.GetMenuChoice();
				if (index == 0)
				{
					return;
				}

				var typeName = implementationProvider.GetImplementations().Keys.OrderBy(k => k).ToArray()[index - 1];
				var implementation = ImplementationFactory.CreateInstance(typeName);
				var rootFolder = consoleUI.GetInput("Please specify root folder", "c:\\program files");
				var searchPattern = consoleUI.GetInput("Please specify searchPattern", "*.exe");

				Console.WriteLine($"\r\nImplementation:\t{typeName}");
				Console.WriteLine($"Root folder:\t{rootFolder}");
				Console.WriteLine($"Search pattern:\t{searchPattern}");

				var w = new Stopwatch();
				w.Start();
				var count = implementation.EnumerateFiles(rootFolder, searchPattern).Count();
				//Thread.Sleep(1000);
				w.Stop();
				Console.WriteLine($"\r\nFound {count} file(s). Operation took {w.ElapsedMilliseconds} msecs.");
				Console.WriteLine(@"Press any key to continue");
				Console.ReadKey();
			}
		}

		private static void Exit(string message, int exitCode, Exception e = null)
		{
			if (e == null)
			{
				Console.Error.WriteLine(message);
			}
			else
			{
				message += e.Message;
			}

			if (exitCode < 0)
			{
				Console.Error.WriteLine($@"Benchmark exited with code: {exitCode}. {message}");
			}

			Environment.Exit(exitCode);
		}
	}
}