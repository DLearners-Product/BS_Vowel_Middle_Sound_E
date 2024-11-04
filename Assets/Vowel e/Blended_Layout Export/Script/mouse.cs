using UnityEngine;

using UnityEngine.EventSystems;



public class mouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
	//public string name;

	Vector2 startscale;
	public void Start()

	{
		startscale = this.gameObject.transform.localScale;
		name = this.gameObject.name;


	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log(this.name);

		{
			
			this.gameObject.transform.localScale = new Vector2(1.1f, 1.1f);
			if (this.gameObject.GetComponent<Animator>() != null)
			{
				this.gameObject.GetComponent<Animator>().SetInteger("cond", 1);
			}
			
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("exit");
		this.gameObject.transform.localScale = new Vector2(1, 1);
		if (this.gameObject.GetComponent<Animator>() != null)
		{
			this.gameObject.GetComponent<Animator>().SetInteger("cond", 0);
		}
	}
}