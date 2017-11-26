using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Content;
using Android.Content.Res;
using System.Timers;

namespace TeamManager3
{
    [Activity(Label = "@string/app_name", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.Locale)]
    public class MainActivity : Activity
    {
        RosterDataAdapter listAdapter;
        ExpandableListView expListView;
        List<string> listDataHeader;
        Dictionary<string, List<Player>> listDataChild;
        TextView homeScore, awayScore, matchTime;
        ImageButton homeScoreMinus, homeScorePlus, awayScoreMinus, awayScorePlus;
        Timer timer;
        List<Player> lstPitch;
        List<Player> lstBench;
        List<Player> lstRoster;
        int min = 0, sec = 0, homeScoreVal = 0, awayScoreVal = 0;
        bool timerIsRunning = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            expListView = FindViewById<ExpandableListView>(Resource.Id.RosterListview);

            DataAccess.Initialize();
            GetListData();

            listAdapter = new RosterDataAdapter(this, listDataHeader, listDataChild);
            expListView.SetAdapter(listAdapter);

            expListView.ExpandGroup(0);
            expListView.ExpandGroup(1);
            expListView.ExpandGroup(2);

            expListView.ChildClick += delegate (object sender, ExpandableListView.ChildClickEventArgs e)
            {
                var groupPosition = e.GroupPosition;
                var childPosition = e.ChildPosition;

                if (groupPosition == 2) //roster
                {
                    var player = listAdapter.GetChildObj(groupPosition, childPosition);

                    listAdapter.DeleteChild(groupPosition, childPosition);
                    player = listAdapter.AddChild(groupPosition - 1, player);

                    Player.UpdatePlayer(player);
                }
                else if (groupPosition == 1) //bench
                {
                    var player = listAdapter.GetChildObj(groupPosition, childPosition);

                    listAdapter.DeleteChild(groupPosition, childPosition);
                    player = listAdapter.AddChild(groupPosition - 1, player);

                    Player.UpdatePlayer(player);
                }
                else //game
                {
                    var player = listAdapter.GetChildObj(groupPosition, childPosition);

                    if (!player.isGoalkeeper)
                    {
                        listAdapter.DeleteChild(groupPosition, childPosition);
                        player = listAdapter.AddChild(groupPosition + 1, player);

                        Player.UpdatePlayer(player);
                    }
                }
                listAdapter.NotifyDataSetChanged();
            };

            expListView.ItemLongClick += delegate (object sender, AdapterView.ItemLongClickEventArgs e)
            {
                var listPosition = expListView.GetExpandableListPosition(e.Position);
                var groupPosition = ExpandableListView.GetPackedPositionGroup(listPosition);
                var childPosition = ExpandableListView.GetPackedPositionChild(listPosition);

                if (groupPosition == 1 || groupPosition == 0)
                {
                    var player = listAdapter.GetChildObj(groupPosition, childPosition);

                    listAdapter.DeleteChild(groupPosition, childPosition);
                    player = listAdapter.AddChild(2, player);

                    Player.UpdatePlayer(player);
                }
                listAdapter.NotifyDataSetChanged();
            };

            homeScoreMinus = FindViewById<ImageButton>(Resource.Id.home_score_minus);
            homeScore = FindViewById<TextView>(Resource.Id.home_score);
            homeScorePlus = FindViewById<ImageButton>(Resource.Id.home_score_plus);
            matchTime = FindViewById<TextView>(Resource.Id.match_time);
            awayScoreMinus = FindViewById<ImageButton>(Resource.Id.away_score_minus);
            awayScore = FindViewById<TextView>(Resource.Id.away_score);
            awayScorePlus = FindViewById<ImageButton>(Resource.Id.away_score_plus);

            if (savedInstanceState != null)
            {
                homeScoreVal = savedInstanceState.GetInt("home_score");
                awayScoreVal = savedInstanceState.GetInt("away_score");

                homeScore.Text = homeScoreVal.ToString();
                awayScore.Text = awayScoreVal.ToString();
            }

            homeScoreMinus.Click += (sender, e) => {
                if (homeScoreVal > 0)
                {
                    homeScoreVal--;
                    homeScore.Text = homeScoreVal.ToString();
                }
            };

            homeScorePlus.Click += (sender, e) =>
            {
                homeScoreVal++;
                homeScore.Text = homeScoreVal.ToString();
            };

            awayScoreMinus.Click += (sender, e) =>
            {
                if (awayScoreVal > 0)
                {
                    awayScoreVal--;
                    awayScore.Text = awayScoreVal.ToString();
                }
            };

            awayScorePlus.Click += (sender, e) =>
            {
                awayScoreVal++;
                awayScore.Text = awayScoreVal.ToString();
            };

