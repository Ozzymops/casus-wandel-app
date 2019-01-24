using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WandelApp.Models
{
    public class BindableMap : Map
    {

        private Position _myPosition = new Position(-37.8141, 144.9633);
        public Position MyPosition { get { return _myPosition; } set { _myPosition = value; OnPropertyChanged(); } }

        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection { get { return _pinCollection; } set { _pinCollection = value; OnPropertyChanged(); } }

        public static readonly BindableProperty MapPinsProperty = BindableProperty.Create(
                     nameof(Pin),
                     typeof(ObservableCollection<Pin>),
                     typeof(Map),
                     new ObservableCollection<Pin>(),
                     propertyChanged: (b, o, n) =>
                     {
                         var bindable = (BindableMap)b;
                         bindable.Pins.Clear();

                         var collection = (ObservableCollection<Pin>)n;
                         foreach (var item in collection)
                             bindable.Pins.Add(item);
                         collection.CollectionChanged += (sender, e) =>
                         {
                             Device.BeginInvokeOnMainThread(() =>
                             {
                                 switch (e.Action)
                                 {
                                     case NotifyCollectionChangedAction.Add:
                                     case NotifyCollectionChangedAction.Replace:
                                     case NotifyCollectionChangedAction.Remove:
                                         if (e.OldItems != null)
                                             foreach (var item in e.OldItems)
                                                 bindable.Pins.Remove((Pin)item);
                                         if (e.NewItems != null)
                                             foreach (var item in e.NewItems)
                                                 bindable.Pins.Add((Pin)item);
                                         break;
                                     case NotifyCollectionChangedAction.Reset:
                                         bindable.Pins.Clear();
                                         break;
                                 }
                             });
                         };
                     });
        public IList<Pin> MapPins { get; set; }

        public static readonly BindableProperty MapPositionProperty = BindableProperty.Create(
                 nameof(MapPosition),
                 typeof(Position),
                 typeof(BindableMap),
                 new Position(0, 0),
                 propertyChanged: (b, o, n) =>
                 {
                     ((BindableMap)b).MoveToRegion(MapSpan.FromCenterAndRadius(
                          (Position)n,
                          Distance.FromMiles(1)));
                 });

        public Position MapPosition { get; set; }
    }
}
