using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droprak : MonoBehaviour
{
    public bool B_matched;
 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.name== collision.gameObject.name)
        {
             
               B_matched = true; 
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.name == collision.gameObject.name)
        {
           
            B_matched = false;

        }
    }
}
