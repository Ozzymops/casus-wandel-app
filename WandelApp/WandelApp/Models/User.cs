using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WandelApp.Models
{
    internal class User : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string _name;

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public Preferences Preferences { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
