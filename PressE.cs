using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Important!!! In order to Ellen will able to interact with GameObject that
//holds this script, turn Layer of the GameObject to "Environment"

public class PressE : MonoBehaviour
{

    public GameObject uiObject;

    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
    }

    // trigger is called 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Ellen")
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

        IEnumerator WaitForSec()
        {
            yield return new WaitForSeconds(5);
            uiObject.SetActive(false);
        }

   
    
}
