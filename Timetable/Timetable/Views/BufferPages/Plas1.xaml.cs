using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timetable.Views.BufferPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Plas1 : ContentPage
    {
        public Plas1()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            CarouselPage CP = Parent as CarouselPage;
            NotesPage NP = CP.Children[1] as NotesPage;
            NP.ChangeDay(+1);
            CP.CurrentPage = CP.Children[1];
        }

    }
}