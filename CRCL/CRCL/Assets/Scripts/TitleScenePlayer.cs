using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class TitleScenePlayer : MonoBehaviour
    {
        public enum SCENE
        {
            NONE,
            START,
            EXIT,
            MANUAL,
        }

        private SCENE scene; 

        public SCENE STATE
        {
            get { return scene; }
        }

        // Use this for initialization
        void Start()
        {
            scene = SCENE.NONE;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.name == "StartCol")
            {
                scene = SCENE.START;
            }
            else if(other.name == "ExitCol")
            {
                scene = SCENE.EXIT;
            }
            else if (other.name == "ManualCol")
            {
                scene = SCENE.MANUAL;
            }
        }
    }
}
