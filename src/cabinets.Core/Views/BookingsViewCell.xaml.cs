using cabinets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cabinets.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingsViewCell : ViewCell
    {
        public BookingsViewCell()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }
    }
}