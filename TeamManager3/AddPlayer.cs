﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TeamManager3
{
    [Activity(Label = "AddPlayer")]
    public class AddPlayer : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddPlayer);

            EditText name = FindViewById<EditText>(Resource.Id.playerName);
            EditText number = FindViewById<EditText>(Resource.Id.playerNumber);

            Button addPlayerButton = FindViewById<Button>(Resource.Id.buttonAddPlayer);

            addPlayerButton.Click += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(number.Text))
                {
                    Player newPlayer = new Player(name.Text, number.Text);
                    var result = Player.InsertPlayer(newPlayer);
                }

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }
    }
}