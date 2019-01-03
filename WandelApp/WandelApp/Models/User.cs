using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using SQLite;

namespace WandelApp.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, NotNull] public int Id { get; set; }
        [NotNull]public string Name { get; set; }
        [NotNull] public string Username { get; set; }
        [NotNull] public string Password { get; set; }
        public int PreferencesId { get; set; }
    }
}
