using ProgMob.Models;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private int _countSeconds = 30;
        int index = 0;
        
        public TimerPage(string Cid, string Uid)
        {
            InitializeComponent();

            CardId = Cid;
            UserId = Uid;

            Title = Cid;

            
            Startup();

            PlayPauseButton.Source = "playbutton.png";
        }

        private async void Startup()
        {
            if (await DatabaseDetailCard.ListExercise(UserId, CardId))
            {
                list = (List<Exercise>)await DatabaseDetailCard.GetExercises();
                foreach (var e in list) Console.WriteLine("e.NAME: " + e.Name);
                Console.WriteLine(index);
                ExLbl.Text = list[index].Name;
            }
            
        }

        private void PlayPauseButton_Clicked(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if(_countSeconds != 0)
                {
                    _countSeconds--;
                }
                if(_countSeconds == 0) { 
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