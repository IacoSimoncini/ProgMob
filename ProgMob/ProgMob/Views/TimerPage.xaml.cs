using ProgMob.Models;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerPage : ContentPage
    {
        List<Exercise> list;
        readonly string CardId;
        readonly string UserId;
        private int _countSeconds = 60;
        int index = 0;
        private double _ProgressValue;
        public double ProgressValue
        {
            get
            {
                return _ProgressValue;
            }
            set
            {
                _ProgressValue = value;
                OnPropertyChanged();
            }
        }
        private double _Minimum;
        public double Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                _Minimum = value;
                OnPropertyChanged();
            }
        }
        private double _Maximum;
        public double Maximum
        {
            get
            {
                return _ProgressValue;
            }
            set
            {
                _ProgressValue = value;
                OnPropertyChanged();
            }
        }
        private Timer time = new Timer();
        private bool timerRunning;


        public TimerPage(string Cid, string Uid)
        {
            InitializeComponent();
            BindingContext = this;
            
            CardId = Cid;
            UserId = Uid;

            Title = Cid;

            
            Startup();

            PlayPauseButton.Source = "playbutton.png";
        }

        private async void Startup()
        {
            Minimum = 0;
            Maximum = 60;
            ProgressValue = 60;
            timerRunning = true;
            if (await DatabaseDetailCard.ListExercise(UserId, CardId, "1"))
            {
                list = (List<Exercise>)await DatabaseDetailCard.GetExercises();
                foreach (var e in list) Console.WriteLine("e.NAME: " + e.Name);
                Console.WriteLine(index);
                ExLbl.Text = list[index].Name;
            }
            
        }

        private void PlayPauseButton_Clicked(object sender, EventArgs e)
        {
            time.Start();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (ProgressValue > Minimum)
                {
                    ProgressValue--;
                    return true;
                }
                else
                {
                    if (ProgressValue == Minimum)
                    {
                        time.Stop();
                        timerRunning = false;
                        return false;
                    }
                }
                if (_countSeconds != 0)
                {
                    _countSeconds--;
                }
                if (_countSeconds == 0) { 
                    if (index != list.Count - 1) index++;
                    else 
                    {
                        App.Current.MainPage.Navigation.PopAsync(); 
                        index = 0; 
                    }
                    ExLbl.Text = list[index].Name;
                }
                Console.WriteLine("Time: " + _countSeconds.ToString());
                TimerLbl.Text = "Time: " + _countSeconds.ToString();
                return Convert.ToBoolean(_countSeconds);
            });
        }
    }
}