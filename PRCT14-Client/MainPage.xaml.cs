using System.Collections.ObjectModel;
using PRCT14_Client.Models;
using PRCT14_Client.Services;

namespace PRCT14_Client
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<AcademicLoadDTO> _academicLoads = new ObservableCollection<AcademicLoadDTO>();

        public MainPage()
        {
            InitializeComponent();
            lvAcademicLoads.ItemsSource = _academicLoads;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadAcademicLoads();
        }

        private void LoadAcademicLoads()
        {
            try
            {                
                var loads = APIService.Get<List<AcademicLoadDTO>>("api/AcademicLoads/Info");
                if (loads != null)
                {
                    _academicLoads.Clear();
                    foreach (var load in loads) _academicLoads.Add(load);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", $"Не удалось загрузить данные: {ex.Message}", "OK");
            }
        }

        private async void btnAddAcademicLoad_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditAcademicLoadPage());
        }

        private async void btnEditAcademicLoad_Clicked(object sender, EventArgs e)
        {
            var selected = lvAcademicLoads.SelectedItem as AcademicLoadDTO;
            if (selected == null)
            {
                await DisplayAlert("Ошибка", "Выберите запись для редактирования", "OK");
                return;
            }
            
            try
            {
                var fullLoad = APIService.Get<AcademicLoadEditDTO>($"api/AcademicLoads/{selected.AcademLoadCode}");
                await Navigation.PushAsync(new AddEditAcademicLoadPage(fullLoad));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось загрузить запись: {ex.Message}", "OK");
            }
        }

        private async void btnDeleteAcademicLoad_Clicked(object sender, EventArgs e)
        {
            var selected = lvAcademicLoads.SelectedItem as AcademicLoadDTO;
            if (selected == null)
            {
                await DisplayAlert("Ошибка", "Выберите запись для удаления", "OK");
                return;
            }

            var confirm = await DisplayAlert("Подтверждение", "Удалить запись?", "Да", "Нет");
            if (confirm)
            {
                try
                {
                    APIService.Delete(selected.AcademLoadCode, "api/AcademicLoads");
                    LoadAcademicLoads();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Не удалось удалить запись: {ex.Message}", "OK");
                }
            }
        }

        private async void btnTeachers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeachersPage());
        }
    }
}
