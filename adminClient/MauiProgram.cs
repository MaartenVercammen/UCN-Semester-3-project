using admin_client.MVVM.View;
using admin_client.MVVM.ViewModel;
using admin_client.Services;
using adminClient.Services;

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

        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<BambooSessionService>();
        builder.Services.AddSingleton<RecipeService>();

        builder.Services.AddTransient<UserView>();
        builder.Services.AddSingleton<UserViewModel>();

        builder.Services.AddSingleton<UsersViewModel>();
        builder.Services.AddSingleton<UsersView>();

        return builder.Build();
	}
}
