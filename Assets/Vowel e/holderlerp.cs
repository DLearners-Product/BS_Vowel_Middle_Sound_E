using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class holderlerp : MonoBehaviour
{
    Vector2 endpos;
    // Start is called before the first frame update
    void Start()
    {
        endpos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite!=null)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, endpos, 0.1f);
        }
    }
}
