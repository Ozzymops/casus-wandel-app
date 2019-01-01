using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace WandelApp.Tables
{
    class UserTable
    {
        [PrimaryKey]public int Id { get; set; }
        [MaxLength(250)]public string Name { get; set; }
        [MaxLength(250)] public string Username { get; set; }
        [MaxLength(250)] public string Password { get; set; }
        public int Preferences { get; set; }
    }
}
