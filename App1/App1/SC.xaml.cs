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
            
            //create new layout stack
            var layout = new StackLayout();
            layout.HorizontalOptions = LayoutOptions.Center;
            layout.VerticalOptions = LayoutOptions.CenterAndExpand;
            
            //add appropriate number of buttons to layout stack
            foreach (String element in nextSet)
            {   
                var btn = new Button { Text = element }; //, FontSize = 30, TranslationY = 30};
                btn.Clicked += new EventHandler(NewLayout);
                btn.CornerRadius = 5;
                btn.Background = Brush.Aqua;
                btn.BorderWidth = 3;
                btn.BorderColor = Color.Blue;
                
                layout.Children.Add(btn);
            }
            
            //set temporary layout stack to active one to display new button set
            this.Content = layout;
            
        }
        
        //redundant method, but is for the purpose of isolating the data input method. once db is integrated, it will be removed.
        //takes in filename, stores it in array, then translates to arraylist
        private ArrayList parseList(String filename)
        {
            ArrayList temp = new ArrayList();
            String[] file = System.IO.File.ReadAllLines(filename);

            foreach (String element in file)
            {
                temp.Add(element);
            }

            return temp;
        }
    }
}