using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using System.Collections.Generic;
using Android.Content.PM;

namespace TeamManager3
{
    [Activity(Label = "Ordnung muss sein!", ScreenOrientation = ScreenOrientation.Portrait)]
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
            FnGetListData();

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

        }

        void FnGetListData()
        {
            listDataHeader = new List<string>();
            listDataChild = new Dictionary<string, List<Player>>();

            // Adding child data
            listDataHeader.Add("On pitch");
            listDataHeader.Add("On bench");
            listDataHeader.Add("Roster");

            // Adding child data
            lstPitch = new List<Player>();

            var lstBench = new List<Player>();

            var lstRoster = new List<Player>();
            lstRoster.Add(new Player("Algot", "82"));
            lstRoster.Add(new Player("Elias", "33"));
            lstRoster.Add(new Player("Casper", "55"));
            lstRoster.Add(new Player("Niklas", "55"));
            lstRoster.Add(new Player("Alvi", "55"));
            lstRoster.Add(new Player("Max", "55"));
            lstRoster.Add(new Player("Oscar", "55"));
            lstRoster.Add(new Player("Daniel", "55"));
            lstRoster.Add(new Player("Eetu", "55"));
            lstRoster.Add(new Player("Alvar L", "55"));
            lstRoster.Add(new Player("Alvar M", "55"));
            lstRoster.Add(new Player("Joel", "55"));
            lstRoster.Add(new Player("Wilmer", "55"));
            lstRoster.Add(new Player("Aaron", "55"));
            lstRoster.Add(new Player("Oiva", "55"));
            lstRoster.Add(new Player("Hendri", "55"));

            // Header, Child data
            listDataChild.Add(listDataHeader[0], lstPitch);
            listDataChild.Add(listDataHeader[1], lstBench);
            listDataChild.Add(listDataHeader[2], lstRoster);
        }
    }
}

