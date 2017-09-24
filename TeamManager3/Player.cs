using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;

namespace TeamManager3
{
    public class Player
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }
        public string name { get; set; }
        public string number { get; set; }

        public Player(string name, string number)
        {
            this.name = name;
            this.number = number;
        }

        public Player() { }

        public static List<Player> GetAllPlayers()
        {
            var playerList = new List<Player>();

            var db = DataAccess.GetConnection();
            var table = db.Table<Player>();

            foreach(var s in table)
            {
                playerList.Add(s);
            }

            return playerList;
        }
    }
}