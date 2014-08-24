using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
namespace Drako3
{
    
    public class Hex
    {
      public  Point center;
      public  Point axialCoordinates; // 0,0...6,6
      public  List<Point> corners = new List<Point>();
      public bool isBlocked=false;
      public static double hexHeight;
      public static double hexWidth;
      public Figure figure = null;
      public Polygon polygon = new Polygon();

        public Hex()
        {
            ResetPolygon();
        }
        public Hex(Point c)
        {
            center = c;
            ResetPolygon();
            
        }
        public Hex(Point c, Point ax)
        {
            center = c;
            axialCoordinates = ax;
            ResetPolygon();
        }
      
        public static Microsoft.Xna.Framework.Vector3 ConvertAxialToCube(Point p)
        {
            return new Microsoft.Xna.Framework.Vector3((float)p.X,(float)(-p.X-p.Y),(float)p.Y);
        }
        public static Point ConvertCubeToAxial(Microsoft.Xna.Framework.Vector3 v)
        {
            return new Point(v.X,v.Z);
        }
        public static int distance(Hex h1, Hex h2)
        {

            return (int)(Math.Abs(h1.axialCoordinates.X - h2.axialCoordinates.X) + Math.Abs(h1.axialCoordinates.Y - h2.axialCoordinates.Y)
                  + Math.Abs(h1.axialCoordinates.X + h1.axialCoordinates.Y - h2.axialCoordinates.X - h2.axialCoordinates.Y)) / 2;

        }
        public static bool CanBeMovedTo(Hex from, Hex to, int moves,List<Hex> hexs)
        {
           // List<Hex> visited = new List<Hex>();
            //for(KeyedByTypeCollection=2;KeyedByTypeCollection<)
            List<List<Hex>> visited = new List<List<Hex>>();
            visited.Add(new List<Hex>());
            visited[visited.Count-1].Add(from);

            for (int i = 1; i <= moves;i++ )
            {
                visited.Add(new List<Hex>());
                foreach(Hex h in visited[i-1])
                {
                    foreach (Hex h2 in GetUnblockedNeighbors(hexs, h, visited))
                        visited[visited.Count - 1].Add(h2);
//visited.Add(GetUnblockedNeighbors(hexs, h,visited));

                }


            }
            if (Hex.DoesVisitedContainsHex(visited, to))
            {
                return true;
            }
                return false;
        }
        public static List<Hex> GetUnblockedNeighbors(List<Hex> hexs, Hex h, List<List<Hex>> visited)
        {
            List<Hex> neighbors = new List<Hex>();
            foreach( Hex hex in Neighbors(h, hexs))
            {
                if(hex.figure==null && !DoesVisitedContainsHex(visited,hex))
                {
                    neighbors.Add(hex);
                }
            }
            return neighbors;

        }

        public static bool DoesVisitedContainsHex(List<List<Hex>> visited, Hex h)
        {
            foreach(List<Hex> list in visited)
                foreach(Hex hex in list)
                {
                    if (hex.axialCoordinates == h.axialCoordinates) 
                        return true;
                }
            return false;
        }

