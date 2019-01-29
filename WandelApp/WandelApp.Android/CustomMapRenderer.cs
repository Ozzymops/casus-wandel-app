using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using WandelApp.Droid;
using WandelApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace WandelApp.Droid
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        Models.Logger l = new Models.Logger();

        List<CustomPin> customPins;
        private GoogleMap _map;
        private CustomMap formsMap;
        private Polyline finalLine;
        private bool _mapDrawn;

        public CustomMapRenderer(Context context) : base(context)
        {
            l.WriteToLog("Constructor!");
        }

        protected override void OnMapReady(GoogleMap map)
        {

            //if (_mapDrawn)
            //{
              //  return;
            //}
            
            l.WriteToLog("OnMapReady!");

            base.OnMapReady(map);

            _map = map;

            if (_map != null)
            {
                _map.MapClick += googleMap_MapClick;
            }

                l.WriteToLog("CustomPins!");

                foreach (var customPin in formsMap.CustomPins)
                {
                    var markerWithIcon = new MarkerOptions();

                    markerWithIcon.SetPosition(new LatLng(customPin.Position.Latitude, customPin.Position.Longitude));
                    markerWithIcon.SetTitle(customPin.Name);
                    markerWithIcon.SetSnippet(customPin.Description);

                    if (customPin.Label.Contains("Start"))
                    {
                        markerWithIcon.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StartPin));
                    }
                    else if (customPin.Label.Contains("Eind"))
                    {
                        markerWithIcon.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.EndPin));
                    }
                    else if (customPin.Label.Contains("POI"))
                    {
                        markerWithIcon.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.POIPin));
                    }
                    else if (customPin.Label.Contains("Step"))
                    {
                        markerWithIcon.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StepPin));
                    }

                    var m = base.NativeMap.AddMarker(markerWithIcon);
                }

            _mapDrawn = true;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            l.WriteToLog("OnElementChanged!");

            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (_map != null)
                {
                    _map.MapClick -= googleMap_MapClick;
                }

                formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;                
                Control.GetMapAsync(this);
            }
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            l.WriteToLog("MapClick!");

            ((CustomMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }

        //protected override MarkerOptions CreateMarker(Pin pin)
        //{
        //    l.WriteToLog("CreateMarker - pin " + pin.Label);
        //    var marker = new MarkerOptions();
        //    marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
        //    marker.SetTitle(pin.Label);
        //    marker.SetSnippet(pin.Address);

        //    // Determine image
        //    if (pin.Label.Contains("Start"))
        //    {
        //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StartPin));
        //    }
        //    else if (pin.Label.Contains("Eind"))
        //    {
        //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.EndPin));
        //    }
        //    else if (pin.Label.Contains("Step"))
        //    {
        //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StepPin));
        //    }
        //    else if (pin.Label.Contains("POI"))
        //    {
        //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.POIPin));
        //    }
        //    else
        //    {
        //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Placeholder));
        //    }

        //    return marker;
        //}

    }
}