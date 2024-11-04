using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apples : MonoBehaviour
{
    public bool B_collected;
    float ypos;

    private void Start()
    {
        B_collected = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (B_collected == false)
        {
            if (collision.gameObject.name.Contains("basket"))// || collision.gameObject.name==this.gameObject.name)
            {
                Invoke("THI_Collected", 0.1f); 
                treeapple.OBJ_treeapple.B_lerp = false;
                ypos = this.gameObject.transform.position.y;
              //  treeapple.OBJ_treeapple.I_collection++;
                
            }
        }
    }
    public void THI_Collected()
    {
        B_collected = true;
        this.gameObject.GetComponent<Collider2D>().sharedMaterial = null;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;    
    }
    public void Update()
    {
        if(B_collected)
        {
            Vector3 tmpPos = this.transform.position;
            tmpPos.y = Mathf.Clamp(tmpPos.y, -5.0f, 5.0f);
            this.transform.position = tmpPos;
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, new Vector2(treeapple.OBJ_treeapple.G_basket.transform.position.x, ypos), 0.1f);
           
        }
    }

}
