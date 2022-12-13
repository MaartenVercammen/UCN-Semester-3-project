using adminClient.MVVM.View;

namespace adminClient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//Registerd route
		Routing.RegisterRoute(nameof(UserView), typeof(UserView));
		Routing.RegisterRoute(nameof(RecipeView), typeof(RecipeView));
	}
}
