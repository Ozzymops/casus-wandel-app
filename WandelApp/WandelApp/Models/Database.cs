using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using SQLite;
using Xamarin.Forms;

// Code adapted from https://www.youtube.com/watch?v=b0JPpVgAv9w

namespace WandelApp.Models
{
    public class Database
    {
        /// <summary>
        /// Database settings:
        /// database - The connection(string) to the internal SQLite database.
        /// collisionLock - The collision lock exists to prevent overlapping or duplicate entries whilst awaiting other async tasks.
        /// Users - The observable collection is a list of users.
        /// </summary>
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<User> Users { get; set; }

        /// <summary>
        /// Constructor:
        /// A connection is made with the SQLite database.
        /// The database creates a table. If one already exists, it gets skipped.
        /// The observable collection list of users is filled with the table from the database.
        /// </summary>
        public Database()
        {
            database = DependencyService.Get<Interface.IDatabase>().DbConnection();
            database.CreateTable<User>();
            this.Users = new ObservableCollection<User>(database.Table<User>());
        }

        #region User
        /// <summary>
        /// Add a new user to the database table.
        /// </summary>
        public void AddUser(User user)
        {
            database.Insert(new User { Id = user.Id, Name = user.Name, Username = user.Username, Password = user.Password, PreferencesId = user.PreferencesId });
        }

        /// <summary>
        /// Return a single user from the database table.
        /// </summary>
        /// <returns>a user</returns>
        public User GetCurrentUser()
        {
            lock(collisionLock)
            {
                return database.Table<User>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Delete the current user permanently from the database table.
        /// </summary>
        /// <param name="user"></param>
        public void DeleteCurrentUser(User user)
        {
            int id = user.Id;
            if (id != 0)
            {
                lock(collisionLock)
                {
                    database.Delete<User>(id);
                }
            }
        }

        /// <summary>
        /// Completely wipe the user database table, permanently, until remade.
        /// </summary>
        public void WipeUsers()
        {
            lock (collisionLock)
            {
                database.DropTable<User>();
                database.CreateTable<User>();
            }
        }
        #endregion

        #region Danger-Zone!
        /// <summary>
        /// Completely wipe all database tables, permanently, until remade.
        /// </summary>
        public void WipeDatabase()
        {
            lock(collisionLock)
            {
                database.DropTable<User>();
                database.CreateTable<User>();
            }
        }
        #endregion
    }
}
