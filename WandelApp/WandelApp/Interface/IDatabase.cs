using System;
using System.Collections.Generic;
using System.Text;

namespace WandelApp.Interface
{
    public interface IDatabase
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
