using cabinets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsViewCell : ViewCell
    {
        public NewsViewCell()
        {
            InitializeComponent();
            BindingContext = new NewsViewModel();
        }
    }
}