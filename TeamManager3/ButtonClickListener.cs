
using Android.App;
using Android.Content;
using Android.Graphics;
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
            if ((string)v.Tag == "Edit")
            {
                var intent = new Intent(v.Context, typeof(EditPlayerActivity));
                intent.PutExtra("id", player.id);
                v.Context.StartActivity(intent);
                activity.Finish();
            }
            else if ((string)v.Tag == "Delete")
            {
                Player.DeletePlayer(this.player);
                var intent = new Intent(v.Context, typeof(MainActivity));
                v.Context.StartActivity(intent);
                activity.Finish();
            }
            else if ((string)v.Tag == "Goalkeeper")
            {
                Button button = (Button)v.FindViewById(Resource.Id.buttonGoalkeeper);

                if (player.isGoalkeeper)
                {
                    button.SetTextColor(Color.ParseColor("#000000"));
                    player.isGoalkeeper = false;
                    Player.UpdatePlayer(player);
                }
                else
                {
                    button.SetTextColor(Color.ParseColor("#ff0000"));
                    player.isGoalkeeper = true;
                    Player.UpdatePlayer(player);
                }                
            }
            else if ((string)v.Tag == "Captain")
            {
                Button button = (Button)v.FindViewById(Resource.Id.buttonCaptain);
                SetCaptain(player, button);
            }
        }

        private void SetCaptain (Player captainPlayer, Button captainButton)
        {
            if (player.isCaptain)
            {
                captainButton.SetTextColor(Color.ParseColor("#000000"));
                captainPlayer.isCaptain = false;
                Player.UpdatePlayer(captainPlayer);
            }
            else
            {
                captainButton.SetTextColor(Color.ParseColor("#ff0000"));

                var currentCaptain = Player.GetCaptain();

                if (currentCaptain != null)
                {
                    currentCaptain.isCaptain = false;
                    Player.UpdatePlayer(currentCaptain);
                }

                captainPlayer.isCaptain = true;
                Player.UpdatePlayer(captainPlayer);
            }
        }
    }
}