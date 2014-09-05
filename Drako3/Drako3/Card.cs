using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Drako3
{
    public class Card
    {
        public int id;
        private static bool areActionsInitialized = false;
        public Fraction fraction;
        public Action action1=null;
        public Action action2 = null;
        public static int dragonCards=11;
        public static int dwarfCards = 16;
        public static int dragonCardsLeft = 11;
        public static int dwarfCardsLeft = 16;
        public  string src;
        public static Dictionary<int, List<Action>> dragonActionsDictionary=new Dictionary<int,List<Action>>();
        public static Dictionary<int, List<Action>> dwarfsActionsDictionary=new Dictionary<int,List<Action>>();

        public bool AreActionsInitialized
        {
            get;
            set;
        }
        public Card(int i,Fraction f)
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
               // src = new BitmapImage();
                string c;
                
                if (fraction == Fraction.Dwarf)
                
                    c = "K";
                
                    else
                    c = "D";
                //if (i < 10) c += 0;
                //src.UriSource = new Uri("Images/Cards/"+c+id.ToString(), UriKind.Relative);
                src = "Images/Cards/" + c + id.ToString()+".png";
            }

        }
      
        public int SingleMoveActionValue()
        {

            if (action1.actionType == ActionType.SINGLE_MOVE)
                return action1.value;
            if (action2.actionType == ActionType.SINGLE_MOVE)
                return action2.value;
            return 0;
        }
        public int DoubleMoveActionValue()
        {

            if (action1.actionType == ActionType.DOUBLE_MOVE)
                return action1.value;
            if (action2.actionType == ActionType.DOUBLE_MOVE)
                return action2.value;
            return 0;
        }
        public int FlyActionValue()
        {

            if (action1.actionType == ActionType.FLY)
                return 5;
            if (action2.actionType == ActionType.FLY)
                return 5;
            return 0;
        }

        public int SingleAttackActionValue()
        {

            if (action1.actionType == ActionType.SINGLE_ATTACK)
                return action1.value;
            if (action2.actionType == ActionType.SINGLE_ATTACK)
                return action2.value; 
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
           
          //  l.Add()
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
                /*
            dragonActionsDictionary.Add(AddAction(1, new Action(ActionType.FIRE, 1)).Key,AddAction(1, new Action(ActionType.FIRE, 1)).Value);
            dragonActionsDictionary.Add(AddAction(2, new Action(ActionType.SINGLE_ATTACK, 3)).Key,AddAction(2, new Action(ActionType.SINGLE_ATTACK, 3)).Value);
            dragonActionsDictionary.Add(AddAction(3, new Action(ActionType.FLY, 1)).Key,AddAction(3, new Action(ActionType.FLY, 1)).Value);
            dragonActionsDictionary.Add(AddAction(4, new Action(ActionType.BLOCK, 1)).Key,AddAction(4,new Action(ActionType.BLOCK, 1)).Value);
            dragonActionsDictionary.Add(AddAction(5, new Action(ActionType.SINGLE_ATTACK, 2),new Action(ActionType.SINGLE_MOVE,1))
            dragonActionsDictionary.Add(AddAction(6, new Action(ActionType.SINGLE_MOVE, 2)).Key
            dragonActionsDictionary.Add(AddAction(7, new Action(ActionType.SINGLE_MOVE, 1)).Key
            dragonActionsDictionary.Add(AddAction(8, new Action(ActionType.FIRE, 2)).Key
            dragonActionsDictionary.Add(AddAction(9, new Action(ActionType.SINGLE_ATTACK, 2),new Action(ActionType.SINGLE_MOVE,2)).Key
            dragonActionsDictionary.Add(AddAction(10, new Action(ActionType.SINGLE_ATTACK, 1),new Action(ActionType.BLOCK,1)).Key
            dragonActionsDictionary.Add(AddAction(11, new Action(ActionType.SINGLE_ATTACK, 1),new Action(ActionType.SINGLE_MOVE,2)).Key
        */
          }

        public static bool DrawCard(Player p, int amount)
        {

            if (p.hand.Count + amount > 8) return false;
            Card c;
            int i;
            Random r = new Random((int)DateTime.Now.Ticks);
            for (int j = 0; j < amount; j++)
            {
                if (p.library.Count == 0)
                {

                }
                else
                {
                    i = r.Next(p.library.Count - 1);
                    c = p.library[i];
                    p.library.RemoveAt(i);
                    if (p.fraction == Fraction.Dragon)
                        Card.dragonCardsLeft--;
                    else
                        Card.dwarfCardsLeft--;
                    p.hand.Add(new Card(c.id, p.fraction));
                }
            }

            p.RenderHand();

            return true;
        }
    }
}
