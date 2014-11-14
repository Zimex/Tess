using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

enum DwarfType
{
    Crossbowman,
    Webber,
    Leader

}
namespace Drako3
{

    class Dwarf: Figure
    {
        private DwarfType type;
        private int hp;
        private readonly int originalHp;
        private PanoramaPage1 page;
        private List<Image> crossbowmanImageList = new List<Image>();
        private List<Image> webberImageList = new List<Image>();
        private List<Image> leaderImageList = new List<Image>();
        private List<Image> dwarfImageList = new List<Image>();


        public DwarfType Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Hp
        {
            get { return hp; }
            set { hp = value; PutDamageOnDwarf(); }
        }
        public PanoramaPage1 Page
        {
            get { return page; }
            set { page = value;  }
        }
        public List<Image> CrossbowmanImageList
        {
            get { return crossbowmanImageList; }
            set { crossbowmanImageList = value; }
        }
        public List<Image> WebberImageList
        {
            get { return webberImageList; }
            set { webberImageList = value; }
        }
        public List<Image> LeaderImageList
        {
            get { return leaderImageList; }
            set { leaderImageList = value; }
        }

        //public int b=1;
        public Dwarf(DwarfType t, Point p, PanoramaPage1 pag)
        {
            type = t;
            page = pag;
            Position = p;
            IsSelected = false;

            foreach (Image img in page.DwarfsGrid.Children)
            {
                if (img.Name == "crossbowmanCounter1")
                {
                    crossbowmanImageList.Add(img);
                }
                else
                    if (img.Name == "crossbowmanCounter2")
                    {
                        crossbowmanImageList.Add(img);
                    }
                    else
                        if (img.Name == "crossbowmanCounter3")
                        {
                            crossbowmanImageList.Add(img);
                        }
                        else
                            if (img.Name == "crossbowmanCounter4")
                            {
                                crossbowmanImageList.Add(img);
                            }
                            else
                                if (img.Name == "crossbowmanCounter5")
                                {
                                    crossbowmanImageList.Add(img);
                                }
                                else
                    if (img.Name == "webberCounter1")
                {
                    webberImageList.Add(img);
                }
                else
                        if (img.Name == "webberCounter2")
                        {
                            webberImageList.Add(img);
                        }
                        else
                            if (img.Name == "webberCounter3")
                            {
                                webberImageList.Add(img);
                            }
                            else
                                if (img.Name == "webberCounter4")
                                {
                                    webberImageList.Add(img);
                                }
                                else
                                if (img.Name == "leaderCounter1")
                                {
                                    leaderImageList.Add(img);
                                }
                                else
                                    if (img.Name == "leaderCounter2")
                                    {
                                        leaderImageList.Add(img);
                                    }
                                    else
                                        if (img.Name == "leaderCounter3")
                                        {
                                            leaderImageList.Add(img);
                                        }
                                        else
                                            if (img.Name == "leaderCounter4")
                                            {
                                                leaderImageList.Add(img);
                                            }
                                            else
                                                if (img.Name == "leaderCounter5")
                                                {
                                                    leaderImageList.Add(img);
                                                }
                                                else
                                                    if (img.Name == "leaderCounter6")
                                                    {
                                                        leaderImageList.Add(img);
                                
                                                    }
                                      
                                                   
            }

          

            switch(type)
            {
                case(DwarfType.Crossbowman):
                    hp = 5;
                    originalHp = 5;
                    dwarfImageList = crossbowmanImageList;
                    break;
                case(DwarfType.Leader):
                    hp = 6;
                    originalHp = 6;
                    dwarfImageList = leaderImageList;
                    break;
                case(DwarfType.Webber):
                    hp = 4;
                    originalHp = 4;
                    dwarfImageList = webberImageList;
                    break;
                default: break;

            }


        }
      

        public void PutDamageOnDwarf()
        {
            for (int i = 0; i < originalHp - hp; i++)
            {
                dwarfImageList[i].Visibility = Visibility.Visible;
            }
        }
    }
}
