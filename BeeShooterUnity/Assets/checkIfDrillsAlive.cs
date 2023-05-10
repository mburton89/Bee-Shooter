using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfDrillsAlive : MonoBehaviour
{
   public GameObject drill1;
   public GameObject drill2;
   public GameObject drill3;
   public GameObject drill4;
   public GameObject claw1;
   public GameObject claw2;
   public GameObject claw3;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        areDrillsHere();
    }

   public void areDrillsHere()
    {
        if (drill1 == null && drill2 == null && drill3 == null && drill4 == null && claw2 == null && claw2 == null && claw3 == null)
        {
            GameManager.Instance.winGame();
        }
    }
}
