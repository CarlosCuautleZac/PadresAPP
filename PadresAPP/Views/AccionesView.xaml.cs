using PadresAPP.ViewModels;

namespace PadresAPP.Views;

public partial class AccionesView : ContentPage
{
	public AccionesView()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var viewmodel = (AlumnosViewModel)this.BindingContext;

        var alumno = ((Grid)sender).BindingContext;

        viewmodel.VerDatosAlumnoCommand.Execute(null);
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        var viewmodel = (AlumnosViewModel)this.BindingContext;

        var alumno = ((Border)sender).BindingContext;

        viewmodel.VerCalificacionesCommand.Execute(null);
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        var viewmodel = (AlumnosViewModel)this.BindingContext;

        var alumno = ((Border)sender).BindingContext;

        viewmodel.VerMateriasCommand.Execute(null);
    }
}