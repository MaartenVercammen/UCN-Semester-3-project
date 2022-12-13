using adminClient.MVVM.ViewModel;

namespace adminClient.MVVM.View;

public partial class UsersView : ContentPage
{
	public UsersView(UsersViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}