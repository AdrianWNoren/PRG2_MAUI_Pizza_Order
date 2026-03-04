namespace PizzaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

#if WINDOWS || MACCATALYST
            window.Width = 350;
            window.Height = 600;
#endif

            return window;
        }
    }
}