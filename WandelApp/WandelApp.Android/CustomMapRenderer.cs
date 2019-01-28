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
        List<CustomPin> customPins;
        private GoogleMap _map;

        public CustomMapRenderer(Context context) : base(context)
        {

        }

        public void OnMapReady(GoogleMap map)
        {
            _map = map;

            if (_map != null)
            {
                _map.MapClick += googleMap_MapClick;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
                Control.GetMapAsync(this);

                if (_map != null)
                {
                    _map.MapClick -= googleMap_MapClick;
                }
            }
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((CustomMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);

            // Determine image
            if (pin.Label.Contains("Start"))
            {
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StartPin));
            }

            if (pin.Label.Contains("Eind"))
            {
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.EndPin));

            }

            if (pin.Label.Contains("Step"))
            {
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StepPin));

            }

            if (pin.Label.Contains("POI"))
            {
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.POIPin));

            }

            return marker;
        }

    }
}