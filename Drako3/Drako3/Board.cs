using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Drako3
{
   public class Board
    {
       public Size screenSize;
       public  Double boardSize;
        public Point center;
        //Point[] corners;
        public List<Point> corners;
        public List<Hex> hexs;
        int margin = 12;
        double scale;
        double hexHeight;
        double hexWidth;
        

        public  Board(List<Figure> figures)
        {
            var content = Application.Current.Host.Content;
             scale = (double)content.ScaleFactor / 100;
          //  screenSize=new Size((int)Math.Ceiling(content.ActualWidth * scale),(int)Math.Ceiling(content.ActualHeight * scale));
               screenSize=new Size((int)Math.Ceiling(content.ActualWidth * scale),(int)Math.Ceiling(content.ActualHeight * scale));

            //size.Width = (int)Math.Ceiling(content.ActualWidth * scale);
           // size.Height = (int)Math.Ceiling(content.ActualHeight * scale);
        //    if()
           // center = new Point(screenSize.Height -screenSize.Width/2,  screenSize.Width / 2);
                center = new Point(screenSize.Height/2,  screenSize.Width / 2);

            //boardSize = new Size(screenSize.Width  - margin, screenSize.Width - margin);
            boardSize = screenSize.Width - margin;
           // center.X += boardSize/ 2 *  ((1 / 0.866) - 1);
           // corners = new Point[4] { new Point((center.Y - boardSize.Height / 2), (center.X - boardSize.Width / 2)), new Point((center.Y + boardSize.Height / 2), (center.X - boardSize.Width / 2)), new Point((center.Y + boardSize.Height / 2), (center.X + boardSize.Width / 2)), new Point((center.Y - boardSize.Height / 2), (center.X + boardSize.Width / 2)) };
             hexHeight = (boardSize / 5)/(Math.Sqrt(3)/2);
             Hex.hexHeight = hexHeight;
             hexWidth = Math.Sqrt(3) / 2 * hexHeight;
             Hex.hexWidth = hexWidth;
             hexs = new List<Hex>();
            corners = new List<Point>();
            
            for (int j = 0; j < 3;j++ )
            {
               // hexs.Add(new Hex(new Point(center.X - hexWidth)));
                hexs.Add(new Hex(new Point(((center.X - hexWidth) + j * hexWidth), (center.Y - 1.5 * hexHeight)), new Point(j, -2)));
              //  hexs[hexs.Count - 1].axialCoordinates = new Point(j, -2);
                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    // double d = (1 / 0.866)-1;
                    // d = 1.0 - d;
                    double x = (((center.X - hexWidth)+j*hexWidth) + hexHeight/2  * Math.Cos(angle));
                    double y = ((center.Y-1.5*hexHeight) + hexHeight / 2 * Math.Sin(angle));
                   // hexs[j].Add(new Point(x, y));
                    hexs[hexs.Count-1].corners.Add(new Point(x, y));

                }
            }
            
            
            for (int j = 0; j < 4; j++)
            {
                //hexs.Add(new List<Point>());
                hexs.Add(new Hex(new Point(((center.X - hexWidth*1.5) + j * hexWidth), (center.Y - 0.75 * hexHeight))));
                hexs[hexs.Count - 1].axialCoordinates = new Point(j-1, -1);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    // double d = (1 / 0.866)-1;
                    // d = 1.0 - d;
                    double x = (((center.X-hexWidth*1.5) + j * hexWidth) + hexHeight / 2 * Math.Cos(angle));
                    double y = ((center.Y - 0.75 * hexHeight) + hexHeight / 2 * Math.Sin(angle));
                   // hexs[j+3].Add(new Point(x, y));
                    hexs[hexs.Count - 1].corners.Add(new Point(x, y));


                }
            }

            for (int j = 0; j < 5; j++)
            {
               // hexs.Add(new List<Point>());
                hexs.Add(new Hex(new Point(((center.X - hexWidth * 2) + j * hexWidth), (center.Y ))));
                hexs[hexs.Count - 1].axialCoordinates = new Point(j-2, -0);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    // double d = (1 / 0.866)-1;
                    // d = 1.0 - d;
                    double x = (((center.X - hexWidth * 2) + j * hexWidth) + hexHeight / 2 * Math.Cos(angle));
                    double y = ((center.Y  ) + hexHeight / 2 * Math.Sin(angle));
                  //  hexs[j + 7].Add(new Point(x, y));
                    hexs[hexs.Count - 1].corners.Add(new Point(x, y));

                }
            }

            for (int j = 0; j < 4; j++)
            {
               // hexs.Add(new List<Point>());
                hexs.Add(new Hex(new Point(((center.X - hexWidth * 1.5) + j * hexWidth), (center.Y+0.75 * hexHeight))));
                hexs[hexs.Count - 1].axialCoordinates = new Point(j-2, 1);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    // double d = (1 / 0.866)-1;
                    // d = 1.0 - d;
                    double x = (((center.X - hexWidth * 1.5) + j * hexWidth) + hexHeight / 2 * Math.Cos(angle));
                    double y = ((center.Y + 0.75 * hexHeight) + hexHeight / 2 * Math.Sin(angle));
                   // hexs[j + 12].Add(new Point(x, y));
                    hexs[hexs.Count - 1].corners.Add(new Point(x, y));

                }
            }
            
            for (int j = 0; j < 3; j++)
            {
               // hexs.Add(new List<Point>());
                hexs.Add(new Hex(new Point(((center.X - hexWidth ) + j * hexWidth), (center.Y + 1.5 * hexHeight))));
                hexs[hexs.Count - 1].axialCoordinates = new Point(j-2, 2);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    // double d = (1 / 0.866)-1;
                    // d = 1.0 - d;
                    double x = (((center.X - hexWidth) + j * hexWidth) + hexHeight / 2 * Math.Cos(angle));
                    double y = ((center.Y + 1.5 * hexHeight) + hexHeight / 2 * Math.Sin(angle));
                 //   hexs[j+16].Add(new Point(x, y));
                    hexs[hexs.Count - 1].corners.Add(new Point(x, y));

                }
            }
           // Hex d=Hex.GetHexByAxialCoordinates(hexs, new Point(0, 0));
           // d.figure = new Dragon(d.axialCoordinates);
            foreach(Figure f in figures)
            {
                 Hex.GetHexByAxialCoordinates(hexs, f.position).figure = f;
               // a.figure=f;
            }
           // foreach (Hex h in hexs)
            //    h.ResetPolygon();
            ResetPolygons(hexs);
    
        }
       public static void ResetPolygons(List<Hex> hexs)
        {
            foreach (Hex h in hexs)
                h.ResetPolygon();
        }
        public void DrawBoard( Grid g) //List<Polygon> drawBoard()
        {
            
        //    Polygon p = new Polygon();
           // Polygon p2 = new Polygon();
           // Polygon p3 = new Polygon();
           // Polygon p4 = new Polygon();

          //  List<Polygon> Polygons = new List<Polygon>();
          //  p2.StrokeThickness = 3;
          //  p3.StrokeThickness = 3;
          //  p4.StrokeThickness = 3;
            /*
            p.StrokeThickness = 5;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Top;
            for (int i = 0; i < corners.Count; i++)
                p.Points.Add(corners[i]);

            for (int i = 0; i < hexs[0].Count; i++)
                p2.Points.Add(hexs[0][i]);
            for (int i = 0; i < hexs[1].Count; i++)
                p3.Points.Add(hexs[1][i]);
            for (int i = 0; i < hexs[2].Count; i++)
                p4.Points.Add(hexs[2][i]);
            /*
            p.Points.Add(new Point((center.Y - boardSize.Height / 2) , (center.X - boardSize.Width / 2) ));
            p.Points.Add(new Point((center.Y + boardSize.Height / 2) , (center.X - boardSize.Width / 2) ));
            p.Points.Add(new Point((center.Y + boardSize.Height / 2) , (center.X + boardSize.Width / 2) ));
            p.Points.Add(new Point((center.Y - boardSize.Height / 2) , (center.X + boardSize.Width / 2) ));
            */
            /*
              p.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155,0,255,0));
              p2.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 0, 255, 0));
              p3.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 0, 255, 0));
              p4.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 0, 255, 0));
  
            
            Polygons.Add(p);
              Polygons.Add(p2);
              Polygons.Add(p3);
              Polygons.Add(p4);
             * 
             * */
/*
            for (int i = 0; i < hexs.Count;i++ )
            {
                Polygon p = hexs[i].polygon;
               // p.StrokeThickness = 3;
               // for(int j=0;j<hexs[i].Count;j++)
              //  foreach(Point pnt in hexs[i].corners)
                //{
                  //  p.Points.Add(pnt);
               // }
               // p.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 0, 255, 1));
                hexs[i].polygon = p;
                //Polygons.Add(p);
            }
 * */
            for (int i = 0; i < hexs.Count; i++)
            {
               if(hexs[i].figure==null) g.Children.Add(hexs[i].polygon);

            }
            for (int i = 0; i < hexs.Count; i++)
            {
                if (hexs[i].figure != null) g.Children.Add(hexs[i].polygon);

            }
            
            return; //Polygons;
        }


    }
}
