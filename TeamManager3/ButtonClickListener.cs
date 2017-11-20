
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
                intent.PutExtra("id", player.Id);
                v.Context.StartActivity(intent);
                activity.Finish();
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
                RefreshView(v);
            }
        }

        private void SetGoalkeeper(Player goalkeeperPlayer, Button goalkeeperButton)
        {
            if (goalkeeperPlayer.IsGoalkeeper)
            {
                goalkeeperPlayer.IsGoalkeeper = false;
                Player.UpdatePlayer(goalkeeperPlayer);
            }
            else
            {
                var currentGoalkeeper = Player.GetGoalkeeper();

                if (currentGoalkeeper != null)
                {
                    currentGoalkeeper.IsGoalkeeper = false;
                    Player.UpdatePlayer(currentGoalkeeper);
                }

                goalkeeperPlayer.IsGoalkeeper = true;
                goalkeeperPlayer.GroupPosition = 0;
                Player.UpdatePlayer(goalkeeperPlayer);
            }
        }

        private void SetCaptain (Player captainPlayer, Button captainButton)
        {
            if (captainPlayer.IsCaptain)
            {
                captainPlayer.IsCaptain = false;
                Player.UpdatePlayer(captainPlayer);
            }
            else
            {
                var currentCaptain = Player.GetCaptain();

                if (currentCaptain != null)
                {
                    currentCaptain.IsCaptain = false;
                    Player.UpdatePlayer(currentCaptain);
                }

                captainPlayer.IsCaptain = true;
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