using PadresAPP.Models;
using PadresAPP.Views;

namespace PadresAPP;

public partial class App : Application
{
	public static Usuario Usuario { get; set; } = new Usuario();

	public App()
	{
		InitializeComponent();

		MainPage = new LoginView();
	}
}
