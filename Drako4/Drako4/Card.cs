using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Drako3
{
    [DataContract]
    [KnownType(typeof(Action))]
    public class Card
    {
        private int id;
        private static bool areActionsInitialized = false;
        private Fraction fraction;
        private Action action1=null;
        private Action action2 = null;
        public static int dragonCards = 11;
        public static int dwarfCards = 16;
        public static int dragonCardsLeft = 11;
        public static int dwarfCardsLeft = 16;
        private string src;
        private static Dictionary<int, List<Action>> dragonActionsDictionary = new Dictionary<int, List<Action>>();
        private static Dictionary<int, List<Action>> dwarfsActionsDictionary = new Dictionary<int, List<Action>>();

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [DataMember]
        public Fraction Fraction
        {
            get { return fraction; }
            set { fraction = value; }
        }
        [DataMember]
        public Action Action1
        {
            get { return action1; }
            set { action1 = value; }
        }
        [DataMember]
        public Action Action2
        {
            get { return action2; }
            set { action2 = value; }
        }

        [DataMember]
        public bool AreActionsInitialized
        {
            get;
            set;
        }
        [DataMember]
        public string Src
        {
            get { return src; }
            set { src = value; }
        }
        public Card()
        {

        }
        public Card(int i, Fraction f)
        {

            int n;
            if (f == Fraction.Dragon)
            {
                n = Card.dragonCards;
                action1 = dragonActionsDictionary[i][0];
                if (dragonActionsDictionary[i].Count > 1)
                    action2 = dragonActionsDictionary[i][1];
                else action2 = action1;
            }
            else
            {
                n = Card.dwarfCards;
                action1 = dwarfsActionsDictionary[i][0];
                if (dwarfsActionsDictionary[i].Count > 1)
                    action2 = dwarfsActionsDictionary[i][1];
                else action2 = action1;
            }
                
                
            if (i > 0 && i <= n)
            {
                id = i;
                fraction = f;
                string c;                
                if (fraction == Fraction.Dwarf)               
                    c = "K";               
                    else
                    c = "D";
                src = "Images/Cards/" + c + id.ToString()+".png";
            }
        }      
      

         public int SingleAttackActionValue()
        {

            if (action1.ActionType == ActionType.SINGLE_ATTACK)
                return action1.Value;
            if (action2.ActionType == ActionType.SINGLE_ATTACK)
                return action2.Value; 
            return 0;
        }
        public int DoubleAttackActionValue()
        {

            if (action1.ActionType == ActionType.DOUBLE_ATTACK)
                return action1.Value;
            if (action2.ActionType == ActionType.DOUBLE_ATTACK)
                return action2.Value;
            return 0;
        }
        public int SingleMoveActionValue()
        {

            if (action1.ActionType == ActionType.SINGLE_MOVE)
                return action1.Value;
            if (action2.ActionType == ActionType.SINGLE_MOVE)
                return action2.Value;
            return 0;
        }

        public int DoubleMoveActionValue()
        {

            if (action1.ActionType == ActionType.DOUBLE_MOVE)
                return action1.Value;
            if (action2.ActionType == ActionType.DOUBLE_MOVE)
                return action2.Value;
            return 0;
        }
        public int FlyActionValue()
        {

            if (action1.ActionType == ActionType.FLY)
                return 5;
            if (action2.ActionType == ActionType.FLY)
                return 5;
            return 0;
        }
        public int FireActionValue()
        {

            if (action1.ActionType == ActionType.FIRE)
                return action1.Value;
            if (action2.ActionType == ActionType.FIRE)
                return action2.Value;
            return 0;
        }
        public int BoltActionValue()
        {

            if (action1.ActionType == ActionType.BOLT)
                return action1.Value;
            if (action2.ActionType == ActionType.BOLT)
                return action2.Value;
            return 0;
        }

       
        public static  KeyValuePair<int, List<Action>>  AddAction(int id,Action a1)
        {
            KeyValuePair<int, List<Action>> result = new KeyValuePair<int, List<Action>>();
            List<Action> list = new List<Action>();
            list.Add(a1);
            result=new KeyValuePair<int,List<Action>>(id, list);
            return result;

        }
        public static KeyValuePair<int, List<Action>> AddAction(int id, Action a1, Action a2)
        {
            KeyValuePair<int, List<Action>> result = new KeyValuePair<int, List<Action>>();
            List<Action> list = new List<Action>();
            list.Add(a1);
            list.Add(a2);
            result = new KeyValuePair<int, List<Action>>(id, list);
            return result;

        }

        public static void InitiateActionsDictionary()
        {
            KeyValuePair<int, List<Action>> list = new KeyValuePair<int, List<Action>>();
            if (!areActionsInitialized)
            {
                list = AddAction(1, new Action(ActionType.FIRE, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(2, new Action(ActionType.SINGLE_ATTACK, 3));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(3, new Action(ActionType.FLY, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(4, new Action(ActionType.BLOCK, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(5, new Action(ActionType.SINGLE_ATTACK, 2), new Action(ActionType.SINGLE_MOVE, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(6, new Action(ActionType.SINGLE_MOVE, 2));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(7, new Action(ActionType.SINGLE_MOVE, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(8, new Action(ActionType.FIRE, 2));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(9, new Action(ActionType.SINGLE_ATTACK, 2), new Action(ActionType.SINGLE_MOVE, 2));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(10, new Action(ActionType.SINGLE_MOVE, 1), new Action(ActionType.BLOCK, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(11, new Action(ActionType.SINGLE_ATTACK, 1), new Action(ActionType.SINGLE_MOVE, 2));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(1, new Action(ActionType.SINGLE_MOVE, 1), new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(2, new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(3, new Action(ActionType.DOUBLE_MOVE, 1), new Action(ActionType.BOLT, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(4, new Action(ActionType.SINGLE_MOVE, 2), new Action(ActionType.BOLT, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(5, new Action(ActionType.BOLT, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(6, new Action(ActionType.DOUBLE_MOVE, 1), new Action(ActionType.WEB, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(7, new Action(ActionType.DOUBLE_MOVE, 1), new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(8, new Action(ActionType.SINGLE_MOVE, 2), new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(9, new Action(ActionType.WEB, 1), new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(10, new Action(ActionType.SINGLE_MOVE, 1), new Action(ActionType.SINGLE_ATTACK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(11, new Action(ActionType.DOUBLE_MOVE, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(12, new Action(ActionType.SINGLE_ATTACK, 2));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(13, new Action(ActionType.SINGLE_ATTACK, 1), new Action(ActionType.BOLT, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(14, new Action(ActionType.WEB, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(15, new Action(ActionType.SINGLE_ATTACK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(16, new Action(ActionType.DOUBLE_ATTACK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);
            }
            areActionsInitialized = true;
          }

        public static bool DrawCard(Player p, int amount)
        {

            if (p.Hand.Count + amount > 8) return false;
            Card c;
            int i;
            Random r = new Random((int)DateTime.Now.Ticks);
            for (int j = 0; j < amount; j++)
            {
                if (p.Library.Count == 0)
                {

                }
                else
                {
                    i = r.Next(p.Library.Count - 1);
                    c = p.Library[i];
                    p.Library.RemoveAt(i);
                    if (p.Fraction == Fraction.Dragon)
                        Card.dragonCardsLeft--;
                    else
                        Card.dwarfCardsLeft--;
                    p.Hand.Add(new Card(c.id, p.Fraction));
                }
            }
            p.RenderHand();
            return true;
        }
    }
}
