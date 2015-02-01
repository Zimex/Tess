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
        public Player p1, p2;
        [DataMember]
        public int actionsLeft;
        [DataMember]
        public int cardsToDiscard;
        public int damageToDragon;
       
        public Player activPlayer;
       
        public bool doubleAttack=false;
        public bool doubleMove = false;
        public Figure wasMovedInDouble = null;
        public Figure wasAttackingInDouble = null;
       
        public Dwarf leader;// = new Dwarf(DwarfType.Leader, new Point(1, -2));
        public Dwarf crossbowman;// = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
        public Dwarf webber;// = new Dwarf(DwarfType.Webber, new Point(1, 1));
        public Dragon dragon;// = new Dragon(new Point(0, 0));
        public bool showHandBeforeTurnChange=false;
        public bool changeTurnAfterSlide = false;
       
        public Card playedCard;

   

       
    }
}
