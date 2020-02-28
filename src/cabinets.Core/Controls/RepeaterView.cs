using System.Collections;
using Xamarin.Forms;

namespace cabinets.Core.Controls
{
	public class RepeaterView : StackLayout
	{
		#region Data
		#region Static
		public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
			nameof(ItemTemplate),
			typeof(DataTemplate),
			typeof(RepeaterView),
			default(DataTemplate));
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
			nameof(ItemsSource),
			typeof(ICollection),
			typeof(RepeaterView),
			null,
			BindingMode.OneWay,
			propertyChanged: ItemsChanged);
		#endregion
		#endregion

		#region .ctor
		public RepeaterView() => Spacing = 0;
		#endregion

		#region Properties
		public ICollection ItemsSource
		{
			get => (ICollection) GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		public DataTemplate ItemTemplate
		{
			get => (DataTemplate) GetValue(ItemTemplateProperty);
			set => SetValue(ItemTemplateProperty, value);
		}
		#endregion

		#region Overridable
		protected virtual View ViewFor(object item)
		{
			View view = null;

			if (ItemTemplate != null)
			{
				var content = ItemTemplate.CreateContent();

				view = content is View view1 ? view1 : ((ViewCell) content).View;

				view.BindingContext = item;
			}

			return view;
		}
		#endregion

		#region Private
		private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!(bindable is RepeaterView control))
			{
				return;
			}

			control.Children.Clear();

			var items = (ICollection) newValue;

			if (items == null)
			{
				return;
			}

			foreach (var item in items)
			{
				control.Children.Add(control.ViewFor(item));
			}
		}
		#endregion
	}
}
