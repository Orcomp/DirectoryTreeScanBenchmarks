// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleUI.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Console
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	// ReSharper disable once InconsistentNaming
	internal class ConsoleUI
	{
		private static readonly string MainMenuHeader =
			"Welcome to Directory Tree Scan POC";

		private static readonly string MainMenuQuestion =
			"Enter what would you like to do: ";

		private static readonly IEnumerable<string> MainMenuItems = new List<string>
		{
			"Run Benchmark",
			"Search directory"
		};

		private static readonly string ImplementationMenuHeader =
			"Available implementations";

		private static readonly string ImplementationMenuQuestion =
			"Enter which implementation would you like to use: ";

		private Menu _lastMenu;

		private Menu BuildMenu(string header, IEnumerable<string> itemTexts, string question)
		{
			var exitText = "Exit";
			var menuText = $"{header ?? string.Empty}\r\n\r\n";
			var index = 1;

			itemTexts = itemTexts as string[] ?? itemTexts.ToArray();
			foreach (var itemText in itemTexts)
			{
				menuText += $"{index++:0}. {itemText}\r\n";
			}

			menuText += $"0. {exitText}\r\n";
			menuText += $"\r\n{question ?? string.Empty}";

			return new Menu(menuText,
				itemTexts
					.Select(t => new MenuItem(t, null, false))
					.Union(Enumerable.Repeat(new MenuItem(menuText, null, true), 1)));
		}

		public void DisplayMainMenu()
		{
			DisplayMenu(BuildMenu(MainMenuHeader, MainMenuItems, MainMenuQuestion));
		}

		public void DisplayImplementationMenu(IImplementationProvider implementationProvider)
		{
			DisplayMenu(BuildImplementationMenu(implementationProvider));
		}

		private Menu BuildImplementationMenu(IImplementationProvider implementationProvider)
		{
			return BuildMenu(ImplementationMenuHeader, implementationProvider.GetImplementations().Keys.OrderBy(k => k), ImplementationMenuQuestion);
		}

		private void DisplayMenu(Menu menu)
		{
			_lastMenu = menu;
			Console.Write(menu.MenuText);
		}

		public int GetMenuChoice(Menu menu = null)
		{
			menu = menu ?? _lastMenu;
			do
			{
				var line = Console.ReadLine();
				if (line == null)
				{
					return 0;
				}

				if (int.TryParse(line, out var result) &&
				    result >= 0 &&
				    result <= menu.MenuItems.Count)
				{
					return result;
				}

				Console.WriteLine($@"Valid choices are: 0 - {menu.MenuItems.Count}");
			} while (true);
		}

		public string GetInput(string prompt, string @default = "")
		{
			if (!string.IsNullOrEmpty(@default))
			{
				prompt = $"{prompt} [{@default}]:";
			}

			while (true)
			{
				Console.Write(prompt);
				var line = Console.ReadLine();
				if (line == null)
				{
					Environment.Exit(-1);
				}

				line = line.Trim();

				if (string.IsNullOrEmpty(line) && !string.IsNullOrEmpty(@default))
				{
					return @default;
				}

				if (!string.IsNullOrEmpty(line))
				{
					return line.Trim();
				}
			}
		}
	}
}