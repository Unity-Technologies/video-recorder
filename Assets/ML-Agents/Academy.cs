using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLAgents
{
    public class Academy : MonoBehaviour
    {

       public int GetTotalStepCount(){
		   return 0;
	   }

       static Academy academy = null;

       public static Academy Instance { get { return academy; } }
    }
}