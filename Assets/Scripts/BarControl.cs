using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarControl : MonoBehaviour
{

    public Slider progressBar;
    float maxVal = 100;
    public float currentval = 0;
    bool isInc = true; //yukarı mı aşağı mı gideceğimize karar veriyor 0 ise true oluyor ve yukarı cıkıyor, 100 ise false oluyor ve aşağı iniyor
    bool defspeed = false; //durcak mı calıscak mı (true iken calısıyor false iken duruyor)
    bool isCliked = false; //bir kere bastığımızda fonksiyonları devamlı olarak cagırmayı engelliyor. tutukluk yapmasını engelliyor
    public bool speedef = false;

     private void Start()
     {
         StartCoroutine("UpdateRoutine");
     }

     IEnumerator UpdateRoutine()
     {
         while (true)
         {
             if (Input.GetMouseButton(0) && !isCliked) 
             {
                 isCliked = true;
                 if (!defspeed)
                 {
                     defspeed = true;
                     StartCoroutine("GucBarPlay");

                 }
                 else
                 {
                     speedef = true;
                     defspeed = false;
                     StopCoroutine("GucBarPlay");
                     StopCoroutine("UpdateRoutine");
                 }
             }
             if(isCliked && !Input.GetMouseButton(0))
             {
                 isCliked = false;
             }
             yield return null;
         }
     }

     IEnumerator GucBarPlay()
     {
         Debug.Log("progres bar artsın");
         while (true)
         {

            if (isInc)
             {
                 currentval++;
                 if (currentval == 100)
                 {
                     isInc = false;
                 }
             }

             else
             {
                 currentval--;
                 if (currentval == 0)
                 {
                     isInc = true;
                 }
             }
            progressBar.value = currentval; //progress barı güncelliyoruz

            yield return null;
         } 


    }

}
