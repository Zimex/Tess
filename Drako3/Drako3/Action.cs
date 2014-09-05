using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Action
    {
        public ActionType actionType;
        public int value=0;

        public Action(ActionType t, int v)
        {
            actionType = t;
            value = v;
        }
        public Action(ActionType t)
        {
            actionType = t;
            
        }
    }
}
