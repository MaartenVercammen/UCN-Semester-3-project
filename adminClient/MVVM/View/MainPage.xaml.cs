using admin_client.MVVM.ViewModel;

namespace admin_client.MVVM.View;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(MainPageView modelView)
	{
		InitializeComponent();

		BindingContext = modelView;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

