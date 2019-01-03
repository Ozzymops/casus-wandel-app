using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using SQLite;
using Xamarin.Forms;

// Code taken from https://www.youtube.com/watch?v=b0JPpVgAv9w

namespace WandelApp.Models
{
    public class Database
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<User> Users { get; set; }

        public Database()
        {
            database = DependencyService.Get<Interface.IDatabase>().DbConnection();
            database.CreateTable<User>();
            this.Users = new ObservableCollection<User>(database.Table<User>());
        }

        #region User
        /// <summary>
        /// Voeg nieuwe huidige user info toe aan een table.
        /// </summary>
        public void AddUser(User user)
        {
            database.Insert(new User { Id = user.Id, Name = user.Name, Username = user.Username, Password = user.Password, PreferencesId = user.PreferencesId });
        }

        /// <summary>
        /// Haal de huidige user info op uit een table.
        /// </summary>
        /// <returns>De opgehaalde user</returns>
        public User GetCurrentUser()
        {
            lock(collisionLock)
            {
                return database.Table<User>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Delete de huidige user info PERMANENT uit een table.
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
        /// Wipe alle user info PERMANENT uit een table.
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
        /// Delete PERMANENT alle data uit alle tables.
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
