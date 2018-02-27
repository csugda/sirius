using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    class ActionAxeAttack : Action
    {
        //class variables
        public Collider2D attackTrigger;
        
        

        private void Awake()
        { 
            attackTrigger.enabled = false;
        }

        public override void DoAction()
        {
                base.DoAction();
                attackTrigger.enabled = true;        
            }
        }
    }

