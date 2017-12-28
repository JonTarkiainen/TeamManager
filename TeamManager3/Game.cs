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
    public class Game
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public Game()
        {
            HomeScore = 0;
            AwayScore = 0;
        }

        public static int LoadHomeScore()
        {
            var db = DataAccess.GetConnection();

            var games = from g in db.Table<Game>()
                        select g;

            if (games.FirstOrDefault() != null)
                return games.FirstOrDefault().HomeScore;
            else
                return 0;
        }

        public static int LoadAwayScore()
        {
            var db = DataAccess.GetConnection();

            var games = from g in db.Table<Game>()
                        select g;

            if (games.FirstOrDefault() != null)
                return games.FirstOrDefault().AwayScore;
            else
                return 0;
        }

        public static void UpdateScores(int homeScore, int awayScore)
        {
            var db = DataAccess.GetConnection();

            var game = db.Query<Game>("Select * from [Game]");

            if (game.FirstOrDefault() != null)
            {
                db.Execute("Update [Game] set [HomeScore] = ?, [AwayScore] = ? ", homeScore, awayScore);
            }
            else
            {
                db.Execute("Insert into [Game] ([HomeScore], [AwayScore]) values (?, ?)", homeScore, awayScore);
            }
        }

        public static void DeleteScores()
        {
            var db = DataAccess.GetConnection();

            db.Execute("Delete from [Game]");
        }
    }
}