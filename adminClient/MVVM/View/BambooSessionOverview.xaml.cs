using adminClient.MVVM.ViewModel;

namespace adminClient.MVVM.View;

public partial class BambooSessionOverview : ContentPage
{
	public BambooSessionOverview(BambooSessionOverviewViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

    }
}