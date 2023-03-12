using PadresAPP.ViewModels;

namespace PadresAPP.Views;

public partial class ListaHijosView : ContentPage
{
	public ListaHijosView()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var viewmodel = (AlumnosViewModel)this.BindingContext;

        var alumno = ((Border)sender).BindingContext;

        viewmodel.VerAccionesAlumnoCommand.Execute(alumno);
    }
}