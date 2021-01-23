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
        int index;
        int pausa;
        int MaxIndex;
        bool value = false;
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
        string Diff;

        public TimerPage(string Cid, string Uid, string name, string diff)
        {
            InitializeComponent();
            BindingContext = this;
            Minimum = 0;
            index = 0;

            Diff = diff;
            CardId = Cid;
            UserId = Uid;

            Title = name;

            CheckDiff(Diff);

            Startup();

            PlayPauseButton.Source = "playbutton.png";

        }

        private async void Startup()
        {
            if (await DatabaseDetailCard.ListExercise(UserId, 
                    CardId,
                    Application.Current.Properties["selectedDay"].ToString(),
                    Application.Current.Properties["selectedWeek"].ToString()))
            {
                list = (List<Exercise>)await DatabaseDetailCard.GetExercises();
                if (!list.Any())
                {
                    await DisplayAlert("Empty card", "The card looks empty, please choose another one", "Ok");
                    Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    MaxIndex = list.Count();

                    ExLbl.Text = list[index].Name;
                }
                
            }
            
        }

        private void CheckDiff(string diff)
        {
            if (diff.Equals("Hard"))
            {
                pausa = 5;
                ProgressValue = 15;
                Maximum = 15;
                Application.Current.Properties["value"] = "15";
                Application.Current.SavePropertiesAsync();
            }
            else
            {
                if (diff.Equals("Medium"))
                {
                    pausa = 10;
                    ProgressValue = 10;
                    Maximum = 10;
                    Application.Current.Properties["value"] = "10";
                    Application.Current.SavePropertiesAsync();
                }
                else
                {
                    if (diff.Equals("Easy"))
                    {
                        pausa = 15;
                        ProgressValue = 5;
                        Maximum = 5;
                        Application.Current.Properties["value"] = "5";
                        Application.Current.SavePropertiesAsync();
                    }
                }
            }
        }

        private void PlayPauseButton_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn)
            {
                if (value)
                {
                    ProgressValue = pausa;
                    Maximum = ProgressValue;
                }
                time.Start();
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    btn.IsEnabled = false;
                    timerRunning = true;
                    if (ProgressValue > Minimum)
                    {
                        ProgressValue--;
                        return true;
                    }
                    else if (ProgressValue == Minimum)
                    {
                        time.Stop();
                        timerRunning = false;

                        ProgressValue = Maximum;
                        if (value)
                        {
                            CheckDiff(Diff);
                            index += 1;
                            value = false;
                            if (index == MaxIndex)
                            {
                                ProgressValue = Maximum;
                                DisplayAlert("Finished workout", "You will be redirected to the previous page", "Ok");
                                Application.Current.MainPage.Navigation.PopAsync();
                            }
                            else
                            {
                                ExLbl.Text = list[index].Name;
                            }
                        }
                        else
                        {
                            ProgressValue = pausa;
                            Maximum = ProgressValue;
                            ExLbl.Text = "Break";
                            value = true;
                        }
                        btn.IsEnabled = true;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                });
            }
            
        }
    }
}