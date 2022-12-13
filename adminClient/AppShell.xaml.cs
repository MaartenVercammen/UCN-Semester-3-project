using adminClient.MVVM.View;

namespace adminClient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//Registerd route
		Routing.RegisterRoute(nameof(UserView), typeof(UserView));
		Routing.RegisterRoute(nameof(UsersView), typeof(UsersView));

		Routing.RegisterRoute(nameof(BambooSessionOverview), typeof(BambooSessionOverview));
		Routing.RegisterRoute(nameof(BambooSessionView), typeof(BambooSessionView));
		
	}
}
