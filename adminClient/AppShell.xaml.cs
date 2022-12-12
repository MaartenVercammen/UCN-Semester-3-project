using admin_client.MVVM.View;

namespace adminClient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(UserView), typeof(UserView));
	}
}
