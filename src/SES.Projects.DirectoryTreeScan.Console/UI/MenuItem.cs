// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuItem.cs" company="Simply Effective Solutions">
//   Copyright (c) 2008 - 2018 Simply Effective Solutions. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SES.Projects.DirectoryTreeScan.Console
{
	using System;

	public class MenuItem
	{
		public MenuItem(string itemText, Action itemHandler, bool isExitOption = false)
		{
			ItemText = itemText;
			ItemHandler = itemHandler;
			IsExitOption = isExitOption;
		}

		public string ItemText { get; }
		public Action ItemHandler { get; }
		public bool IsExitOption { get; }
	}
}