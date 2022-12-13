using adminClient.MVVM.ViewModel;

namespace adminClient.MVVM.View;

public partial class BambooSessionView : ContentPage
{
	public BambooSessionView(BambooSessionViewModel viewModel )
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}