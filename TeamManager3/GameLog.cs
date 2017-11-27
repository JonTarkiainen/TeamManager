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
    class GameLog
    {
        public enum LogEventEnums
        {
            GameStart = 0,
            GameEnd = 1,
            AddGoal = 2,
            AddGoalScorer = 3,
            RemoveGoal = 4,
            RemoveGoalScorer = 5,
            PauseGame = 6,
            ResumeGame = 7,
            FromRosterToBench = 8,
            FromBenchToPitch = 9,
            FromPitchToBench = 10,
            FromBenchToRoster = 11,
            SetCaptain = 12,
            RemoveCaptain = 13,
            SetGoalkeeper = 14,
            RemoveGoalkeeper = 15,
            ResetMatchClock = 16,
            ResetScore = 17,
            ResetGame = 18
        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public Guid GameId { get; set; }
        public DateTime LogTime { get; set; }
        public int LogEvent { get; set; }
        public string LogEventValue { get; set; }

        public GameLog(Guid gameId)
        {
            GameId = gameId;
        }

        public static int AddGameLogEvent(GameLog game)
        {
            var db = DataAccess.GetConnection();

            return db.Insert(game);
        }
    }
}