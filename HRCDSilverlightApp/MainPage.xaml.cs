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


           // InformationLayer layer = (sender as MapShapeReader).Layer;

            // extract the seat colorization information from the data attributes
            //foreach (MapShape shape in layer.Items)
            //{
            //   // byte red = byte.Parse(shape.ExtendedData.GetValue("RGB0").ToString(), CultureInfo.InvariantCulture);
            //   // byte green = byte.Parse(shape.ExtendedData.GetValue("RGB1").ToString(), CultureInfo.InvariantCulture);
            //   // byte blue = byte.Parse(shape.ExtendedData.GetValue("RGB2").ToString(), CultureInfo.InvariantCulture);

            //   // shape.Fill = new SolidColorBrush(Color.FromArgb(255, red, green, blue));
            //    shape.MouseLeftButtonDown += new MouseButtonEventHandler(shape_MouseLeftButtonDown);
            //}


            foreach (var poly in eventArgs.Items)
            {
                if (poly is MapShape)
                {
                    double myHectares = Convert.ToDouble((poly as MapShape).ExtendedData.GetValue("Hectares"));
                    String myName = Convert.ToString((poly as MapShape).ExtendedData.GetValue("WRIA_NM"));
                    (poly as MapShape).MouseLeftButtonDown += new MouseButtonEventHandler(shape_MouseLeftButtonDown);
                    (poly as MapShape).MouseEnter += new MouseEventHandler(MainPage_MouseEnter);
                    ShapeData temp = new ShapeData() { Hectares = myHectares, WriaName = myName };
                    shapeData.Add(temp);
                    
                }
            }
        }

        void MainPage_MouseEnter(object sender, MouseEventArgs e)
        {
            MapShape shape = sender as MapShape;
            string sector = shape.ExtendedData.GetValue("WRIA_NM").ToString();

            selName.Text = sector;
        }

        void shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MapShape shape = sender as MapShape;
            string sector = shape.ExtendedData.GetValue("Hectares").ToString();

            selHectares.Text = sector;

           // this.StartTransitionAnimation();
           // (this.Resources["DataContext"] as ExampleViewModel).UpdateViewModel(sector);
        }


        void myReader_ReadCompleted(object sender, Telerik.Windows.Controls.Map.ReadShapesCompletedEventArgs eventArgs)
        {
            
        }
    }
}
