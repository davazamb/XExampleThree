using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XExampleThree.Classes;

namespace XExampleThree.Pages
{
    public partial class EditPage : ContentPage
    {
        private Employee employee;

        public EditPage(Employee employee)
        {
            InitializeComponent();

            Padding = Device.OnPlatform(
                new Thickness(10, 20, 10, 10),
                new Thickness(10),
                new Thickness(10));

            this.employee = employee;

            firstNameEntry.Text = employee.FirstName;
            lastNameEntry.Text = employee.LastName;
            contractDateDatePicker.Date = employee.ContractDate;
            salaryEntry.Text = employee.Salary.ToString();
            activeSwitch.IsToggled = employee.Active;

            updateButton.Clicked += UpdateButton_Clicked;
            deleteButton.Clicked += DeleteButton_Clicked;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var rta = await DisplayAlert("Confirm", "Are you sure to delete the record?", "Yes", "No");
            if (!rta)
            {
                return;
            }

            using (var data = new DataAccess())
            {
                data.Delete(employee);
            }

            await DisplayAlert("Message", "The record was deleted", "Acept");
            await Navigation.PopAsync();
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(firstNameEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a first name", "Acept");
                firstNameEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(lastNameEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a last name", "Acept");
                lastNameEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(salaryEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a salary", "Acept");
                salaryEntry.Focus();
                return;
            }

            employee.FirstName = firstNameEntry.Text;
            employee.LastName = lastNameEntry.Text;
            employee.Salary = decimal.Parse(salaryEntry.Text);
            employee.ContractDate = contractDateDatePicker.Date;
            employee.Active = activeSwitch.IsToggled;

            using (var data = new DataAccess())
            {
                data.Update(employee);
            }

            await DisplayAlert("Message", "The record was updated", "Acept");
            await Navigation.PopAsync();
        }

    }
}
