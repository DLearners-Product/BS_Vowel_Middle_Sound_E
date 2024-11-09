using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetsFindOutController : MonoBehaviour
{
    public Sprite[] sprites;
    Stack<Sprite> _sprites;

    void Awake()
    {
        _sprites = new Stack<Sprite>(sprites);
    }

    public Sprite GetSprite()
    {
        _sprites.Push(_sprites.Peek());
        return _sprites.Pop();
    }

    public void Demo()
    {
        Debug.Log("WPPPp");
    }
}
