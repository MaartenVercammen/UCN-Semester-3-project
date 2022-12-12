using admin_client.MVVM.ViewModel;

namespace admin_client.MVVM.View;

public partial class UsersView : ContentPage
{
	public UsersView(UsersViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}