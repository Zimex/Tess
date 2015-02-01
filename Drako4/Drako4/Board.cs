using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Drako3
{
    /// <summary>
    /// Klasa hexagonalnej planszy do gry
    /// </summary>
    [DataContract]
    [KnownType(typeof(Hex))]
    [KnownType(typeof(Figure))]
   public class Board
    {
        private Point center;
        private Size screenSize;
        private  Double boardSize;
        private List<Point> corners;
        private List<Hex> hexs;
        private int margin = 12;
        private double scale;
        private double hexHeight;
        private double hexWidth;
        private  Polygon activePlayer;
        private  Fraction activeFraction;
        //[DataMember(IsRequired = false)]
        private  Image libraryImage = new Image();
       // [DataMember(IsRequired = false)]
        private  Image dragonLibrary = new Image();
        //[DataMember(IsRequired = false)]
        private  Image dwarfLibrary = new Image();


        public Board()
        {

        }
        public  Board(List<Figure> figures)
        {
            var content = Application.Current.Host.Content;
            scale = (double)content.ScaleFactor / 100;
            activeFraction = Fraction.Dragon;
            activePlayer = new Polygon();
            BitmapImage src = new BitmapImage();
            src.UriSource = new Uri("Images/Dwarf.jpg", UriKind.Relative);
            dwarfLibrary.Source = src;
            BitmapImage src2 = new BitmapImage();
            src.UriSource = new Uri("Images/Dragon.jpg", UriKind.Relative);
            dragonLibrary.Source = src2;
            screenSize=new Size((int)Math.Ceiling(content.ActualWidth * scale),(int)Math.Ceiling(content.ActualHeight * scale));
            center = new Point(screenSize.Height/2,  screenSize.Width / 2);
            boardSize = screenSize.Width - margin;
            hexHeight = (boardSize / 5)/(Math.Sqrt(3)/2);
            Hex.hexHeight = hexHeight;
            hexWidth = Math.Sqrt(3) / 2 * hexHeight;
            Hex.hexWidth = hexWidth;
            hexs = new List<Hex>();
            corners = new List<Point>();
            activePlayer.Points.Add(new Point(5, 5));
            activePlayer.Points.Add(new Point(25, 5));
            activePlayer.Points.Add(new Point(25, 25));
            activePlayer.Points.Add(new Point(5, 25));
            activePlayer.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 0, 0));
            activePlayer.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 255, 0, 0));
                
            for (int j = 0; j < 3;j++ )
            {
                hexs.Add(new Hex(new Point(((center.X - hexWidth) + j * hexWidth), (center.Y - 1.5 * hexHeight)), new Point(j, -2)));
                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((center.X - hexWidth)+j*hexWidth) + hexHeight/2  * Math.Cos(angle));
                    double y = ((center.Y-1.5*hexHeight) + hexHeight / 2 * Math.Sin(angle));
                    hexs[hexs.Count-1].Corners.Add(new Point(x, y));
                }
            }
                       
            for (int j = 0; j < 4; j++)
            {
                hexs.Add(new Hex(new Point(((center.X - hexWidth*1.5) + j * hexWidth), (center.Y - 0.75 * hexHeight))));
                hexs[hexs.Count - 1].AxialCoordinates = new Point(j-1, -1);
                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((center.X-hexWidth*1.5) + j * hexWidth) + hexHeight / 2 * Math.Cos(angle));
                    double y = ((center.Y - 0.75 * hexHeight) + hexHeight / 2 * Math.Sin(angle));
                    hexs[hexs.Count - 1].Corners.Add(new Point(x, y));
                }
            }

            for (int j = 0; j < 5; j++)
            {
                hexs.Add(new Hex(new Point(((center.X - hexWidth * 2) + j * hexWidth), (center.Y ))));
                hexs[hexs.Count - 1].AxialCoordinates = new Point(j-2, -0);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((center.X - hexWidth * 2) + j * hexWidth) + hexHeight / 2 * Math.Cos(angle));
                    double y = ((center.Y  ) + hexHeight / 2 * Math.Sin(angle));
                    hexs[hexs.Count - 1].Corners.Add(new Point(x, y));

                }
            }

            for (int j = 0; j < 4; j++)
            {
                hexs.Add(new Hex(new Point(((center.X - hexWidth * 1.5) + j * hexWidth), (center.Y+0.75 * hexHeight))));
                hexs[hexs.Count - 1].AxialCoordinates = new Point(j-2, 1);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((center.X - hexWidth * 1.5) + j * hexWidth) + hexHeight / 2 * Math.Cos(angle));
                    double y = ((center.Y + 0.75 * hexHeight) + hexHeight / 2 * Math.Sin(angle));
                    hexs[hexs.Count - 1].Corners.Add(new Point(x, y));

                }
            }
            
            for (int j = 0; j < 3; j++)
            {
                hexs.Add(new Hex(new Point(((center.X - hexWidth ) + j * hexWidth), (center.Y + 1.5 * hexHeight))));
                hexs[hexs.Count - 1].AxialCoordinates = new Point(j-2, 2);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((center.X - hexWidth) + j * hexWidth) + hexHeight / 2 * Math.Cos(angle));
                    double y = ((center.Y + 1.5 * hexHeight) + hexHeight / 2 * Math.Sin(angle));
                    hexs[hexs.Count - 1].Corners.Add(new Point(x, y));

                }
            }
            foreach(Figure f in figures)
            {
                 Hex.GetHexByAxialCoordinates(hexs, f.Position).Figure = f;
            }
            ResetPolygons(hexs);
    
        }

        [DataMember]
        public Point Center
        {
            get { return center; }
            set { center = value; }
        }
        [DataMember]
        public Size ScreenSize
        {
            get { return screenSize; }
            set { screenSize = value; }
        }
        [DataMember]
        public Double BoardSize
        {
            get { return boardSize; }
            set { boardSize = value; }
        }
        [DataMember]
        public List<Point> Corners
        {
            get { return corners; }
            set { corners = value; }
        }
        [DataMember]
        public List<Hex> Hexs
        {
            get { return hexs; }
            set { hexs = value; }
        }
        [DataMember]
        public int Margin
        {
            get { return margin; }
            set { margin = value; }
        }
        [DataMember]
        public Double Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        [DataMember]
        public Double HexHeight
        {
            get { return hexHeight; }
            set { hexHeight = value; }
        }
        [DataMember]
        public Double HexWidth
        {
            get { return hexWidth; }
            set { hexWidth = value; }
        }
        [DataMember]
        public Polygon ActivePlayer
        {
            get { return activePlayer; }
            set { activePlayer = value; }
        }
        [DataMember]
        public Fraction ActiveFraction
        {
            get { return activeFraction; }
            set { activeFraction = value; }
        }
        [DataMember(IsRequired = false)]
        public Image LibraryImage
        {
            get { return libraryImage; }
            set { libraryImage = value; }
        }
        [DataMember(IsRequired = false)]
        public Image DragonLibrary
        {
            get { return dragonLibrary; }
            set { dragonLibrary = value; }
        }
        public Image DwarfLibrary
        {
            get { return dwarfLibrary; }
            set { dwarfLibrary = value; }
        }
       public void ChangeActivePlayer(Fraction f)
        {
           if(f==Fraction.Dwarf)
           {
                activePlayer.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 0, 0));
                 activePlayer.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155,255 , 0, 0));
                 activeFraction = Fraction.Dragon;

           }
           else{
                activePlayer.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 0, 0, 255));
                 activePlayer.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 0, 0, 255));
                 activeFraction = Fraction.Dwarf;


           }
        }
       public static void ResetPolygons(List<Hex> hexs)
        {

            foreach (Hex h in hexs)
                h.ResetPolygon();
        }
       public  Point ConvertPhonePointToBoardPoint(Point p, PageOrientation o)
       {
           
           if (o == PageOrientation.LandscapeRight)
            {
                double temp = p.X;
               
                p.X = this.screenSize.Height - p.Y - this.center.X;
                p.Y = -(this.center.Y-temp);

            }
       

            else if (o == PageOrientation.LandscapeLeft)
            {
                double temp = p.X;
             
                p.X = -(this.screenSize.Height - p.Y - this.center.X);
                p.Y = ((this.center.Y - temp));
            }
        
           return p;
       }
        public void DrawBoard( Grid g) //List<Polygon> drawBoard()
        {
            
        
            
            for (int i = 0; i < hexs.Count; i++)
            {
               if(hexs[i].Figure==null) g.Children.Add(hexs[i].Polygon);

            }
            for (int i = 0; i < hexs.Count; i++)
            {
                if (hexs[i].Figure != null) g.Children.Add(hexs[i].Polygon);

            }
            g.Children.Add(activePlayer);
            

            return; 
        }


    }
}
