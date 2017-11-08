
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;

namespace TeamManager3
{
    [Activity(Label = "Edit player details", LaunchMode = LaunchMode.SingleInstance, NoHistory = true, ConfigurationChanges = ConfigChanges.Locale)]
    public class EditPlayerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EditPlayer);

            EditText name = FindViewById<EditText>(Resource.Id.playerName);
            EditText number = FindViewById<EditText>(Resource.Id.playerNumber);

            int id = Intent.GetIntExtra("id", 0);

            var player = Player.GetPlayer(id);
            name.Text = player.Name;
            number.Text = player.Number;

            Button doneButton = FindViewById<Button>(Resource.Id.buttonDone);

            doneButton.Click += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(number.Text))
                {
                    player.Name = name.Text;
                    player.Number = number.Text;

                    var result = Player.UpdatePlayer(player);
                }

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            };
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}