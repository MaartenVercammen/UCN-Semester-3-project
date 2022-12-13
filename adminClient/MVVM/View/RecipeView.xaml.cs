using adminClient.MVVM.ViewModel;

namespace adminClient.MVVM.View;

public partial class RecipeView : ContentPage
{
	public RecipeView(RecipeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	
}