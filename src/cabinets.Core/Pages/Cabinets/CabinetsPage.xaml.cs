﻿using cabinets.Core.ViewModels.Cabinets;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Cabinets
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(NoHistory = true, Animated = false)]
	public partial class CabinetsPage : MvxContentPage<CabinetsViewModel>
	{
		#region .ctor
		public CabinetsPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
		#endregion
	}
}
