using PRCT14_Client.Models;
using PRCT14_Client.Services;

namespace PRCT14_Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //LoadAcademicLoads();
            var teachers = APIService.Get<List<Teacher>>("api/teachers");            
            var disciplines = APIService.Get<List<Discipline>>("api/disciplines");            
            var academicLoads = APIService.Get<List<AcademicLoad>>("api/academicloads");
           
            foreach (var load in academicLoads)
            {
                load.TeacherCodeNavigation = teachers.FirstOrDefault(t => t.ServiceNumber == load.TeacherCode);
                load.DisciplineCodeNavigation = disciplines.FirstOrDefault(d => d.DisciplineCode == load.DisciplineCode);
            }

            lvAcademicLoads.ItemsSource = academicLoads;
        }

        private void LoadAcademicLoads()
        {
            try
            {
                lvAcademicLoads.ItemsSource = APIService.Get<List<AcademicLoad>>("api/academicloads?includeTeachers=true&includeDisciplines=true");
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", $"Не удалось загрузить данные: {ex.Message}", "OK");
            }
        }

        private async void btnTeachers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeachersPage());
        }

        private async void btnAddAcademicLoad_Clicked(object sender, EventArgs e)
        {
            Data.AcademicLoad = null;
            var page = new AddEditAcademicLoadPage();
            page.Disappearing += (s, args) => LoadAcademicLoads();
            await Navigation.PushModalAsync(page);
        }

        private async void btnEditAcademicLoad_Clicked(object sender, EventArgs e)
        {
            var selectedLoad = (AcademicLoad)lvAcademicLoads.SelectedItem;
            if (selectedLoad != null)
            {
                Data.AcademicLoad = selectedLoad;
                var page = new AddEditAcademicLoadPage();
                page.Disappearing += (s, args) => LoadAcademicLoads();
                await Navigation.PushModalAsync(page);
            }
            else
            {
                await DisplayAlert("Ошибка", "Выберите нагрузку для редактирования", "OK");
            }
        }

        private void btnDeleteAcademicLoad_Clicked(object sender, EventArgs e)
        {
            var item = (AcademicLoad)lvAcademicLoads.SelectedItem;
            if (item != null)
            {
                APIService.Delete(item.AcademLoadCode, "api/academicloads");
                LoadAcademicLoads();
            }
        }
    }
}