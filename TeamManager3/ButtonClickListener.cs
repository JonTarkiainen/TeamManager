
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
                RefreshView(v);
            }
            else if ((string)v.Tag == "Delete")
            {
                Player.DeletePlayer(this.player);
                RefreshView(v);
            }
            else if ((string)v.Tag == "Goalkeeper")
            {
                Button button = (Button)v.FindViewById(Resource.Id.buttonGoalkeeper);
                SetGoalkeeper(player, button);
                RefreshView(v);
            }
            else if ((string)v.Tag == "Captain")
            {
                Button button = (Button)v.FindViewById(Resource.Id.buttonCaptain);
                SetCaptain(player, button);
            }
        }

        private void SetGoalkeeper(Player goalkeeperPlayer, Button goalkeeperButton)
        {
            if (player.isGoalkeeper)
            {
                goalkeeperPlayer.isGoalkeeper = false;
                Player.UpdatePlayer(goalkeeperPlayer);
            }
            else
            {
                goalkeeperPlayer.isGoalkeeper = true;
                goalkeeperPlayer.groupPosition = 0;
                Player.UpdatePlayer(goalkeeperPlayer);

                var currentGoalkeeper = Player.GetGoalkeeper();
                currentGoalkeeper.isGoalkeeper = false;
                Player.UpdatePlayer(currentGoalkeeper);
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
                captainPlayer.isCaptain = true;
                Player.UpdatePlayer(captainPlayer);
            }
        }

        private void RefreshView(View view)
        {
            var intent = new Intent(view.Context, typeof(MainActivity));
            view.Context.StartActivity(intent);
            activity.Finish();
        }
    }
}