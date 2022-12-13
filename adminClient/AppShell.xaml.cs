using admin_client.MVVM.View;

namespace adminClient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//Registerd route
		Routing.RegisterRoute(nameof(UserView), typeof(UserView));
	}
}
