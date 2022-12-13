using adminClient.MVVM.ViewModel;

namespace adminClient.MVVM.View;

public partial class RecipesView : ContentPage
{
	public RecipesView(RecipesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}