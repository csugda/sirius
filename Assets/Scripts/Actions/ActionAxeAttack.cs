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
        private Animator anim;

        private new void Start()
        {
            base.Start();
            anim = this.gameObject.GetComponent<Animator>();
            attackTrigger.enabled = false;
        }

        public override void DoAction()
        {
            anim.SetTrigger("Attack");

            base.DoAction();
            attackTrigger.enabled = true;
        }
    }
}

