using MAUI.ViewsModels;

namespace MAUI.Views;

public partial class VideoGamePage : ContentPage
{
	public VideoGamePage(VideoGameVModel viewModel)
	{
		InitializeComponent();
        BindingContext= viewModel;
    }
}