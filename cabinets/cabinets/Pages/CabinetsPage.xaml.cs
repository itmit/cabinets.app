using cabinets.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CabinetsPage : ContentPage
    {
        public CabinetsPage()
        {
            InitializeComponent();
            BindingContext = new CabinetsViewModel();
        }
    }
}