using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokedes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("THI_des", 1f);
    }

   public void THI_des()
    {
        Destroy(this.gameObject);
    }
}
