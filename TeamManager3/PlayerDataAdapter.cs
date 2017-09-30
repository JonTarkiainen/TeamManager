using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace TeamManager3
{
    public class PlayerDataAdapter : BaseAdapter<Player>
    {
        readonly Activity context;
        public List<Player> playerList { get; set; }

        public PlayerDataAdapter(Activity context, List<Player> playerList) : base()
        {
            this.context = context;
            this.playerList = playerList;
        }

        public override int Count
        {
            get { return playerList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View newView = convertView; // re-use an existing view, if one is available

            if (newView == null) // otherwise create a new one
                newView = context.LayoutInflater.Inflate(Resource.Layout.DataListItem, null);

            newView.FindViewById<TextView>(Resource.Id.playerName).Text = playerList[position].name;

            return newView;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Player this[int index]
        {
            get
            {
                return playerList[index];
            }
        }
    }
}

