using CarListApp.Maui.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CarListApp.Maui.Views;
using CarListApp.Maui.Services;

namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        const string editButtonText = "Update Car";
        const string createButtonText = "Add Car";
        private readonly CarApiService carApiService;

        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel(CarApiService carApiService)
        {
            Title = "Car List";
            AddEditButtonText = createButtonText;
            this.carApiService = carApiService;
            //GetCarList();
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        int carId;

        [RelayCommand]
        async Task GetCarList()
        {
            if(IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();
                var cars = new List<Car>();
                //var cars = App.CarDatabaseService.GetCars();
                cars = await carApiService.GetCars();

                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unable to get cars: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrive list of cars.", "Ok");
                throw;
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetCarDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }
        [RelayCommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
                return;
            }
            var car = new Car
            {
                Make = Make,
                Model = Model,
                Vin = Vin
            };
            if (CarId != 0)
            {
                car.Id = CarId;
                App.CarDatabaseService.UpdateCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarDatabaseService.StatusMessage, "Ok");
            }
            else
            {
                App.CarDatabaseService.AddCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarDatabaseService.StatusMessage, "Ok");
            }
            await GetCarList();
            await ClearForm();
        }
        [RelayCommand]
        async Task AddCar()
        {
            if(string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
                return;
            }
            var car = new Car
            {
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            App.CarDatabaseService.AddCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarDatabaseService.StatusMessage, "Ok");
            await GetCarList();
        }
        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }
            var result = App.CarDatabaseService.DeleteCar(id);
            if (result == 0) await Shell.Current.DisplayAlert("Failed", "Please insert vaild data", "Ok");
            else
            {
                await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "Ok");
                await GetCarList();
            }
        }
        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            CarId = id;
            var car = App.CarDatabaseService.GetCar(id);
            Make = car.Make;
            Model = car.Model;
            Vin = car.Vin;
        }
        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = 0;
            Make = string.Empty;
            Model = string.Empty;
            Vin = string.Empty;
        }
    }
}
