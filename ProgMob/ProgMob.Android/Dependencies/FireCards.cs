using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Util;
using ProgMob.Models;
using ProgMob.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireCards))]
namespace ProgMob.Droid.Dependencies
{
    class FireCards : Java.Lang.Object, ViewModel.Helpers.FireCards, IOnCompleteListener
    {
        List<Card> cardList;
        string UserId;
        
        public FireCards()
        {
            cardList = new List<Card>();
        }

        public async Task<bool> DeleteCard(Card Card)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Card.Ref)
                    .Collection("Schede")
                    .Document(Card.Path)
                    .Delete();
                cardList.Remove(Card);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public bool InsertCard(Card Card)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Card.Ref)
                    .Collection("Schede")
                    .Document(Card.Path);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IList<Card>> ListCard(string Uid)
        {
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                .Document(Uid)
                .Collection("Schede");
            UserId = Uid;
            collection.Get().AddOnCompleteListener(this);
            return cardList;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                cardList.Clear();
                var documents = (QuerySnapshot)task.Result;
                foreach (var doc in documents.Documents)
                {
                    Card card = new Card();
                    card.Path = doc.Id.ToString();
                    card.Ref = UserId;
                    cardList.Add(card);
                }
            }
        }

        public Task<bool> UpdateCard(Card Card)
        {
            throw new NotImplementedException();
        }
    }
}