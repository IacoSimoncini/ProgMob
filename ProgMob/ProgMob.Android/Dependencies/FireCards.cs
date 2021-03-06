﻿using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;
using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireCards))]
namespace ProgMob.Droid.Dependencies
{
    class FireCards : Java.Lang.Object, ViewModel.Helpers.FireCards, IOnCompleteListener
    {
        List<Card> cardList;
        string UserId;
        int value;
        string currentType;

        public FireCards()
        {
            cardList = new List<Card>();
        }

        public async Task<bool> DeleteCard(Card card , string day, string week)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(card.Ref)
                    .Collection(week)
                    .Document(day)
                    .Collection("dailyCard")
                    .Document(card.Path);
                collection.Delete();
                Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(card.Ref).Collection(week).Document(day).Update("ifSet" + card.Type, "False");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool InsertCard(Card card, string day, string week)
        {
            try
            {
                HashMap mp = new HashMap();
                mp.Put("type", card.Type);
                mp.Put("name", card.Name);
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(card.Ref)
                    .Collection(week)
                    .Document(day)
                    .Collection("dailyCard")
                    .Document();
                collection.Set(mp);
                HashMap mp2 = new HashMap();
                mp2.Put("ifSet" + card.Type, "True");
                Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(card.Ref)
                    .Collection(week)
                    .Document(day)
                    .Set(mp2);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ListCard(string Uid , string day, string week , string type)
        {
            value = 0;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                .Document(Uid)
                .Collection(week)
                .Document(day)
                .Collection("dailyCard");
            UserId = Uid;
            currentType = type;
            collection.Get().AddOnCompleteListener(this);

            for (int i = 0; i < 30; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (value != 0)
                    break;
            }

            if (value == 1) { return true; }
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
                    if (doc.Get("type").Equals(currentType)) {
                        Card card = new Card();
                        card.Name = doc.Get("name").ToString();
                        card.Type = doc.Get("type").ToString();
                        card.Path = doc.Id.ToString();
                        card.Ref = UserId;
                        cardList.Add(card);
                    }
                    
                }
                value = 1;
            }
            else
            {
                value = 2;
            }
        }

        public async Task<bool> UpdateCard(Card c, string day, string week, string newName)
        {
            try
            {
                Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                       .Document(c.Ref).Collection(week).Document(day).Collection("dailyCard").Document(c.Path)
                       .Update("name", newName);
                return true;             
            }
            catch (Exception e)
            {
                return false;
            }
        }



    }
}