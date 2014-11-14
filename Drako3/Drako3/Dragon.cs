using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Drako3
{
    
    class Dragon:Figure
    {
        private int shieldHp = 4;
        private int wingsHp = 2;
        private int legsHp = 3;
        private int fireHp = 2;
        private List<Image> shieldImageList = new List<Image>();
        private List<Image> wingsImageList = new List<Image>();
        private List<Image> legsImageList = new List<Image>();
        private List<Image> fireImageList = new List<Image>();
        private PanoramaPage1 page;
        private Point position;
        
        public int ShieldHp
        {
            get { return shieldHp; }
            set { if(value >=0)shieldHp = value;
            PutDamageOnShield();
            }
        }
        public int LegsHp
        {
            get { return legsHp; }
            set { if (value >= 0)legsHp = value;
            PutDamageOnLegs();
            }
        }
        public int FireHp
        {
            get { return fireHp; }
            set { if (value >= 0)fireHp = value;
            PutDamageOnFire();
            }
        }
        public List<Image> ShieldImageList
        {
            get { return shieldImageList;}
            set{shieldImageList = value;}
        }
        public List<Image> WingsImageList
        {
            get { return wingsImageList; }
            set { wingsImageList = value; }
        }
        public List<Image> LegsImageList
        {
            get { return legsImageList; }
            set { legsImageList = value; }
        }
        public List<Image> FireImageList
        {
            get { return fireImageList; }
            set { fireImageList = value; }
        }
        public int WingsHp
        {
            get { return wingsHp; }
            set
            {
                if (value >= 0) wingsHp = value;
                PutDamageOnWings();
            }
        }

        public Dragon(Point p, PanoramaPage1 pag)
        {
            page = pag;
            position = p;
            IsSelected = false;
            foreach (Image img in page.DragonGrid.Children)
            {
                if(img.Name=="shield1")
                {
                    shieldImageList.Add(img);
                } else
                if (img.Name == "shield2")
                {
                    shieldImageList.Add(img);
                } else
                if (img.Name == "shield3")
                {
                    shieldImageList.Add(img);
                }else
                if (img.Name == "shield4")
                {
                    shieldImageList.Add(img);
                } else
                    if (img.Name == "legs1")
                    {
                        legsImageList.Add(img);
                    }
                    else
                        if (img.Name == "legs2")
                        {
                            legsImageList.Add(img);
                        }
                        else
                            if (img.Name == "legs3")
                            {
                                legsImageList.Add(img);
                            }
                            else
                                if (img.Name == "fire1")
                                {
                                    fireImageList.Add(img);
                                }
                                else
                                    if (img.Name == "fire2")
                                    {
                                        fireImageList.Add(img);
                                    }
                                    else
                                        if (img.Name == "wings1")
                                        {
                                            wingsImageList.Add(img);
                                        }
                                        else
                                        if (img.Name == "wings2")
                                        {
                                            wingsImageList.Add(img);
                                        }
            }
            
        }
        public bool IsDead()
        {
            if(ShieldHp==0 &&LegsHp==0 && FireHp==0 &&WingsHp==0)
            {
                return true;
            }
                return false;
        }
        public void PutDamageOnShield()
        {
            for(int i=0;i<4-ShieldHp;i++)
            {
                shieldImageList[i].Visibility = Visibility.Visible;
            }
        }
        public void PutDamageOnLegs()
        {
            for (int i = 0; i < 3 - LegsHp; i++)
            {
                legsImageList[i].Visibility = Visibility.Visible;
            }
        }
        public void PutDamageOnFire()
        {
            for (int i = 0; i < 2 - FireHp; i++)
            {
                fireImageList[i].Visibility = Visibility.Visible;
            }
        }
        public void PutDamageOnWings()
        {
            for (int i = 0; i < 2 - WingsHp; i++)
            {
                wingsImageList[i].Visibility = Visibility.Visible;
            }
        }
    }
}
