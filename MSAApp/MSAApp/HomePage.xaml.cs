using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSAApp.DataModels;
using Geolocator.Plugin;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MSAApp
{
    public partial class HomePage : ContentPage
    {
        Times times = new Times();
        Users user;
        ObservableCollection<Output> list = new ObservableCollection<Output>();

        public HomePage()
        {
            InitializeComponent();
            listView.ItemTemplate = new DataTemplate(typeof(CustomCell));
            user = Application.Current.Properties["name"] as Users;

            save.IsVisible = false;
            test.Text += ", " + user.Name;
            test.Clicked += GetTimes;
            save.Clicked += Save_Clicked;
            progress.IsVisible = false;
        }


        public async void GetLocation()
        {
            var position = await CrossGeolocator.Current.GetPositionAsync();

            user.lon = position.Latitude;
            user.lat = position.Longitude;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {

            await DatabaseHandler.DatabaseHandlerInstance.UpdateInfo(user);

            progress.IsVisible = true;
        }

        public async void GetTimes(object sender, EventArgs e)
        {
            GetLocation();

            times = await TimesAPI.timesAPIinstance.RefreshDataAsync(user.lat, user.lon);

            list.Add(new Output { title = "Sunrise", time = times.result.Sunrise.TimeOfDay.ToString(), imageSource = "sunrise.jpg" });
            list.Add(new Output { title = "Sunset", time = times.result.Sunset.TimeOfDay.ToString(), imageSource = "sunset.jpg" });
            list.Add(new Output { title = "Solar noon", time = times.result.solar.TimeOfDay.ToString(), imageSource = "noon.jpg" });
            list.Add(new Output { title = "Day length", time = times.result.daylength.TimeOfDay.ToString(), imageSource = "day.png" });

            listView.ItemsSource = list;
            Label timeLeft = new Label();

            if (DateTime.Now.ToString("tt") == "PM")
            {
                if ((DateTime.Now.Hour + 12) < times.result.Sunset.Hour)
                {
                    timeLeft.Text = (times.result.Sunset.TimeOfDay - DateTime.Now.TimeOfDay).ToString();
                }

                else timeLeft.Text = (times.result.Sunrise.TimeOfDay - DateTime.Now.TimeOfDay).ToString();
            }
            else
            {
                if (DateTime.Now.Hour > times.result.Sunrise.Hour)
                {
                    timeLeft.Text = (times.result.Sunset.TimeOfDay - DateTime.Now.TimeOfDay).ToString();
                }

                else timeLeft.Text = (times.result.Sunrise.TimeOfDay - DateTime.Now.TimeOfDay).ToString();
            }

            test.IsVisible = false;
            save.IsVisible = true;
        }
        
    }

    public class CustomCell : ViewCell
    {
        public CustomCell()
        {
            //instantiate each of our views
            var image = new Image();
            StackLayout cellWrapper = new StackLayout();
            StackLayout horizontalLayout = new StackLayout();
            Label left = new Label();
            Label right = new Label();

            //set bindings
            left.SetBinding(Label.TextProperty, "title");
            right.SetBinding(Label.TextProperty, "time");
            image.SetBinding(Image.SourceProperty, "imageSource");


            //Set properties for desired design
            cellWrapper.BackgroundColor = Color.FromHex("#217ff3");
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            right.VerticalOptions = LayoutOptions.Center;
            left.VerticalOptions = LayoutOptions.Center;
            right.HorizontalOptions = LayoutOptions.CenterAndExpand;
            left.HorizontalOptions = LayoutOptions.EndAndExpand;
            left.TextColor = Color.White;
            right.TextColor = Color.White;
            left.FontSize = 20;
            right.FontSize = 20;
            
            //add views to the view hierarchy
            horizontalLayout.Children.Add(image);
            horizontalLayout.Children.Add(left);
            horizontalLayout.Children.Add(right);
            cellWrapper.Children.Add(horizontalLayout);
            View = cellWrapper;
        }
    }
}
