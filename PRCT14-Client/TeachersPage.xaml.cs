using PRCT14_Client.Models;
using PRCT14_Client.Services;

namespace PRCT14_Client
{
    public partial class TeachersPage : ContentPage
    {
        public TeachersPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
            lvTeachers.ItemsSource = APIService.Get<List<Teacher>>("api/teachers");
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {            
            Data.Teacher = null;
            await Navigation.PushModalAsync(new AddEditTeachersPage());
        }

        private async void btnEdit_Clicked(object sender, EventArgs e)
        {            
            var selectedTeacher = (Teacher)lvTeachers.SelectedItem;
            if (selectedTeacher != null)
            {
                Data.Teacher = selectedTeacher;
                await Navigation.PushModalAsync(new AddEditTeachersPage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Выберите преподавателя для редактирования", "OK");
            }
        }

        private void btnDelete_Clicked(object sender, EventArgs e)
        {
            var item = (Teacher)lvTeachers.SelectedItem;
            if (item != null)
            {
                APIService.Delete(item.ServiceNumber, "api/teachers");                
                lvTeachers.ItemsSource = APIService.Get<List<Teacher>>("api/teachers");
            }
        }
    }
}