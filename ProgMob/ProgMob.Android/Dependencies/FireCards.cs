﻿using System;
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
        int value;

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
                    .Document(Card.Path);
                collection.Delete();
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
                HashMap mp = new HashMap();
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Card.Ref)
                    .Collection("Schede")
                    .Document(Card.Path);
                collection.Set(mp);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ListCard(string Uid)
        {
            value = 0;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                .Document(Uid)
                .Collection("Schede");
            UserId = Uid;
            collection.Get().AddOnCompleteListener(this);

            for(int i = 0; i < 30; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (value != 0)
                    break;
            }

            if(value == 1) { return true; }
            else { return false; }
        }

        public async Task<IList<Card>> GetCard()
        {
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
                value = 1;
            }
            else
            {
                Console.WriteLine(task.Exception);
                value = 2;
            }
        }

        
    }
}