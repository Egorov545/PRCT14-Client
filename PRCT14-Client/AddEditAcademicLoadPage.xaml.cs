using Newtonsoft.Json;
using PRCT14_Client.Models;
using PRCT14_Client.Services;

namespace PRCT14_Client
{
    public partial class AddEditAcademicLoadPage : ContentPage
    {
        private List<Teacher> _teachers;
        private List<Discipline> _disciplines;
        private AcademicLoad _academicLoad;

        public AddEditAcademicLoadPage()
        {
            InitializeComponent();            
            LoadData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
            InitializeForm();
        }

        private void LoadData()
        {
            try
            {                
                _teachers = APIService.Get<List<Teacher>>("api/teachers");
                _disciplines = APIService.Get<List<Discipline>>("api/disciplines");
                
                pickerTeacher.ItemsSource = _teachers;
                pickerDiscipline.ItemsSource = _disciplines;
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", $"Не удалось загрузить данные: {ex.Message}", "OK");
            }
        }

        private void InitializeForm()
        {            
            _academicLoad = Data.AcademicLoad;

            if (_academicLoad != null)
            {                
                Title = "Редактирование учебной нагрузки";
                
                if (_teachers != null && _academicLoad.TeacherCode > 0)
                {
                    var selectedTeacher = _teachers.FirstOrDefault(t => t.ServiceNumber == _academicLoad.TeacherCode);
                    if (selectedTeacher != null)
                    {
                        pickerTeacher.SelectedItem = selectedTeacher;
                    }
                }

                if (_disciplines != null && _academicLoad.DisciplineCode > 0)
                {
                    var selectedDiscipline = _disciplines.FirstOrDefault(d => d.DisciplineCode == _academicLoad.DisciplineCode);
                    if (selectedDiscipline != null)
                    {
                        pickerDiscipline.SelectedItem = selectedDiscipline;
                    }
                }
                
                tvAcademicLoad.BindingContext = _academicLoad;
            }
            else
            {                
                Title = "Добавление учебной нагрузки";
                _academicLoad = new AcademicLoad();
                tvAcademicLoad.BindingContext = _academicLoad;
            }
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            
        }

    }
}