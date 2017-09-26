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

        public ButtonClickListener(Activity activity)
        {
            this.activity = activity;
        }

        public void OnClick(View v)
        {
            Toast.MakeText(this.activity, "button "+v.Tag, ToastLength.Short).Show();

            var intent = new Intent(v.Context, typeof(EditPlayerActivity));
            v.Context.StartActivity(intent);
        }
    }
}