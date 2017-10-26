using System.Collections.Generic;
using SQLite;

namespace TeamManager3
{
    public class Player
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public int groupPosition { get; set; }
        public bool isGoalkeeper { get; set; }
        public bool isCaptain { get; set; }

        public Player(string name, string number)
        {
            this.name = name;
            this.number = number;
            this.groupPosition = 2;
            this.isGoalkeeper = false;
            this.isCaptain = false;
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

        public static Player GetCaptain()
        {
            var db = DataAccess.GetConnection();

            var captains = from p in db.Table<Player>()
                   where p.isCaptain == true
                   select p;

            return captains.FirstOrDefault();
        }

        public static List<Player> GetChildren(int groupPosition)
        {
            var db = DataAccess.GetConnection();

            if (groupPosition == 2)
            {
                return db.Query<Player>("Select * from [Player] where ([groupPosition] = ? or [groupPosition] is null)", groupPosition);
            }
            else
            {
                return db.Query<Player>("Select * from [Player] where [groupPosition] = ?", groupPosition);
            }
        }
    }
}