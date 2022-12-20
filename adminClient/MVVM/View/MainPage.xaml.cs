using adminClient.MVVM.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace adminClient.MVVM.View;

public partial class MainPage : ContentPage
{

	public MainPage(MainPageView modelView)
	{
		InitializeComponent();

        BindingContext = modelView;
	}
}

