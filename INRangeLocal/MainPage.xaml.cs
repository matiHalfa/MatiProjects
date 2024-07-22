using System.Timers;

namespace INRangeLocal
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;
        private System.Timers.Timer resetTimer;

        public MainPage()
        {
            //BindingContext = new MainPageVM();
            InitializeComponent();
            LoadCheckBoxStates();
            StartResetTimer();
        }

        private void LoadCheckBoxStates()
        {
            CheckBox1.IsChecked = Preferences.Get("CheckBox1", false);
            CheckBox2.IsChecked = Preferences.Get("CheckBox2", false);
            CheckBox3.IsChecked = Preferences.Get("CheckBox3", false);
        }

        private void SaveCheckBoxStates()
        {
            Preferences.Set("CheckBox1", CheckBox1.IsChecked);
            Preferences.Set("CheckBox2", CheckBox2.IsChecked);
            Preferences.Set("CheckBox3", CheckBox3.IsChecked);
        }

        private void StartResetTimer()
        {
            DateTime now = DateTime.Now;
            DateTime next6AM = now.Date.AddHours(6);
            if (now > next6AM)
            {
                next6AM = next6AM.AddDays(1);
            }

            double initialInterval = (next6AM - now).TotalMilliseconds;

            resetTimer = new System.Timers.Timer
            {
                Interval = initialInterval,
                AutoReset = false
            };
            resetTimer.Elapsed += (sender, e) =>
            {
                ResetCheckBoxStates(sender, e);
                resetTimer.Interval = 24 * 60 * 60 * 1000; // 24 hours
                resetTimer.AutoReset = true;
                resetTimer.Start();
            };
            resetTimer.Start();
        }
        private void ResetCheckBoxStates(object sender, ElapsedEventArgs e)
        {
            Preferences.Set("CheckBox1", false);
            Preferences.Set("CheckBox2", false);
            Preferences.Set("CheckBox3", false);
            Dispatcher.Dispatch(LoadCheckBoxStates);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SaveCheckBoxStates();
        }
    }

}
