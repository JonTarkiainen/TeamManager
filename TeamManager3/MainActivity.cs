using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Content;
using Android.Content.Res;

namespace TeamManager3
{
    [Activity(Label = "@string/app_name", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.Locale)]
    public class MainActivity : Activity
    {
        RosterDataAdapter listAdapter;
        ExpandableListView expListView;
        List<string> listDataHeader;
        Dictionary<string, List<Player>> listDataChild;
        int previousGroup = -1;
        List<Player> lstPitch;
        List<Player> lstBench;
        List<Player> lstRoster;

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
                    listAdapter.AddChild(groupPosition - 1, player);
                }
                else if (groupPosition == 1) //bench
                {
                    var player = listAdapter.GetChildObj(groupPosition, childPosition);

                    listAdapter.DeleteChild(groupPosition, childPosition);
                    listAdapter.AddChild(groupPosition - 1, player);
                }
                else //game
                {
                    var player = listAdapter.GetChildObj(groupPosition, childPosition);

                    listAdapter.DeleteChild(groupPosition, childPosition);
                    listAdapter.AddChild(groupPosition + 1, player);
                }
                listAdapter.NotifyDataSetChanged();
            };

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.InflateMenu(Resource.Menu.Bottom_Menu);
            toolbar.MenuItemClick += (sender, e) =>
            {
                if (e.Item.ItemId == Resource.Id.menuAddPlayer)
                {
                    var intent = new Intent(this, typeof(AddPlayerActivity));
                    StartActivity(intent);
                    Finish();
                }
                else if (e.Item.ItemId == Resource.Id.menuResetGame)
                {
                    ResetListData();
                    listAdapter.NotifyDataSetChanged();
                }
            };
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

            lstBench = new List<Player>();

            lstRoster = new List<Player>();
            lstRoster = Player.GetAllPlayers();

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

            lstRoster = Player.GetAllPlayers();

            listDataChild.Add(listDataHeader[0], lstPitch);
            listDataChild.Add(listDataHeader[1], lstBench);
            listDataChild.Add(listDataHeader[2], lstRoster);
        }
    }
}

