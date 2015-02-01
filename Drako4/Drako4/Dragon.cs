using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Drako3
{
    [DataContract]
    
    public class Dragon:Figure
    {
        private int shieldHp = 4;
        private int wingsHp = 2;
        private int legsHp = 3;
        private int fireHp = 2;
        private readonly int originalShieldHp = 4;
        private readonly int originalWingsHp = 2;
        private readonly int originalLegsHp = 3;
        private readonly int originalFireHp = 2;
        private List<Image> shieldImageList = new List<Image>();
        private List<Image> wingsImageList = new List<Image>();
        private List<Image> legsImageList = new List<Image>();
        private List<Image> fireImageList = new List<Image>();
        private PanoramaPage1 page;
        private Point position;

        [DataMember]
        public int ShieldHp
        {
            get { return shieldHp; }
            set { if(value >=0)shieldHp = value;
            
            }
        }
        [DataMember]
        public int LegsHp
        {
            get { return legsHp; }
            set { if (value >= 0)legsHp = value;
            
            }
        }
        [DataMember]
        public int FireHp
        {
            get { return fireHp; }
            set { if (value >= 0)fireHp = value;
            
            }
        }
        [DataMember]
        public List<Image> ShieldImageList
        {
            get { return shieldImageList;}
            set{shieldImageList = value;}
        }
        [DataMember]
        public List<Image> WingsImageList
        {
            get { return wingsImageList; }
            set { wingsImageList = value; }
        }
        [DataMember]
        public List<Image> LegsImageList
        {
            get { return legsImageList; }
            set { legsImageList = value; }
        }
        [DataMember]
        public List<Image> FireImageList
        {
            get { return fireImageList; }
            set { fireImageList = value; }
        }
        [DataMember]
        public int WingsHp
        {
            get { return wingsHp; }
            set
            {
                if (value >= 0) wingsHp = value;
                
            }
        }
        public Dragon()
        {

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
        public void PutDamageOnDragon(int dmg)
        {

        }
        public int PutDamageOnShield(int dmg)
        {
            if (shieldHp >= dmg)
            {
                int d = originalShieldHp - shieldHp + dmg;
                for (int i = 0; i < d; i++)
                {
                    ShieldImageList[i].Visibility = Visibility.Visible;
                    shieldHp--;
                }
            }
            else 
            { 
            foreach(Image img in shieldImageList)
            {
                img.Visibility = Visibility.Visible;
            }
            int rest = dmg - shieldHp;
            shieldHp = 0;
            return rest;
            }
            return 0;
        }
        public int PutDamageOnLegs(int dmg)
        {
            if (LegsHp >= dmg)
            {
                int d = originalLegsHp - LegsHp + dmg;
                for (int i = 0; i < d; i++)
                {
                    LegsImageList[i].Visibility = Visibility.Visible;
                    LegsHp--;
                }
            }
            else
            {
                foreach (Image img in LegsImageList)
                {
                    img.Visibility = Visibility.Visible;
                }
                int rest = dmg - LegsHp;
                LegsHp = 0;
                return rest;
            }
            return 0;
        }
        public int PutDamageOnFire(int dmg)
        {
            if (FireHp >= dmg)
            {
                int d = originalFireHp - FireHp + dmg;
                for (int i = 0; i < d; i++)
                {
                    FireImageList[i].Visibility = Visibility.Visible;
                    FireHp--;
                }
            }
            else
            {
                foreach (Image img in FireImageList)
                {
                    img.Visibility = Visibility.Visible;
                }
                int rest = dmg - FireHp;
                FireHp = 0;
                return rest;
            }
            return 0;
        }
        public int PutDamageOnWings(int dmg)
        {
            if (WingsHp >= dmg)
            {
                int d = originalWingsHp - WingsHp + dmg;
                for (int i = 0; i < d; i++)
                {
                    WingsImageList[i].Visibility = Visibility.Visible;
                    WingsHp--;
                }
            }
            else
            {
                foreach (Image img in WingsImageList)
                {
                    img.Visibility = Visibility.Visible;
                }
                int rest = dmg - WingsHp;
                WingsHp = 0;
                return rest;
            }
            return 0;
        }
        
    }
}
