using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    class ActionColorChange : Action
    {
        bool Blue = true;
        public override void DoAction()
        {
            base.DoAction();
            this.gameObject.GetComponent<SpriteRenderer>().color = Blue ? Color.green : Color.blue;
            Blue = !Blue;
        }
    }
}
