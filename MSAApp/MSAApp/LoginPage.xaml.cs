using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSAApp.DataModels;

using Xamarin.Forms;

namespace MSAApp
{
    public partial class LoginPage : ContentPage
    {
        public string loggedName;

        public LoginPage()
        {
            InitializeComponent();
            name.IsVisible = false;
            register2.IsVisible = false;
            register1.Clicked += Register1_Clicked;
            register2.Clicked += Register2_Clicked;
            login.Clicked += Login_Clicked;
            back.IsVisible = false;
            back.Clicked += Back_Clicked;
            act.IsVisible = false;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            back.IsVisible = false;
            login.IsVisible = true;
            name.IsVisible = false;
            register1.IsVisible = true;
            register2.IsVisible = false;
            message.Text = "";
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            act.IsVisible = true;
            if (email.Text == null || password.Text == null)
            {
                message.Text = "Fill the fields";
            }
            else
            {
                try
                {

                    List<Users> users = await DatabaseHandler.DatabaseHandlerInstance.GetUsers();
                    int index = 0;

                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].Email == email.Text && users[i].Password == password.Text)
                        {
                            loggedName = users[i].Name;
                            index = i;
                        }
                    }
                    if (loggedName != null)
                    {

                        Application.Current.Properties["name"] = users[index];
                        await Navigation.PushAsync(new HomePage());

                        //message.Text = "Hello, " + loggedName;
                    }
                    else message.Text = "Wrong email or password";
                }
                catch (Exception ex)
                {
                    message.Text = ex.Message;
                }
            }
            act.IsVisible = false;
        }

        private void Register1_Clicked(object sender, EventArgs e)
        {
            name.IsVisible = true;
            login.IsVisible = false;
            back.IsVisible = true;
            register2.IsVisible = true;
            register1.IsVisible = false;
            message.Text = "";
        }

        private async void Register2_Clicked(object sender, EventArgs e)
        {
            act.IsVisible = true;
            if (email.Text == null || name.Text == null || password.Text == null)
            {
                message.Text = "Fill the fields";
            }
            else
            {
                try
                {
                    Users user = new Users()
                    {
                        Email = email.Text,
                        Name = name.Text,
                        Password = password.Text
                    };

                    await DatabaseHandler.DatabaseHandlerInstance.AddUser(user);

                    message.Text = "Registration successful!";
                }
                catch (Exception ex)
                {
                    message.Text = ex.Message;
                }
            }
            act.IsVisible = false;
        }
    }
}
