﻿using adminClient.MVVM.View;
using adminClient.MVVM.ViewModel;
using adminClient.Services;
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
				fonts.AddFont("coco-gothic.ttf", "CocoGothic");
			});

		//Services
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<BambooSessionService>();
        builder.Services.AddSingleton<RecipeService>();

		//Recipe
		builder.Services.AddTransient<RecipeView>();
		builder.Services.AddTransient<RecipeViewModel>();

		//Recipe Overview
		builder.Services.AddSingleton<RecipesViewModel>();
		builder.Services.AddSingleton<RecipesView>();

		//User
        builder.Services.AddTransient<UserView>();
        builder.Services.AddTransient<UserViewModel>();

		//User Overview
        builder.Services.AddSingleton<UsersViewModel>();
        builder.Services.AddSingleton<UsersView>();

		//MainPage
		builder.Services.AddSingleton<MainPageView>();
		builder.Services.AddSingleton<MainPage>();

        return builder.Build();
	}
}
