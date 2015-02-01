using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Drako3
{
    public enum ActionType
    {
        SINGLE_MOVE,
        DOUBLE_MOVE,
        FLY,
        FIRE,
        SINGLE_ATTACK,
        DOUBLE_ATTACK,
        BLOCK,
        WEB,
        BOLT
    }
    /// <summary>
    /// Klasa reprezentuje akcje znajdujące się na kartach
    /// </summary>
    public class Action
    {
        private ActionType actionType;
        private int value=0;
          
        public Action(ActionType t, int v)
        {
            actionType = t;
            value = v;
        }
          public Action()
        {

        }
          
        public Action(ActionType t)
        {
            actionType = t;
            
        }
        public ActionType ActionType
        {
        get{return actionType;}
            set{ actionType=value;}
        }
        public int Value
        {
            get { return this.value ; }
            set { this.value = value; }
        }
    }
}
