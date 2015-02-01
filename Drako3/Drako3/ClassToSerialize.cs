using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Drako3
{
    
    [DataContract]
    [KnownType(typeof(Player))]
    [KnownType(typeof(Figure))]
    [KnownType(typeof(Dwarf))]
    [KnownType(typeof(Dragon))]
    public class ClassToSerialize
    {
        [DataMember]
        public GameType gameType;
        [DataMember]
        public string p1Name;
        [DataMember]
        public string p2Name;
        [DataMember]
        public Fraction p1Fraction;
        [DataMember]
        public Fraction p2Fraction;
        [DataMember]
        public int actionsLeft;
        [DataMember]
        public string activePlayer;
        [DataMember]
        public List<double> xPos = new List<double>(); //dragon, webber, cross,leader
        [DataMember]
        public List<double> yPos = new List<double>(); //dragon, webber, cross,leader
        [DataMember]
        public List<int> dwarfsHp = new List<int>(); //webber, cross,leader
        [DataMember]
        public List<int> dragonHp = new List<int>(); //shield,wings,legs,fire
        [DataMember]
        public List<Card> p1Hand = new List<Card>(); 
        [DataMember]
        public List<Card> p2Hand = new List<Card>();
        [DataMember]
        public List<Card> p1Library = new List<Card>();
        [DataMember]
        public List<Card> p2Library = new List<Card>();
        [DataMember]
        public List<Card> p1Graveyard = new List<Card>();
        [DataMember]
        public List<Card> p2Graveyard = new List<Card>(); 

        //public int cardsToDiscard;
        //public int damageToDragon;
       
        //public Player activPlayer;
       
        //public bool doubleAttack=false;
        //public bool doubleMove = false;
        //public Figure wasMovedInDouble = null;
        //public Figure wasAttackingInDouble = null;
       
        //public Dwarf leader;// = new Dwarf(DwarfType.Leader, new Point(1, -2));
        //public Dwarf crossbowman;// = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
        //public Dwarf webber;// = new Dwarf(DwarfType.Webber, new Point(1, 1));
        //public Dragon dragon;// = new Dragon(new Point(0, 0));
        //public bool showHandBeforeTurnChange=false;
        //public bool changeTurnAfterSlide = false;
       
        //public Card playedCard;
        public ClassToSerialize()
        {

        }
        public ClassToSerialize(Game g)
        {
            gameType = g.GameType;
            p1Name = g.P1.Name;
            p2Name = g.P2.Name;
            p1Fraction = g.P1.Fraction;
            p2Fraction = g.P2.Fraction;
            actionsLeft = g.ActionsLeft;
            activePlayer = g.ActivPlayer.Name;

            xPos.Add(g.Dragon.Position.X);
            if (g.Webber != null)
                xPos.Add(g.Webber.Position.X);
            else
                xPos.Add(-99);
            if (g.Crossbowman != null)
            xPos.Add(g.Crossbowman.Position.X);
            if (g.Leader != null)
            xPos.Add(g.Leader.Position.X);
            else
                xPos.Add(-99);


            yPos.Add(g.Dragon.Position.Y);
            if (g.Webber != null)
                yPos.Add(g.Webber.Position.Y);
            else
                yPos.Add(-99);
            if (g.Crossbowman != null)
            yPos.Add(g.Crossbowman.Position.Y);
            else
                yPos.Add(-99);
            if (g.Leader != null)
            yPos.Add(g.Leader.Position.Y);
            else
                yPos.Add(-99);

            if(g.Webber!=null)
            dwarfsHp.Add(g.Webber.Hp);
            else
                dwarfsHp.Add(0);
            if(g.Crossbowman!=null)
            dwarfsHp.Add(g.Crossbowman.Hp);
            else
                dwarfsHp.Add(0);
            if (g.Leader != null)
                dwarfsHp.Add(g.Leader.Hp);
            else
                dwarfsHp.Add(0);
            
            dragonHp.Add(g.Dragon.ShieldHp);
            dragonHp.Add(g.Dragon.WingsHp);
            dragonHp.Add(g.Dragon.LegsHp);
            dragonHp.Add(g.Dragon.FireHp);

            foreach(Card c in g.P1.Hand)
            {
                p1Hand.Add(c);
            }
            foreach (Card c in g.P2.Hand)
            {
                p2Hand.Add(c);
            }

            foreach (Card c in g.P1.Library)
            {
                p1Library.Add(c);
            }
            foreach (Card c in g.P2.Library)
            {
                p2Library.Add(c);
            }
            foreach (Card c in g.P1.Graveyard)
            {
                p1Graveyard.Add(c);
            }
            foreach (Card c in g.P2.Graveyard)
            {
                p2Graveyard.Add(c);
            }
        }

       
    }
}
