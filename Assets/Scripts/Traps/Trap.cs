using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Traps
{
    // TODO: Create inherited classes for different types of traps
    public class Trap : MonoBehaviour
    {
        protected Animator animator;
        protected SpriteRenderer spriteRenderer;
        protected Dictionary<string, MonoBehaviour> script;
        protected int trapType;
        [SerializeField] protected Collider2D[] colliders;

        
        private static int[] counter = { 0, 0, 0 };
        
        protected void Start()
        {
            
        }

        protected void Update()
        {
            
        }
        // TODO
        
        public static Trap Generate(GameObject pivot, int trapType, Vector3 transformPosition, Quaternion transformRotation, Vector3 transformScale)
        {
            String trapName = "";
            switch (trapType)
            {
                case 0:
                    trapName = "impaler/traps__1";
                    break;
                case 1:
                    trapName = "poison/traps__6";
                    break;
                case 2:
                    trapName = "trapdoor/traps__11";
                    break;
                default:
                    return null;
            }
 

            GameObject sample = pivot.transform.Find("TrapSamples/"+trapName).gameObject;

            GameObject g = Instantiate<GameObject>(sample, pivot.gameObject.transform);
            Trap trap = g.AddComponent<Trap>();
            g.transform.SetPositionAndRotation(transformPosition, transformRotation);
            g.transform.localScale = transformScale;
            trap.trapType = trapType;
            
            trap.animator = g.GetComponent<Animator>();
            trap.spriteRenderer = g.GetComponent <SpriteRenderer>();
            trap.colliders = g.GetComponents<Collider2D>();
            
            trap.script = new Dictionary<string, MonoBehaviour>();
            try
            {
                foreach (MonoBehaviour script in g.GetComponents<MonoBehaviour>())
                {
                    trap.script[script.name] = script;
                }
            }
            catch (Exception)
            {

            }
            g.name = "trap" + trapType + "-" + counter[trapType]++;
            
            return trap;
        }
    }
}