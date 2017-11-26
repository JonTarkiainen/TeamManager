
using Android.App;
using Android.Content;
using Android.Content.Res;
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
                var builder = new AlertDialog.Builder(v.Context);

                builder.SetMessage(v.Resources.GetString(Resource.String.delete_player));
                builder.SetNegativeButton(v.Resources.GetString(Resource.String.no), (s, e) => { });
                builder.SetPositiveButton(v.Resources.GetString(Resource.String.yes), (s, e) => {
                    Player.DeletePlayer(player);
                    RefreshView(v);
                });
                
                builder.Create().Show();
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
            if (goalkeeperPlayer.isGoalkeeper)
            {
                goalkeeperPlayer.isGoalkeeper = false;
                Player.UpdatePlayer(goalkeeperPlayer);
            }
            else
            {
                var currentGoalkeeper = Player.GetGoalkeeper();

                if (currentGoalkeeper != null)
                {
                    currentGoalkeeper.isGoalkeeper = false;
                    Player.UpdatePlayer(currentGoalkeeper);
                }

                goalkeeperPlayer.isGoalkeeper = true;
                goalkeeperPlayer.groupPosition = 0;
                Player.UpdatePlayer(goalkeeperPlayer);
            }
        }

        private void SetCaptain (Player captainPlayer, Button captainButton)
        {
            if (captainPlayer.isCaptain)
            {
                captainPlayer.isCaptain = false;
                Player.UpdatePlayer(captainPlayer);
            }
            else
            {
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

        private void RefreshView(View view)
        {
            var intent = new Intent(view.Context, typeof(MainActivity));
            view.Context.StartActivity(intent);
            activity.Finish();
        }
    }
}