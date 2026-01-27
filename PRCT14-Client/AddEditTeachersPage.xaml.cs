using PRCT14_Client.Models;
using PRCT14_Client.Services;

namespace PRCT14_Client
{
    public partial class AddEditTeachersPage : ContentPage
    {        
        Teacher _teacher;

        public AddEditTeachersPage()
        {
            InitializeComponent();
            
            if (Data.Teacher == null)
            {                
                _teacher = new Teacher();
                Title = "Добавить преподавателя";
            }
            else
            {
                _teacher = Data.Teacher;
                Title = "Редактировать преподавателя";
            }

            tvTeacher.BindingContext = _teacher;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (Data.Teacher == null)
            {                
                APIService.Post(_teacher, "api/teachers");
            }
            else
            {                
                APIService.Put(_teacher, _teacher.ServiceNumber, "api/teachers");
            }

            await Navigation.PopModalAsync();
        }       
    }
}