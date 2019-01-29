using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

// Code adapted from https://www.youtube.com/watch?v=b0JPpVgAv9w

namespace WandelApp.Models
{
    public class Database
    {
        private Constants c = new Constants();
        private Logger l = new Logger();
    
        #region SQLite
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
            database.CreateTable<Preferences>();
            this.Users = new ObservableCollection<User>(database.Table<User>());
        }

        #region User
        /// <summary>
        /// Add a new user to the database table.
        /// </summary>
        public void AddUser(User user)
        {
            database.Insert(new User { Id = user.Id, Name = user.Name, Username = user.Username, Password = user.Password });
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

        /// <summary>
        /// Return preferences saved in database table.
        /// </summary>
        /// <returns></returns>
        public Preferences GetPreferences()
        {
            lock (collisionLock)
            {
                return database.Table<Preferences>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Saving the preferences into the SQLite database
        /// </summary>
        public void SavePreferences(Preferences preferences)
        {
            database.Insert(preferences);
        }

        public void WipePreferences()
        {
            lock (collisionLock)
            {
                database.DropTable<Preferences>();
                database.CreateTable<Preferences>();
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
        #endregion
        #region API
        /// <summary>
        /// Return a boolean to the requested method.
        /// Returned value is determined by login succes.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>login status</returns>
        public async Task<int> LogIn(string username, string password)
        {
            int status = 0;

            try
            {
                var uri = new Uri(string.Format("{0}/user/LogIn?username={1}&password={2}", Models.Constants.ApiAddress, username, password));
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(uri);

                if (response != null || response != "null")
                {
                    User user = JsonConvert.DeserializeObject<User>(response);

                    // Save to SQLite
                    WipeUsers();
                    AddUser(user);

                    status = 1;
                }
            }
            catch (Exception e)
            {
                l.WriteToLog(e.ToString());
                status = -1;
            }
            return status;
        }

        /// <summary>
        /// Return a specific Route by id to the requested method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Route</returns>
        public async Task<Route> GetRoute(int id)
        {
            try
            {
                var uri = new Uri(string.Format("{0}/route/GetRoute?id={1}", Models.Constants.ApiAddress, id));
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(uri);

                if (response != null || response != "null")
                {
                    Route route = JsonConvert.DeserializeObject<Route>(response);

                    // Save to SQLite
                    return route;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                l.WriteToLog(e.ToString());
            }
            return null;
        }

        /// <summary>
        /// Return all available Routes to the requested method.
        /// </summary>
        /// <returns>Route list</returns>
        public async Task<List<Route>> GetAllRoutes()
        {
            try
            {
                var uri = new Uri(string.Format("{0}/route/GetAllRoutes", Models.Constants.ApiAddress));
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(uri);

                if (response != null || response != "null")
                {
                    List<Route> routeList = JsonConvert.DeserializeObject<List<Route>>(response);

                    // Save to SQLite
                    return routeList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                l.WriteToLog(e.ToString());
            }
            return null;
        }

        /// <summary>
        /// Add a route to the db.
        /// </summary>
        /// <returns>Route</returns>
        public async Task<string> AddRoute(Route route)
        {
            string json = JsonConvert.SerializeObject(route);

            var uri = new Uri(string.Format("{0}/route/AddRoute?json={1}", Models.Constants.ApiAddress, json));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                l.WriteToLog(json);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            string result = "";
            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var error = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(error.GetResponseStream()))
                        {
                            string errorMsg = reader.ReadToEnd();
                            result = errorMsg;
                        }
                    }
                }
            }

            return result;
        }

        #region HELL
        /// <summary>
        /// Return all Routes within a certain difficulty range to the requested method.
        /// </summary>
        /// <returns>Route list</returns>
        public async Task<List<Route>> GetRoutesByDifficulty(int difficulty)
        {
            try
            {
                var uri = new Uri(string.Format("{0}/route/GetRoutesByDifficulty?difficulty={0}", Models.Constants.ApiAddress, difficulty));
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(uri);

                if (response != null || response != "null")
                {
                    List<Route> routeList = JsonConvert.DeserializeObject<List<Route>>(response);

                    // Save to SQLite
                    return routeList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                l.WriteToLog(e.ToString());
            }
            return null;
        }
        #endregion
        #endregion
    }
}
