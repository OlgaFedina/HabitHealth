using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }

        private void ScClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LandingPage());
        }
        
        private void HaqClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HAQ());
        }
        
        
        
        private void Button_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}