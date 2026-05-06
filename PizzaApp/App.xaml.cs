using PizzaApp.Services;

namespace PizzaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await AppData.InitAsync();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());
#if WINDOWS || MACCATALYST
            window.Width = 390;
            window.Height = 844;
#endif
            return window;
        }
    }
}