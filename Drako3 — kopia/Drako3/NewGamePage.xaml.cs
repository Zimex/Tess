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
        //public List<Hex> hexs;
        //public Board board;
        //List<Polygon> p;
        //List<Figure> figures = new List<Figure>();
        //Figure selectedFigure = null;

        public NewGamePage()
        {
            InitializeComponent();

            //figures = new List<Figure>();
            //selectedFigure = null;

            //board = new Board();
            //board.DrawBoard(LayoutRoot);
            //hexs = board.Hexs;
            //for (int i = 0; i < hexs.Count; i++)
            //{
            //    //  LayoutRoot.Children.Add(hexs[i].polygon);
            //}


        }
        public void SelectFigure(Hex h)
        {
            Figure figure;
            if (h.Figure != null)
            {
                figure = h.Figure;

                if (figure.GetType() == typeof(Dragon))
                {
                    h.Polygon.StrokeThickness = 5;
                    h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 0, 0));
                    h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                }
                else if (figure.GetType() == typeof(Dwarf))
                {
                    Dwarf d = figure as Dwarf;

                    switch (d.Type)
                    {
                        case (DwarfType.Crossbowman):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 255, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 0));

                            break;
                        case (DwarfType.Leader):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 128, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 128, 0));

                            break;
                        case (DwarfType.Webber):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 128, 255, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 128, 255, 0));


                            break;
                    }
                }
            }
        }

        private void screenTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //Point p = e.GetPosition(null);
            //if (this.Orientation == PageOrientation.LandscapeRight)
            //{
            //    double temp = p.X;
            //    p.X = -p.Y + board.Center.Y;
            //    p.Y = temp - board.Center.Y;
            //}
            //else if (this.Orientation == PageOrientation.LandscapeLeft)
            //{
            //    double temp = p.X;
            //    p.X = -p.Y + board.Center.Y;
            //    p.Y = temp - board.Center.Y;
            //    p.Y = -p.Y;
            //    p.X = -p.X;
            //    p.X = p.X - (board.ScreenSize.Height - board.Center.Y) + 150 + Hex.hexWidth;

            //}
            //Hex h = Hex.PixelToHex(p, hexs);
            //if (h != null)
            //{

            //    if (selectedFigure == null)
            //    {

            //        selectedFigure = h.Figure;
            //        SelectFigure(h);

            //    }

            //    else
            //        if (selectedFigure == h.Figure)
            //        {

            //            Board.ResetPolygons(hexs);
            //            Board.ResetPolygons(hexs);

            //            selectedFigure = null;
            //        }
            //        else
            //            if (h.Figure == null && selectedFigure != null)
            //            {
            //                if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, 2, hexs))
            //                    selectedFigure.MoveTo(h.AxialCoordinates, hexs);
            //                selectedFigure = null;
            //                Board.ResetPolygons(hexs);
            //                Board.ResetPolygons(hexs);

            //            }


            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Board.ResetPolygons(hexs);
            //Board.ResetPolygons(hexs);

        }

    }
}