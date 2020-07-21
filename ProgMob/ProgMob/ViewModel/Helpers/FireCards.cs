using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireCards
    {
        bool InsertCard(Card Card);
        Task<bool> DeleteCard(Card Card);
        Task<bool> UpdateCard(Card Card);
        Task<IList<Card>> ListCard(string Uid);


    }
    class DatabaseCards
    {
        private static FireCards firestoreCards = DependencyService.Get<FireCards>();

        public static bool InsertCard(Card Card)
        {
            return firestoreCards.InsertCard(Card);
        }
        public static Task<bool> DeleteCard(Card Card)
        {
            return firestoreCards.DeleteCard(Card);
        }
        public static Task<bool> UpdateCard(Card Card)
        {
            return firestoreCards.UpdateCard(Card);
        }
        public static Task<IList<Card>> ListCard(string Uid)
        {
            return firestoreCards.ListCard(Uid);
        }
    }
}
