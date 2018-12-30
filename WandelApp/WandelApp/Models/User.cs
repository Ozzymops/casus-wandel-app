using System;
using System.Collections.Generic;
using System.Text;

namespace WandelApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Models.Preferences Preferences { get; set; }
    }
}
