using adminClient.MVVM.ViewModel;

namespace adminClient.MVVM.View;

public partial class UserView : ContentPage
{
	public UserView(UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	
}