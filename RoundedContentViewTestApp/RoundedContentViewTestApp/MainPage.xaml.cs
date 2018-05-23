using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace CarouselViewTestApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var list = new List<string>()
            {
                "g",
                "g",
                "g",
                "g",
                "g",
                "g",
                "g",
                "g",
                "g"
            };
            carousel.ItemsSource = list;
        }
    }
}
