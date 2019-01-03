using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WandelApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Android_Database))]

namespace WandelApp.Droid
{
    public class Android_Database : Interface.IDatabase
    {
        public SQLite.SQLiteConnection DbConnection()
        {
            var dbName = "localDatabase.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

            return new SQLite.SQLiteConnection(path);
        }
    }
}