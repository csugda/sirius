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

        private void Start()
        {
            anim = this.gameObject.GetComponent<Animator>();
            anim.SetBool("isAttacking", false);
            attackTrigger.enabled = false;
        }

        public override void DoAction()
        {
            anim.SetTrigger("Attack");

            base.DoAction();
            attackTrigger.enabled = true;
        }

        public void stopAnim()
        {
            anim.SetBool("isAttacking", false);
        }
    }
}

