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

        public static int InsertPlayer(Player player)
        {
            var db = DataAccess.GetConnection();

            return db.Insert(player);
        }

        public static int DeletePlayer(Player player)
        {
            var db = DataAccess.GetConnection();

            return db.Delete(player);
        }

        public static int UpdatePlayer(Player player)
        {
            var db = DataAccess.GetConnection();

            return db.Update(player);
        }

        public static Player GetPlayer(int id)
        {
            var db = DataAccess.GetConnection();

            return db.Get<Player>(id);
        }

        public static List<Player> GetAllPlayers()
        {
            var db = DataAccess.GetConnection();

            return db.Query<Player>("Select * From [Player]");
        }
    }
}