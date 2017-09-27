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
    [Activity(Label = "Edit player details")]
    public class EditPlayerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EditPlayer);

            Button doneButton = FindViewById<Button>(Resource.Id.buttonDone);

            doneButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }
    }
}