using System;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core
{
	public class CustomMvxFormsPagePresenter : MvxFormsPagePresenter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MvvmCross.Forms.Views.MvxFormsPagePresenter" /> class.
		/// </summary>
		/// <param name="platformPresenter">The native platform presenter from where the MvxFormsPagePresenter is created</param>
		public CustomMvxFormsPagePresenter(IMvxFormsViewPresenter platformPresenter)
			: base(platformPresenter)
		{
		}

		public override async Task<bool> ShowContentPage(Type view, MvxContentPagePresentationAttribute attribute, MvxViewModelRequest request)
		{
			var page = await CloseAndCreatePage(view, request, attribute);
			if (FormsApplication.MainPage is TabbedPage tabbedPage 
				&& tabbedPage.CurrentPage is NavigationPage navigationPage)
			{
				await PushOrReplacePage(navigationPage, page, attribute);
				return true;
			}

			await PushOrReplacePage(FormsApplication.MainPage, page, attribute);
			return true;
		}
	}
}
