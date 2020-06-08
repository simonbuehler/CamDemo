using CamDemo.ViewModel;
using Neutronium.BuildingBlocks.SetUp;
using System;
using System.Windows;

namespace CamDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SetUpViewModel SetUp => App.SetUp;

        public MainWindow()
        {
            Initialized += MainWindow_Initialized;
            InitializeComponent();
        }

        private void MainWindow_Initialized(object sender, EventArgs e)
        {
          // DataContext = new HelloViewModel();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ViewModelLocator.Cleanup();
            this.WcBrowser.Dispose();
        }
    }
}
