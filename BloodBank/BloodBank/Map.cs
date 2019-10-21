using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsPresentation;
using GMapMarker = GMap.NET.WindowsForms.GMapMarker;

namespace BloodBank
{
    public partial class Map : Form
    {
        double lattitude, longitude;
       // string name;
        public Map(double x, double y,string n)
        {
            InitializeComponent();
            lattitude = x;
            longitude = y;
        //    name = n;
            this.Text = n;
            gMapControl1.ShowCenter = false;
            showdata();
        }
        public void showdata()
        {
            
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(lattitude, longitude);
            gMapControl1.Zoom=10;
           // gMapControl1.MapProvider = GMap
            gMapControl1.MaxZoom=100;
            gMapControl1.MinZoom=5;

            //gMapControl1.SetZoomToFitRect(50);

            PointLatLng point=new PointLatLng(lattitude,longitude);
            GMapMarker mark = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);

            GMapOverlay olay = new GMapOverlay("marks");

            olay.Markers.Add(mark);
            gMapControl1.Overlays.Add(olay);
            
        }
    }
}
