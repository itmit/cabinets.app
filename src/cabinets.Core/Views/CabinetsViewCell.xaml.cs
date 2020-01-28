using cabinets.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CabinetsViewCell : ViewCell
    {
        public CabinetsViewCell()
        {
            InitializeComponent();
            BindingContext = new CabinetsViewModel();
        }
    }
}