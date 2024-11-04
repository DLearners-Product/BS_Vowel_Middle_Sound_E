using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockmove : MonoBehaviour
{
    public float F_moveSpeed;
    public Vector2 VEC2_startpos;
    int I_childCount;
    // Start is called before the first frame update
    void Start()
    {
         I_childCount = this.gameObject.transform.parent.childCount - 1;
        VEC2_startpos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * F_moveSpeed;// * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Limit")
        {
           this.gameObject.transform.position = new Vector2(this.gameObject.transform.parent.GetChild(I_childCount).transform.position.x-3f,this.gameObject.transform.position.y);
            this.gameObject.transform.SetAsLastSibling();
           //this.gameObject.transform.position = VEC2_startpos;
            
        }
       
    }
}
