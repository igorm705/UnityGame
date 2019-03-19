using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Important!!! In order to Ellen will able to interact with GameObject that
//holds this script, turn Layer of the GameObject to "Environment"

public class WaitingForE : MonoBehaviour
{
    //UI GameObject
    public GameObject uiObject;
   
    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        //create variable for script "PressE"
        var bc = this.GetComponent<PressKeyE>();

        if (other.gameObject.name == "Ellen")
        {
            uiObject.SetActive(true);
            bc.enabled = true;
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        uiObject.SetActive(false);
        var bc = this.GetComponent<PressKeyE>();
        bc.enabled = false;
    }

}
