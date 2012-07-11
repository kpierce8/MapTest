using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Map;
using System.Diagnostics;

namespace HRCDSilverlightApp
{
    public partial class MainPage : UserControl
    {

        List<ShapeData> shapeData = new List<ShapeData>();

        public MainPage()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            //StreamResourceInfo shapeResourceInfo = Application.GetResourceStream(new Uri("DataSources/BiggestPolygonsWGS84.shp", UriKind.RelativeOrAbsolute));
            //StreamResourceInfo dbfResourceInfo = Application.GetResourceStream(new Uri("DataSources/BiggestPolygonsWGS84.dbf", UriKind.RelativeOrAbsolute));
            //List<FrameworkElement> shapes = ShapeFileReader.Read(shapeResourceInfo.Stream, dbfResourceInfo.Stream);
            //foreach (var shape in shapes)
            //{
            //    this.pugetSound.Items.Add(shape);
            //}



            myReader.Source = new Uri("DataSources/BiggestPolygonsWGS84.shp", UriKind.Relative);
            myReader.DataSource = new Uri("DataSources/BiggestPolygonsWGS84.dbf", UriKind.Relative);

            myReader.PreviewReadCompleted += new Telerik.Windows.Controls.Map.PreviewReadShapesCompletedEventHandler(myReader_PreviewReadCompleted);
            myReader.ReadCompleted += new Telerik.Windows.Controls.Map.ReadShapesCompletedEventHandler(myReader_ReadCompleted);
            //radMap1.MapMouseClick += new MouseRoutedEventHandler(radMap1_MapMouseClick);
        }

       

        void myReader_PreviewReadCompleted(object sender, Telerik.Windows.Controls.Map.PreviewReadShapesCompletedEventArgs eventArgs)
        {
            foreach (var poly in eventArgs.Items)
            {
                if (poly is MapPolygon)
                {
                    double myHectares = Convert.ToDouble((poly as MapPolygon).ExtendedData.GetValue("Hectares"));
                    String myName = Convert.ToString((poly as MapPolygon).ExtendedData.GetValue("WRIA_NM"));

                    ShapeData temp = new ShapeData() { Hectares = myHectares, WriaName = myName };
                    shapeData.Add(temp);

                }
            }
        }


        void myReader_ReadCompleted(object sender, Telerik.Windows.Controls.Map.ReadShapesCompletedEventArgs eventArgs)
        {
            
        }
    }
}
