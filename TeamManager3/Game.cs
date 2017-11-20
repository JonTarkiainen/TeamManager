using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace TeamManager3
{
    class Game
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public Game(string name)
        {
            StartTime = DateTime.Now;
            HomeScore = 0;
            AwayScore = 0;
            Name = name;
        }

        public Game EndGame (Game game)
        {
            game.EndTime = DateTime.Now;

            return game;
        }
    }
}