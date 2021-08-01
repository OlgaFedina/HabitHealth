using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }
        
        private void NewLayout(object sender, EventArgs e)
        {
            //gets name of button that was pressed
            String btnSource = (sender as Button).Text;
            ArrayList nextSet = parseList(btnSource + ".txt"); //replace with database queries
            
            var layout = new StackLayout();
            var btn = new Button { Text = btnSource, FontSize = 30, TranslationY = 30 };
            this.Content = layout;
            layout.Children.Add(btn);
        }

        private ArrayList parseList(String filename)
        {
            ArrayList temp = new ArrayList();
            
            
        }
    }
}