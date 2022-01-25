using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mobile.Services;
using Mobile.Models;
using Mobile.Model;

namespace Mobile
{
    public partial class MainPage : ContentPage
    {

        DataService ds;
        private List<UsuarioResponse> items;

        public MainPage()
        {
            InitializeComponent();
            
            AtualizaDados();
        }

        private async void AtualizaDados()
        {
            DataService ds = new Services.DataService();
            items = await ds.GetUsersAsync();
            listUsers();
        }

        private async void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            DataService ds = new Services.DataService();
            string firstName = InputFirstName.Text;
            string surname = InputSurname.Text;
            int age = int.Parse(InputAge.Text);

            UsuarioRequest newUser = new UsuarioRequest();
            newUser.FirstName = firstName;
            newUser.Surname = surname;
            newUser.Age = age;
            
            await ds.PostUserAsync(newUser);

            InputFirstName.Text = "";
            InputSurname.Text = "";
            InputAge.Text = "";
            items = await ds.GetUsersAsync();
            listUsers();
        }

        private void listUsers()
        {
            lv1.ItemsSource = items.Select(x => new UsuarioDisplay {DisplayName = "Nome: " + x.FirstName + " " + x.Surname, DisplayAge = "Idade: " + x.Age, Id = x.Id });
        }
        private async void ButtonPut_Clicked(object sender, EventArgs e)
        {
            DataService ds = new Services.DataService();
            string firstName = InputFirstNameUpdate.Text;
            string surname = InputSurnameUpdate.Text;
            int age = int.Parse(InputAgeUpdate.Text);

            UsuarioRequest newUser = new UsuarioRequest();
            newUser.FirstName = firstName;
            newUser.Surname = surname;
            newUser.Age = age;

            await ds.PutUserAsync(new Guid(InputIdUpdate.Text), newUser);

            InputIdUpdate.Text = "";
            InputFirstNameUpdate.Text = "";
            InputSurnameUpdate.Text = "";
            InputAgeUpdate.Text = "";
            items = await ds.GetUsersAsync();
            listUsers();
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            DataService ds = new Services.DataService();
            await ds.DeleteUsersAsync(new Guid(InputIdDelete.Text));
            AtualizaDados();
            InputIdDelete.Text = "";
        }
        private async void TextCell_Tapped(object sender, EventArgs e)
        {
            DataService ds = new Services.DataService();
            TextCell obj = sender as TextCell;
            if (obj != null)
            {
                Guid id = (Guid)obj.CommandParameter;
                InputIdDelete.Text = id.ToString();
                InputIdUpdate.Text = id.ToString();

                UsuarioResponse clickedUser = await ds.GetUserByIdAsync(id);

                InputIdDelete.Text = clickedUser.Id.ToString();
                InputIdUpdate.Text = clickedUser.Id.ToString();
                InputFirstNameUpdate.Text = clickedUser.FirstName.ToString();
                InputSurnameUpdate.Text = clickedUser.Surname.ToString();
                InputAgeUpdate.Text = clickedUser.Age.ToString();
            }
        }
    }
}
