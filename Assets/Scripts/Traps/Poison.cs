using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class Poison : Trap
    {
        protected Animator dropletAnimator;
        protected new void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            script = new Dictionary<string, MonoBehaviour>();
            foreach (MonoBehaviour s in GetComponents<MonoBehaviour>())
            {
                script[s.name] = s;
            }
            colliders = GetComponents<Collider2D>();

        }
    }
}
