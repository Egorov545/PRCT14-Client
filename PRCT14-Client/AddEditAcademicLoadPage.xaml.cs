using System.Collections.ObjectModel;
using PRCT14_Client.Models;
using PRCT14_Client.Services;

namespace PRCT14_Client
{
    public partial class AddEditAcademicLoadPage : ContentPage
    {
        private AcademicLoadEditDTO _academicLoad;
        private ObservableCollection<Teacher> _teachers;
        private ObservableCollection<Discipline> _disciplines;

        public AddEditAcademicLoadPage(AcademicLoadEditDTO existingLoad = null)
        {
            InitializeComponent();

            _teachers = new ObservableCollection<Teacher>();
            _disciplines = new ObservableCollection<Discipline>();

            pickerTeacher.ItemsSource = _teachers;
            pickerDiscipline.ItemsSource = _disciplines;

            if (existingLoad != null)
            {
                _academicLoad = existingLoad;
                Title = "Редактирование нагрузки";
            }
            else
            {
                _academicLoad = new AcademicLoadEditDTO();
                Title = "Добавление нагрузки";
            }
            
            BindingContext = _academicLoad;

            LoadTeachers();
            LoadDisciplines();
        }

        private async void LoadTeachers()
        {
            try
            {
                var teachers = APIService.Get<List<Teacher>>("api/Teachers");
                if (teachers != null)
                {
                    _teachers.Clear();
                    foreach (var teacher in teachers)
                        _teachers.Add(teacher);

                    if (_academicLoad.TeacherCode > 0)
                    {
                        var teacherToSelect = _teachers.FirstOrDefault(t => t.ServiceNumber == _academicLoad.TeacherCode);
                        if (teacherToSelect != null)
                        {
                            pickerTeacher.SelectedItem = teacherToSelect;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось загрузить преподавателей: {ex.Message}", "OK");
            }
        }

        private async void LoadDisciplines()
        {
            try
            {
                var disciplines = APIService.Get<List<Discipline>>("api/Disciplines");
                if (disciplines != null)
                {
                    _disciplines.Clear();
                    foreach (var discipline in disciplines)
                        _disciplines.Add(discipline);

                    if (_academicLoad.DisciplineCode > 0)
                    {
                        var disciplineToSelect = _disciplines.FirstOrDefault(d => d.DisciplineCode == _academicLoad.DisciplineCode);
                        if (disciplineToSelect != null)
                        {
                            pickerDiscipline.SelectedItem = disciplineToSelect;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось загрузить дисциплины: {ex.Message}", "OK");
            }
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {            
            if (pickerTeacher.SelectedItem == null)
            {
                await DisplayAlert("Ошибка", "Выберите преподавателя", "OK");
                return;
            }

            if (pickerDiscipline.SelectedItem == null)
            {
                await DisplayAlert("Ошибка", "Выберите дисциплину", "OK");
                return;
            }
            
            _academicLoad.TeacherCode = ((Teacher)pickerTeacher.SelectedItem).ServiceNumber;
            _academicLoad.DisciplineCode = ((Discipline)pickerDiscipline.SelectedItem).DisciplineCode;
            
            if (_academicLoad.Group <= 0)
            {
                await DisplayAlert("Ошибка", "Введите корректный номер группы", "OK");
                return;
            }

            if (_academicLoad.Semester <= 0)
            {
                await DisplayAlert("Ошибка", "Введите корректный номер семестра", "OK");
                return;
            }

            if (_academicLoad.NumberOfHours <= 0)
            {
                await DisplayAlert("Ошибка", "Введите корректное количество часов", "OK");
                return;
            }

            try
            {
                if (_academicLoad.AcademLoadCode == 0)
                {
                    APIService.Post(_academicLoad, "api/AcademicLoads");
                }
                else
                {
                    APIService.Put(_academicLoad, _academicLoad.AcademLoadCode, "api/AcademicLoads");
                }

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось сохранить: {ex.Message}", "OK");
            }
        }
    }
}
