using admin_client.MVVM.View;
using admin_client.MVVM.ViewModel;
using admin_client.Services;

namespace adminClient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<UserView>();
		builder.Services.AddSingleton<UsersView>();
		builder.Services.AddSingleton<UserService>();
		builder.Services.AddSingleton<UserViewModel>();
		builder.Services.AddSingleton<UsersViewModel>();

		return builder.Build();
	}
}
