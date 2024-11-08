using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Randomizer : MonoBehaviour
{
    public float movementSpeed;
    public MovementDirection _movementDirection;
    public float stopDistance = 1.25f;
    List<GameObject> childLetters = new List<GameObject>();
    delegate void CallFunc();
    CallFunc updateDelegate;
    float lerpDuration = 1;
    float timeElapsed = 0;
    GameObject _parentObj;
    int counter = 0;
    // public bool displayLog;

    void Start()
    {
        _parentObj = transform.parent.gameObject;
        GetChildItems();
    }

    void Update()
    {
        updateDelegate?.Invoke();
        // transform.position += ((_direction == MovementDirection.Up) ? Vector3.up : Vector3.down) * movementSpeed * Time.deltaTime;
    }

    void GetChildItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childLetters.Add(transform.GetChild(i).gameObject);
        }
    }

    public void SwitchObject()
    {
        counter++;
        updateDelegate += MoveObject;
    }

    void MoveObject() {
        transform.position += GetDirection(_movementDirection) * movementSpeed * Time.deltaTime;


        // if(displayLog)
        // {
        //     Debug.Log($"COUNTER :: {counter}");
        //     Debug.Log($"Distance == {Vector3.Distance(_parentObj.transform.position, childLetters[counter].transform.position)} {transform.parent.name}");
        // }

        if(Vector3.Distance(_parentObj.transform.position, childLetters[counter].transform.position) <= stopDistance) {
            updateDelegate -= MoveObject;
        }
    }

    Vector3 GetDirection(MovementDirection mD) {
        switch (mD)
        {
            case MovementDirection.Up:
                return Vector3.up;
            case MovementDirection.Down:
                return Vector3.down;
            case MovementDirection.Left:
                return Vector3.left;
            case MovementDirection.Right:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }
}

public enum MovementDirection {
    Up,
    Down,
    Left,
    Right
}