            var bottomMenu = FindViewById<Toolbar>(Resource.Id.toolbar_bottom);
            bottomMenu.InflateMenu(Resource.Menu.Bottom_Menu);
            bottomMenu.MenuItemClick += (sender, e) =>
            {
                if (e.Item.ItemId == Resource.Id.menuAddPlayer)
                {
                    var intent = new Intent(this, typeof(AddPlayerActivity));
                    StartActivity(intent);
                    Finish();
                }
                else if (e.Item.ItemId == Resource.Id.menuResetGame)
                {
                    var builder = new AlertDialog.Builder(this);

                    builder.SetMessage(Resources.GetString(Resource.String.reset_game));
                    builder.SetNegativeButton(Resources.GetString(Resource.String.no), (s, ee) => { });
                    builder.SetPositiveButton(Resources.GetString(Resource.String.yes), (s, ee) =>
                    {
                        StopMatchClock();
                        ResetMatchClock();
                        ResetScores();
                        ResetListData();
                        listAdapter.NotifyDataSetChanged();
                    });

                    builder.Create().Show();
                }
                else if (e.Item.ItemId == Resource.Id.menuStartMatchClock)
                {
                    StartMatchClock();
                }
                else if (e.Item.ItemId == Resource.Id.menuStopMatchClock)
                {
                    StopMatchClock();
                }
                else if (e.Item.ItemId == Resource.Id.menuResetMatchClock)
                {
                    var builder = new AlertDialog.Builder(this);

                    builder.SetMessage(Resources.GetString(Resource.String.reset_match_clock));
                    builder.SetNegativeButton(Resources.GetString(Resource.String.no), (s, ee) => { });
                    builder.SetPositiveButton(Resources.GetString(Resource.String.yes), (s, ee) =>
                    {
                        ResetMatchClock();
                    });

                    builder.Create().Show();
                }
                else if (e.Item.ItemId == Resource.Id.menuResetScores)
                {
                    var builder = new AlertDialog.Builder(this);

                    builder.SetMessage(Resources.GetString(Resource.String.reset_scores));
                    builder.SetNegativeButton(Resources.GetString(Resource.String.no), (s, ee) => { });
                    builder.SetPositiveButton(Resources.GetString(Resource.String.yes), (s, ee) =>
                    {
                        ResetScores();
                    });

                    builder.Create().Show();
                }
            };
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("home_score", homeScoreVal);
            outState.PutInt("away_score", awayScoreVal);

            base.OnSaveInstanceState(outState);
        }

        private void ResetMatchClock()
        {
            if (!timerIsRunning)
            {
                sec = 0;
                min = 0;
                RunOnUiThread(() => { matchTime.Text = $"{min.ToString().PadLeft(2, '0')}:{sec.ToString().PadLeft(2, '0')}"; });
            }
        }

        private void StopMatchClock()
        {
            if (timerIsRunning)
            {
                timer.Dispose();
                timer = null;
                timerIsRunning = false;
            }
        }

        private void StartMatchClock()
        {
            if (!timerIsRunning)
            {
                timer = new Timer();
                timer.Interval = 1000;
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
                timerIsRunning = true;
            }
        }

        private void ResetScores()
        {
            homeScoreVal = 0;
            awayScoreVal = 0;
            homeScore.Text = homeScoreVal.ToString();
            awayScore.Text = awayScoreVal.ToString();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            sec++;
            if (sec == 60)
            {
                min++;
                sec = 0;
            }

            RunOnUiThread(() => { matchTime.Text = $"{min.ToString().PadLeft(2, '0')}:{sec.ToString().PadLeft(2, '0')}";});
        }

        public void GetListData()
        {
            listDataHeader = new List<string>();
            listDataChild = new Dictionary<string, List<Player>>();

            // Adding child data
            listDataHeader.Add(Resources.GetText(Resource.String.on_pitch));
            listDataHeader.Add(Resources.GetText(Resource.String.on_bench));
            listDataHeader.Add(Resources.GetText(Resource.String.roster));

            // Adding child data
            lstPitch = new List<Player>();
            lstPitch = Player.GetChildren(0);

            lstBench = new List<Player>();
            lstBench = Player.GetChildren(1);

            lstRoster = new List<Player>();
            lstRoster = Player.GetChildren(2);
            //lstRoster = Player.GetAllPlayers();

            // Header, Child data
            listDataChild.Add(listDataHeader[0], lstPitch);
            listDataChild.Add(listDataHeader[1], lstBench);
            listDataChild.Add(listDataHeader[2], lstRoster);
        }

        public void ResetListData()
        {
            lstPitch.Clear();
            lstBench.Clear();
            lstRoster.Clear();
            listDataChild.Clear();

            var lstTemp = Player.GetAllPlayers();

            foreach (Player player in lstTemp)
            {
                player.groupPosition = 2;
                lstRoster.Add(player);
                Player.UpdatePlayer(player);
            }

            listDataChild.Add(listDataHeader[0], lstPitch);
            listDataChild.Add(listDataHeader[1], lstBench);
            listDataChild.Add(listDataHeader[2], lstRoster);
        }
    }
}

