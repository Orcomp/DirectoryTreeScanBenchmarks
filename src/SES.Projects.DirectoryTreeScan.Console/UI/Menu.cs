// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Menu.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Console
{
	using System.Collections.Generic;
	using System.Linq;

	public class Menu
	{
		public Menu(string menuText, IEnumerable<MenuItem> menuItems)
		{
			MenuText = menuText;
			MenuItems = menuItems.ToList().AsReadOnly();
		}

		public string MenuText { get; }
		public IReadOnlyList<MenuItem> MenuItems { get; }
	}
}