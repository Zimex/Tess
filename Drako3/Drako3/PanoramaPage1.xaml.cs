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
using System.Windows.Media.Imaging;

namespace Drako3
{
    public partial class PanoramaPage1 : PhoneApplicationPage
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
        Player P1,P2;
        
     

        public PanoramaPage1()
        {
            InitializeComponent();
       //     BitmapImage src = new BitmapImage();
            P1 = new Player("Zimex", Fraction.Dragon, HandGrid);
            P1.DrawInitialHand();
            P2 = new Player("Zaremox", Fraction.Dwarf, HandGrid);
            P2.DrawInitialHand();
            P1.RenderHand();
            
            //libraryImage.Source = dragonLibrary.Source;
         /*   Image im = HandGrid.Children[0] as Image;
            src.UriSource = new Uri("Images/Cards/K01.png", UriKind.Relative);
            im.Source = src;
            Image im2 = HandGrid.Children[4] as Image;
            src.UriSource = new Uri("Images/Dragon.jpg", UriKind.Relative);
            im2.Source = src;
          * */
           // src.UriSource
         //   src.BeginInit();
           // src.UriSource = new Uri("Images/Dwarf.jpg", UriKind.Relative);
          //  src.CacheOption = BitmapCacheOption.OnLoad;
          //  src.EndInit();

           // img = new Image();
           // img.Source = src;
           // HandGrid.DataContext = new GridLength(400);
          //  for (int i = 0; i < 4; i++)
            //    HandGrid.ColumnDefinitions.Add(new ColumnDefinition());
           // HandGrid.DataContext
            ImageSource s;

            /*foreach (Image img2 in HandGrid.Children)
            {
                s = img2.Source;
            }
            */
//foreach(Image img in HandGrid.Children)
//            {
//                BitmapImage src = new BitmapImage();
//                int index = HandGrid.Children.IndexOf(img);

//                if (index >= P1.hand.Count)
//                {
                    
//                    src.UriSource = new Uri("Images/Dwarf.jpg", UriKind.Relative);
//                    // src.UriSource = new Uri(P1.hand[0].src, UriKind.Relative);

//                    img.Source = src;
//                } 
//                else
//                {
//                   // src.UriSource = new Uri(P1.hand[index].src, UriKind.Relative);
//                   // src.UriSource = new Uri(P1.hand[index].src, UriKind.Relative);
//                    src.UriSource = new Uri("Images/Dragon.jpg", UriKind.Relative);
//                    img.Source = src;


//                }
//                Image i = HandGrid.Children[0] as Image;
//                s = i.Source;
//            }
            //src.UriSource = new Uri("Images/Dwarf.jpg", UriKind.Relative);
            //img.Source = src;
            //    def.Width = new GridLength(100);
                
           // }
         //  Grid g = HandGrid;
         //  ImageSource s;
           foreach (Image img in HandGrid.Children)
                s= img.Source;
        //    foreach()
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
            board.DrawBoard(BoardGrid);


            //board.DrawBoard(LayoutRoot);

            hexs = board.hexs;
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

        private void PanoramaItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
           ImageSource s;
            foreach (Image img in HandGrid.Children)
                s = img.Source;
           // board.activePlayer.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 55, 0, 0));
          //  board.activePlayer.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 255, 0, 0));
        
            Point p = e.GetPosition(null);
            if (this.Orientation == PageOrientation.LandscapeLeft)
            {
                if (p.X >= board.screenSize.Width - 30 && p.X <= board.screenSize.Width - 5 && p.Y >= 5 && p.Y <= 25)
                {

                    board.ChangeActivePlayer(P1.ReturnActiveFraction());
                    if (P1.isActiv == true)
                    {
                        P1.isActiv = false;
                        P2.isActiv = true;
                    }
                    else
                    {
                        P2.isActiv = false;
                        P1.isActiv = true;
                    }
                    // BoardGrid.Children.Add(board.activePlayer);
                    return;
                }
            }
            else if (this.Orientation == PageOrientation.LandscapeRight)
            {
                if (p.X >= 5 && p.X <= 25 && p.Y >= board.screenSize.Height - 30 && p.Y <= board.screenSize.Height - 5)
                {

                    board.ChangeActivePlayer(P1.ReturnActiveFraction());
                    if (P1.isActiv == true)
                    {
                        P1.isActiv = false;
                        P2.isActiv = true;
                    }
                    else
                    {
                        P2.isActiv = false;
                        P1.isActiv = true;
                    }
                    // BoardGrid.Children.Add(board.activePlayer);
                    return;
                }
            }

            p=board.ConvertPhonePointToBoardPoint(p, this.Orientation);

            //  MessageBox.Show(p.X.ToString() + " " + p.Y.ToString());
            //Box1.Text = p.X.ToString() + " " + p.Y.ToString();
            /*
            if (this.Orientation == PageOrientation.LandscapeRight)
            {
                double temp = p.X;
               // p.X = -p.Y + board.center.Y;
               // p.Y = temp - board.center.Y;
                //p.X = -p.Y;
                p.X = board.screenSize.Height - p.Y - board.center.X;
                p.Y = -(board.center.Y-temp);

            }

            else if (this.Orientation == PageOrientation.LandscapeLeft)
            {
                double temp = p.X;
              //  p.X=
                //p.Y = -board.center.Y + temp;
                p.X = -(board.screenSize.Height - p.Y - board.center.X);
                p.Y = ((board.center.Y - temp));
               // p.X = -p.Y + board.center.Y;
                //  p.X = board.screenSize.Width - p.X;
               // p.Y = temp - board.center.Y;
                //  p.Y = temp+board.screenSize.Height;

                //  p.X = -p.X-(board.screenSize.Width- board.center.X/2.0);
             //   p.Y = -p.Y;
               // p.X = -p.X;
                //p.X = p.X - (board.screenSize.Height - board.center.Y) + 150 + Hex.hexWidth;
                //  p.Y = p.Y - 100;
                //         p.X += -(board.screenSize.Height - board.center.Y);
                // p.X = board.screenSize.Width - p.X;
                //  p.Y = board.screenSize.Height - p.Y;

            }
             * */
            // p.X += -board.center.Y;

            Hex h = Hex.PixelToHex(p, hexs);


            if (h != null)
            {
                // Hex h2 = Hex.GetHexByAxialCoordinates(hexs, new Point(0, 0));
                // SelectFigure(h2);
                if (selectedFigure == null)
                {
                     if(h.figure!=null)
                    if (board.activeFraction == h.figure.ReturnFraction())
                    {
                        selectedFigure = h.figure;
                        //   foreach (Hex h2 in hexs)
                        //       h2.ResetPolygon();
                        //  Board.ResetPolygons(hexs);
                        SelectFigure(h);
                    }
                    // h.polygon.Stroke.GetValue(Color.FromArgb);
                    // polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(155, 128, 255, 0));

                }

                else
                    if (selectedFigure == h.figure)
                    {

                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);

                        selectedFigure = null;
                    }
                    else
                        if (h.figure == null && selectedFigure != null)
                        {
                            // Hex.GetHexByFigure(hexs, selectedFigure).
                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, 2, hexs))
                            {
                                selectedFigure.MoveTo(h.axialCoordinates, hexs);
                                selectedFigure = null;

                                Board.ResetPolygons(hexs);
                                Board.ResetPolygons(hexs);
                            }
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
         
            
            }
            
        }
        /*
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
            else if (this.Orientation == PageOrientation.LandscapeLeft)
            {
                double temp = p.X;
                p.X = -p.Y + board.center.Y;
                //  p.X = board.screenSize.Width - p.X;
                p.Y = temp - board.center.Y;
                //  p.Y = temp+board.screenSize.Height;

                //  p.X = -p.X-(board.screenSize.Width- board.center.X/2.0);
                p.Y = -p.Y;
                p.X = -p.X;
                p.X = p.X - (board.screenSize.Height - board.center.Y) + 150 + Hex.hexWidth;
                //  p.Y = p.Y - 100;
                //         p.X += -(board.screenSize.Height - board.center.Y);
                // p.X = board.screenSize.Width - p.X;
                //  p.Y = board.screenSize.Height - p.Y;

            }
            // p.X += -board.center.Y;

            Hex h = Hex.PixelToHex(p, hexs);


            if (h != null)
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
                    if (selectedFigure == h.figure)
                    {

                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);

                        selectedFigure = null;
                    }
                    else
                        if (h.figure == null && selectedFigure != null)
                        {
                            // Hex.GetHexByFigure(hexs, selectedFigure).
                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, 2, hexs))
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

        private void libraryImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
          //  board.DrawBoard(BoardGrid);

        }

        /*private void Panorama_Tap(object sender, System.Windows.Input.GestureEventArgs e)
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
            else if (this.Orientation == PageOrientation.LandscapeLeft)
            {
                double temp = p.X;
                p.X = -p.Y + board.center.Y;
                //  p.X = board.screenSize.Width - p.X;
                p.Y = temp - board.center.Y;
                //  p.Y = temp+board.screenSize.Height;

                //  p.X = -p.X-(board.screenSize.Width- board.center.X/2.0);
                p.Y = -p.Y;
                p.X = -p.X;
                p.X = p.X - (board.screenSize.Height - board.center.Y) + 150 + Hex.hexWidth;
                //  p.Y = p.Y - 100;
                //         p.X += -(board.screenSize.Height - board.center.Y);
                // p.X = board.screenSize.Width - p.X;
                //  p.Y = board.screenSize.Height - p.Y;

            }
            // p.X += -board.center.Y;

            Hex h = Hex.PixelToHex(p, hexs);


            if (h != null)
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
                    if (selectedFigure == h.figure)
                    {

                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);

                        selectedFigure = null;
                    }
                    else
                        if (h.figure == null && selectedFigure != null)
                        {
                            // Hex.GetHexByFigure(hexs, selectedFigure).
                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, 2, hexs))
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
         
            
            }
            
        }
        /*
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
            else if (this.Orientation == PageOrientation.LandscapeLeft)
            {
                double temp = p.X;
                p.X = -p.Y + board.center.Y;
                //  p.X = board.screenSize.Width - p.X;
                p.Y = temp - board.center.Y;
                //  p.Y = temp+board.screenSize.Height;

                //  p.X = -p.X-(board.screenSize.Width- board.center.X/2.0);
                p.Y = -p.Y;
                p.X = -p.X;
                p.X = p.X - (board.screenSize.Height - board.center.Y) + 150 + Hex.hexWidth;
                //  p.Y = p.Y - 100;
                //         p.X += -(board.screenSize.Height - board.center.Y);
                // p.X = board.screenSize.Width - p.X;
                //  p.Y = board.screenSize.Height - p.Y;

            }
            // p.X += -board.center.Y;

            Hex h = Hex.PixelToHex(p, hexs);


            if (h != null)
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
                    if (selectedFigure == h.figure)
                    {

                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);

                        selectedFigure = null;
                    }
                    else
                        if (h.figure == null && selectedFigure != null)
                        {
                            // Hex.GetHexByFigure(hexs, selectedFigure).
                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, 2, hexs))
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
         

        }
        */
        /*
        private void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
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
            else if (this.Orientation == PageOrientation.LandscapeLeft)
            {
                double temp = p.X;
                p.X = -p.Y + board.center.Y;
                //  p.X = board.screenSize.Width - p.X;
                p.Y = temp - board.center.Y;
                //  p.Y = temp+board.screenSize.Height;

                //  p.X = -p.X-(board.screenSize.Width- board.center.X/2.0);
                p.Y = -p.Y;
                p.X = -p.X;
                p.X = p.X - (board.screenSize.Height - board.center.Y) + 150 + Hex.hexWidth;
                //  p.Y = p.Y - 100;
                //         p.X += -(board.screenSize.Height - board.center.Y);
                // p.X = board.screenSize.Width - p.X;
                //  p.Y = board.screenSize.Height - p.Y;

            }
            // p.X += -board.center.Y;

            Hex h = Hex.PixelToHex(p, hexs);


            if (h != null)
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
                    if (selectedFigure == h.figure)
                    {

                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);

                        selectedFigure = null;
                    }
                    else
                        if (h.figure == null && selectedFigure != null)
                        {
                            // Hex.GetHexByFigure(hexs, selectedFigure).
                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, 2, hexs))
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
         
            
            }
            
        }
        /*
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
            else if (this.Orientation == PageOrientation.LandscapeLeft)
            {
                double temp = p.X;
                p.X = -p.Y + board.center.Y;
                //  p.X = board.screenSize.Width - p.X;
                p.Y = temp - board.center.Y;
                //  p.Y = temp+board.screenSize.Height;

                //  p.X = -p.X-(board.screenSize.Width- board.center.X/2.0);
                p.Y = -p.Y;
                p.X = -p.X;
                p.X = p.X - (board.screenSize.Height - board.center.Y) + 150 + Hex.hexWidth;
                //  p.Y = p.Y - 100;
                //         p.X += -(board.screenSize.Height - board.center.Y);
                // p.X = board.screenSize.Width - p.X;
                //  p.Y = board.screenSize.Height - p.Y;

            }
            // p.X += -board.center.Y;

            Hex h = Hex.PixelToHex(p, hexs);


            if (h != null)
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
                    if (selectedFigure == h.figure)
                    {

                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);

                        selectedFigure = null;
                    }
                    else
                        if (h.figure == null && selectedFigure != null)
                        {
                            // Hex.GetHexByFigure(hexs, selectedFigure).
                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, 2, hexs))
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
         

        }
        
      */
    }
}