        public static bool DoesContainHex(List<Hex> hexs,Point p) //axialCoords
        {
            foreach(Hex h in hexs)
            {
                if (h.axialCoordinates == p)
                    return true;
            }
            return false;
        }
        public static Hex PixelToHex(Point p, List<Hex> list)
        {
            //double a = 1.0 / 3 * Math.Sqrt(3) * p.X;
            //double b = 1.0 / 3 * p.Y;
            //double c = a - b;
            //double d = hexHeight / 2.0;
           // double q = c / d;
            double q = (1.0 / 3.0 * Math.Sqrt(3) * p.X - 1.0 / 3.0 * p.Y) / (hexHeight/2.0);
            double r = 2.0 / 3.0 * p.Y / (hexHeight / 2.0);
             Microsoft.Xna.Framework.Vector3 cube= ConvertAxialToCube(new Point(q, r));
             Microsoft.Xna.Framework.Vector3 roundedCube = RoundHex(cube);
            Point roundedAxial = ConvertCubeToAxial(roundedCube);
            if(DoesContainHex(list,roundedAxial))
            {
                return GetHexByAxialCoordinates(list,roundedAxial);
            }

            return null;
        }
        public static Microsoft.Xna.Framework.Vector3 RoundHex(Microsoft.Xna.Framework.Vector3 cube)
        {
            double rx = Math.Round(cube.X);
            double ry = Math.Round(cube.Y);
            double  rz = Math.Round(cube.Z);

            double x_diff = Math.Abs(rx - cube.X);
            double y_diff = Math.Abs(ry - cube.Y);
            double z_diff = Math.Abs(rz - cube.Z);

   

    if (x_diff > y_diff && x_diff > z_diff)
        rx = -ry-rz;
    else if (y_diff > z_diff)
        ry = -rx-rz;
    else
            rz = -rx-ry;

    return new Microsoft.Xna.Framework.Vector3((float)rx, (float)ry, (float)rz);
        }

        public static Hex GetHexByAxialCoordinates(List<Hex> list, Point p)
        {
            foreach(Hex h in list)
            {
                if (h.axialCoordinates == p)
                    return h;
            }
            return null;
        }

        public static Hex GetHexByFigure(List<Hex> hexs, Figure f)
        {
            foreach(Hex h in hexs)
            {

                if(h.figure ==f)
                {
                    return h;
                }

            }
            return null;

        }

        public static List<Hex> Neighbors(Hex h, List<Hex> hexs)
        {
  
            List<Hex> neighbors=new List<Hex>();
            List<Point> vec=new List<Point>();
            vec.Add(new Point(1,0));
            vec.Add(new Point(1,-1));
            vec.Add(new Point(0,-1));
            vec.Add(new Point(-1,0));
            vec.Add(new Point(-1,1));
            vec.Add(new Point(0,1));


            
            for (int i = 0; i < 6;i++ )
            {
                Hex n = new Hex();
                n.axialCoordinates.X = h.axialCoordinates.X + vec[i].X;
                n.axialCoordinates.Y = h.axialCoordinates.Y + vec[i].Y;
                if(DoesContainHex(hexs,n.axialCoordinates))
                {
                    neighbors.Add(Hex.GetHexByAxialCoordinates(hexs,n.axialCoordinates));
                }

            }

                return neighbors;
        }
        public override string ToString()
        {
            string s = this.axialCoordinates.X + "," + this.axialCoordinates.Y + Environment.NewLine;
            if (figure != null) s += "figure:" + figure.ToString() + Environment.NewLine;
            return s;
        }
           
        public void ResetPolygon()
        {
            //this.polygon = new Polygon();
            this.polygon.StrokeThickness = 3;
            
            foreach (Point pnt in corners)
            {
                polygon.Points.Add(pnt);
            }
            if(figure==null)
            {
            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 0, 255, 1));
           // polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 0, 0));
            polygon.Fill=new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(0,0,0,0));
            }   
            else
            {
                if (figure.GetType() == typeof(Dragon))
                {
                    polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 0, 0));
                    polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 255, 0, 0));
                }
                else if (figure.GetType() == typeof(Dwarf))
                {
                    Dwarf d = figure as Dwarf;

                    switch(d.type)
                    {
                        case(DwarfType.Crossbowman):
                            polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 255, 0));
                            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 255, 255, 0));
               
                            break;
                        case (DwarfType.Leader):
                            polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 128, 0));
                            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 255, 128, 0));
               
                            break;
                        case (DwarfType.Webber):
                            polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 128, 255, 0));
                            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 128, 255, 0));
               

                            break;
                    }
                   // polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 0, 0, 255));
                }
            }

        }
    }
}
