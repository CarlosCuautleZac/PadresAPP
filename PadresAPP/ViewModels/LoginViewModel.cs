using Newtonsoft.Json;
using PadresAPP.Models;
using PadresAPP.Services;
using PadresAPP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadresAPP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Commandos
        public Command IniciarSesionCommand { get; set; }
        #endregion

        #region Propiedades
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        Usuario? Usuario { get; set; }
        public bool IsLoading { get; set; }
        #endregion

        #region Objetos
        UsuarioService usuarioService;

        #endregion

        //Constructor
        public LoginViewModel()
        {
            //Instanciacion de Commandos
            IniciarSesionCommand = new Command(IniciarSesion);

            //Instanciacion de objetos
            Usuario = new();
            usuarioService = new UsuarioService();
            usuarioService.Error += UsuarioService_Error;
        }

        [Obsolete]
        private async void UsuarioService_Error(string error)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert("Error", error, "OK");
            });
        }



        #region Metodos
        private async void IniciarSesion()
        {
            try
            {


                if (ConectionAvaliable())
                {
                    if (!IsLoading)
                    {
                        IsLoading = true;
                        Actualizar();
                        Usuario = await usuarioService.GetUsuario(NombreUsuario, Password);

                        if (Usuario != null)
                        {
                            App.Usuario = Usuario;
                            App.Current.MainPage = new NavigationPage(new ListaHijosView());
                        }
                    }
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error", "No hay conexion a internet.", "OK");

                IsLoading = false; 
                Actualizar();


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
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
