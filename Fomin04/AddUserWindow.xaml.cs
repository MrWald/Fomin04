using System.Windows;
using FontAwesome.WPF;

namespace Fomin04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        private ImageAwesome _loader;
        private PersonInputView _personInputView;
        private CalculationResultView _calculationResultView;

        public MainWindow()
        {
            InitializeComponent();
            ShowInputView();
            DataContext = new MainWindowViewModel(ShowResultView, ShowInputView, ShowLoader);
        }

        private void ShowInputView()
        {
            ShowtView(ref _personInputView);
        }

        private void ShowResultView()
        {
            ShowtView(ref _calculationResultView);
        }

        private void ShowtView<TObject>(ref TObject view) where TObject:UIElement, new()
        {
            MainGrid.Children.Clear();
            if (view == null)
            {
                view = new TObject();
            }
            MainGrid.Children.Add(view);
        }

        private void ShowLoader(bool isShow)
        {
            LoaderHelper.OnRequestLoader(MainGrid, ref _loader, isShow);
        }
    }
}
