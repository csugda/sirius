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

        private void Awake()
        {
            anim = this.gameObject.GetComponent<Animator>();
            anim.SetBool("isAttacking", false);
            attackTrigger.enabled = false;
        }

        public override void DoAction()
        {
                anim = this.gameObject.GetComponent<Animator>();
                base.DoAction();
                anim.SetBool("isAttacking", true);
                attackTrigger.enabled = true;        
            }
        }
    }

