﻿using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireCards
    {
        bool InsertCard(Card Card, string day);
        Task<bool> DeleteCard(Card Card , string day);
        Task<bool> ListCard(string Uid, string day, string type);
        Task<IList<Card>> GetCard();

    }
    class DatabaseCards
    {
        private static FireCards firestoreCards = DependencyService.Get<FireCards>();

        public static bool InsertCard(Card Card , string day)
        {
            return firestoreCards.InsertCard(Card , day);
        }
        public static Task<bool> DeleteCard(Card Card , string day)
        {
            return firestoreCards.DeleteCard(Card , day);
        }
        public static Task<bool> ListCard(string Uid, string day, string type)
        {
            return firestoreCards.ListCard(Uid , day , type);
        }
        public static Task<IList<Card>> GetCard()
        {
            return firestoreCards.GetCard();
        }

        
    }
}
