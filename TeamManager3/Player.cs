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

namespace TeamManager3
{
    public class Player
    {
        public string name { get; set; }
        public string number { get; set; }

        public Player(string name, string number)
        {
            this.name = name;
            this.number = number;
        }
    }
}