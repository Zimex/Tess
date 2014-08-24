using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Drako3
{
    public partial class NewGamePage : PhoneApplicationPage
    {
        public List<Hex> hexs;
        public Board board;
        List<Polygon> p;
        Dwarf leader = new Dwarf(DwarfType.Leader, new Point(1, -2));
        Dwarf crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
        Dwarf webber = new Dwarf(DwarfType.Webber, new Point(1, 1));
        Dragon dragon = new Dragon(new Point(0, 0));
        List<Figure> figures = new List<Figure>();
        Figure selectedFigure = null;

        public NewGamePage()
        {
            InitializeComponent();
           //// Figure f1 = new Dwarf();
            //Figure f2 = new Dragon();

          // string s= SupportedOrientations.ToString();
             leader = new Dwarf(DwarfType.Leader, new Point(1, -2));
             crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
             webber = new Dwarf(DwarfType.Webber, new Point(1, 1));
             dragon = new Dragon(new Point(0, 0));
             figures = new List<Figure>();
             selectedFigure = null;
            figures.Add(leader);
            figures.Add(crossbowman);
            figures.Add(webber);
            figures.Add(dragon);

            board = new Board(figures);
            board.DrawBoard(LayoutRoot);
            hexs = board.hexs;
            for (int i = 0; i < hexs.Count; i++)
            {
              //  LayoutRoot.Children.Add(hexs[i].polygon);
            }
          // Hex h= Hex.GetHexByAxialCoordinates(hexs,new Point(0, 0));
         //  SelectFigure(h);
           
        }
        public void SelectFigure(Hex h)
        {
            Figure figure;
            if (h.figure != null)
            {
                figure = h.figure;

                if (figure.GetType() == typeof(Dragon))
                {
                    h.polygon.StrokeThickness = 5;
                    h.polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 0, 0));
                    h.polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                }
                else if (figure.GetType() == typeof(Dwarf))
                {
                    Dwarf d = figure as Dwarf;

                    switch (d.type)
                    {
                        case (DwarfType.Crossbowman):
                            h.polygon.StrokeThickness = 5;
                            h.polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 255, 0));
                            h.polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 0));

                            break;
                        case (DwarfType.Leader):
                            h.polygon.StrokeThickness = 5;
                            h.polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 128, 0));
                            h.polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 128, 0));

                            break;
                        case (DwarfType.Webber):
                            h.polygon.StrokeThickness = 5;
                            h.polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 128, 255, 0));
                            h.polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 128, 255, 0));


                            break;
                    }
                }
            }
        }

        private void screenTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Point p = e.GetPosition(null);

          //  MessageBox.Show(p.X.ToString() + " " + p.Y.ToString());
            //Box1.Text = p.X.ToString() + " " + p.Y.ToString();
            if (this.Orientation == PageOrientation.LandscapeRight)
            {
                double temp = p.X;
                p.X = -p.Y + board.center.Y;
                p.Y = temp - board.center.Y;
            }
            else if(this.Orientation==PageOrientation.LandscapeLeft)
            {
                double temp = p.X;
                p.X = -p.Y + board.center.Y;
              //  p.X = board.screenSize.Width - p.X;
                p.Y = temp -board.center.Y;
               //  p.Y = temp+board.screenSize.Height;

              //  p.X = -p.X-(board.screenSize.Width- board.center.X/2.0);
                p.Y = -p.Y;
                p.X = -p.X;
                p.X = p.X - (board.screenSize.Height - board.center.Y)+150+Hex.hexWidth;
              //  p.Y = p.Y - 100;
       //         p.X += -(board.screenSize.Height - board.center.Y);
               // p.X = board.screenSize.Width - p.X;
              //  p.Y = board.screenSize.Height - p.Y;

            }
           // p.X += -board.center.Y;
           
           Hex h= Hex.PixelToHex(p, hexs);

                
                if(h!=null)
            {
               // Hex h2 = Hex.GetHexByAxialCoordinates(hexs, new Point(0, 0));
                 // SelectFigure(h2);
                if (selectedFigure == null)
                {
                   // if(h.figure!=null)
                    selectedFigure = h.figure;
                 //   foreach (Hex h2 in hexs)
                 //       h2.ResetPolygon();
                  //  Board.ResetPolygons(hexs);
                    SelectFigure(h);
                   // h.polygon.Stroke.GetValue(Color.FromArgb);
                                                   // polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 128, 255, 0));

                }
                    
                else
                    if(selectedFigure==h.figure)
                    {
                        
                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);

                        selectedFigure = null;
                    }
                    else
                        if(h.figure==null && selectedFigure!=null)
                        {
                           // Hex.GetHexByFigure(hexs, selectedFigure).
                            if(Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs,selectedFigure),h,2,hexs))
                            selectedFigure.MoveTo(h.axialCoordinates, hexs);
                            selectedFigure = null;
                            Board.ResetPolygons(hexs);
                            Board.ResetPolygons(hexs);

                        }
                    
                    
            }
                /*
                 * 
            if(h!=null)
            {
                Polygon pp = new Polygon();
                pp.StrokeThickness = 5;
                pp.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 255, 0, 1));
                    //( new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 1)));
                foreach(Point pnt in h.corners)
                {
                    pp.Points.Add(pnt);
                }
                
                Board b = new Board();
                List<Polygon> pol = b.drawBoard();
                LayoutRoot.Children.Clear();
                hexs = b.hexs;
                for (int i = 0; i < pol.Count; i++)
                {
                    LayoutRoot.Children.Add(pol[i]);
                }
                pp.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 1));
                LayoutRoot.Children.Add(pp);
            }
             * */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Board.ResetPolygons(hexs);
            Board.ResetPolygons(hexs);

        }
           
    }
}