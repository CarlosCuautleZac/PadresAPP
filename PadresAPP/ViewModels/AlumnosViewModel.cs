using PadresAPP.Models;
using PadresAPP.Services;
using PadresAPP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PadresAPP.ViewModels
{
    public class AlumnosViewModel : INotifyPropertyChanged
    {
        #region Commandos
        public Command RefreshCommand { get; set; }
        public Command VerAccionesAlumnoCommand { get; set; }
        public Command VerDatosAlumnoCommand { get; set; }
        public Command VerCalificacionesCommand { get; set; }
        public Command VerMateriasCommand { get;set; }
        #endregion

        #region Propiedades
        public Usuario? Usuario { get; set; }
        public ObservableCollection<Alumno> Alumnos { get; set; } = new();
        public Alumno Alumno { get; set; }
        public bool IsLoading { get; set; }

        #endregion

        #region Objetos
        AlumnoService alumnoSerivce;
        AccionesView accionesView;
        DetallesAlumnoView detallesAlumnoView;
        CalificacionesView calificacionesView;
        MaestrosView maestrosView { get; set; }
        #endregion

        //Constructor
        public AlumnosViewModel()
        {
            //Instanciacion de Commandos
            RefreshCommand = new Command(Refresh);
            VerAccionesAlumnoCommand = new Command<Alumno>(VerAcciones);
            VerDatosAlumnoCommand = new Command(VerDatosAlumno);
            VerCalificacionesCommand = new Command(VerCalificaciones);
            VerMateriasCommand = new Command(VerMaterias);

            //vistas
            accionesView = new AccionesView() { BindingContext = this };
            detallesAlumnoView = new DetallesAlumnoView() { BindingContext = this };
            calificacionesView=new CalificacionesView() { BindingContext = this };
            maestrosView=new MaestrosView() { BindingContext = this };  

            //Instanciacion de objetos
            Alumno = new();        
            Usuario = App.Usuario;
            alumnoSerivce = new AlumnoService();
            alumnoSerivce.Error += AlumnoSerivce_Error;
            Refresh();
            Actualizar();

        }

        private async void VerMaterias()
        {
            try
            {
                Actualizar();
                await App.Current.MainPage.Navigation.PushAsync(maestrosView);
            }
            catch (Exception ex)
            {
                Actualizar();
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void VerCalificaciones(object obj)
        {
            try
            {
                Actualizar();
                await App.Current.MainPage.Navigation.PushAsync(calificacionesView);
            }
            catch (Exception ex)
            {
                Actualizar();
                await App.Current.MainPage.DisplayAlert("Error",ex.Message, "OK");
            }
        }

        private async void VerDatosAlumno()
        {
            try
            {
                Actualizar();
                await App.Current.MainPage.Navigation.PushAsync(detallesAlumnoView);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            
            
        }

        private async void VerAcciones(Alumno alumno)
        {
            Alumno = alumno;
            await App.Current.MainPage.Navigation.PushAsync(accionesView);
            Actualizar();
        }

        private async void Refresh()
        {
            if (ConectionAvaliable())
            {
                IsLoading = true;
                Actualizar();
                Alumnos = new(await alumnoSerivce.Get(Usuario.Id));
                IsLoading = false;
                Actualizar();
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "No hay conexion a internet.", "OK");
        }

        [Obsolete]
        private async void AlumnoSerivce_Error(string error)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert("Error", error, "OK");
            });
        }



        #region Metodos

        #endregion


        //Checar la conexion a internet
        private bool ConectionAvaliable()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
