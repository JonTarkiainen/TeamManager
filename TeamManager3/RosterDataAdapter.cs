using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace TeamManager3
{
    class RosterDataAdapter : BaseExpandableListAdapter
    {
        private Activity context;
        private List<string> listDataHeader;
        private Dictionary<string, List<Player>> listDataChild;

        public RosterDataAdapter(Activity context, List<string> listDataHeader, Dictionary<string, List<Player>> listDataChild)
        {
            this.context = context;
            this.listDataHeader = listDataHeader;
            this.listDataChild = listDataChild;
        }

        public void AddChild(int groupPosition, Player player)
        {
            var children = listDataChild[listDataHeader[groupPosition]];
            children.Add(player);
        }

        public void DeleteChild(int groupPosition, int childPosition)
        {
            var children = listDataChild[listDataHeader[groupPosition]];
            children.RemoveAt(childPosition);
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            string headerTitle = (string)GetGroup(groupPosition);

            convertView = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.ListGroup, null);
            var lblListHeader = (TextView)convertView.FindViewById(Resource.Id.DataHeader);
            lblListHeader.Text = headerTitle;

            return convertView;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            string childText = (string)GetChild(groupPosition, childPosition);

            if (groupPosition == 2)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.DataListItem, null);

                ImageButton edit = (ImageButton)convertView.FindViewById(Resource.Id.buttonEdit);
                edit.Tag = "Edit";
                edit.Focusable = false;
                edit.FocusableInTouchMode = false;
                edit.Clickable = true;

                ImageButton delete = (ImageButton)convertView.FindViewById(Resource.Id.buttonDelete);
                delete.Tag = "Delete";
                delete.Focusable = false;
                delete.FocusableInTouchMode = false;
                delete.Clickable = true;

                edit.SetOnClickListener(new ButtonClickListener(this.context, GetChildObj(groupPosition, childPosition)));
                delete.SetOnClickListener(new ButtonClickListener(this.context, GetChildObj(groupPosition, childPosition)));
            }
            else
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.DataListItemNoButtons, null);
            }

            TextView txtListChild = (TextView)convertView.FindViewById(Resource.Id.playerName);
            txtListChild.Text = childText;

            return convertView;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return listDataChild[listDataHeader[groupPosition]].Count;
        }

        public override int GroupCount
        {
            get
            {
                return 3;
            }
        }

        public Player GetChildObj(int groupPosition, int childPosition)
        {
            return listDataChild[listDataHeader[groupPosition]][childPosition];
        }

        #region implemented abstract members of BaseExpandableListAdapter

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return listDataChild[listDataHeader[groupPosition]][childPosition].number + "-" + listDataChild[listDataHeader[groupPosition]][childPosition].name;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return listDataHeader[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }

        #endregion
    }
}