using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireCards
    {
        bool InsertCard(Card Card);
        Task<bool> DeleteCard(Card Card);
        Task<bool> ListCard(string Uid);
        Task<IList<Card>> GetCard();

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
        public static Task<bool> ListCard(string Uid)
        {
            return firestoreCards.ListCard(Uid);
        }
        public static Task<IList<Card>> GetCard()
        {
            return firestoreCards.GetCard();
        }
    }
}
