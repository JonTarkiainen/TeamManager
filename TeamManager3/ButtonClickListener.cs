using System;
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
    class ButtonClickListener : Java.Lang.Object, View.IOnClickListener
    {
        private Activity activity;
        private Player player;

        public ButtonClickListener(Activity activity, Player player)
        {
            this.activity = activity;
            this.player = player;
        }

        public void OnClick(View v)
        {
            Toast.MakeText(this.activity, (string)v.Tag, ToastLength.Short).Show();

            if ((string)v.Tag == "Edit")
            {
                var intent = new Intent(v.Context, typeof(EditPlayerActivity));
                v.Context.StartActivity(intent);
            }
            else if ((string)v.Tag == "Delete")
            {
                Player.DeletePlayer(this.player);
            }
        }
    }
}