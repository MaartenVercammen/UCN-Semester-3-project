using admin_client.MVVM.ViewModel;

namespace admin_client.MVVM.View;

public partial class UserView : ContentPage
{
	public UserView(UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}