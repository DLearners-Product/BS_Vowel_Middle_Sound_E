using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    GameObject parentObj;
    public GameObject framePrefab;
    public GameObject[] spawnPoints;
    float timer = 0f;
    float waitTime = 3f;
    public List<GameObject> instantiatedObjs = new List<GameObject>();
    public LetsFindOutController _obj;
    int spawnCount = 0;

    void Start()
    {
        SpawnObjects();
        // StartCoroutine(StartQuesChange());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            ChangeQuestion();
            timer = 0f;
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < spawnPoints.Length - 1; i++)
        {
            var instantiatedObj = Instantiate(framePrefab, transform);
            instantiatedObj.transform.position = spawnPoints[i].transform.position;
            instantiatedObjs.Add(instantiatedObj);
            instantiatedObj.name += $"_{++spawnCount}";
            instantiatedObj.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = _obj.GetSprite();
            Debug.Log($"Sprite Name :: {instantiatedObj.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite.name}");

            if(i == 2){
                Utilities.Instance.ScaleObject(instantiatedObj.transform);
            }
        }
    }

    // IEnumerator StartQuesChange()
    // {
    //     yield return new WaitForSeconds(2);
    // }

    void ChangeQuestion()
    {
        for (int i = 0; i < instantiatedObjs.Count; i++)
        {
            if(i == 1)
            {
                Utilities.Instance.ANIM_MoveWithScaleUp(instantiatedObjs[i].transform, spawnPoints[i + 1].transform.position);
            }else if(i == 2)
            {
                Utilities.Instance.ANIM_MoveWithScaleDown(instantiatedObjs[i].transform, spawnPoints[i + 1].transform.position);
            }else{
                Utilities.Instance.ANIM_Move(instantiatedObjs[i].transform, spawnPoints[i + 1].transform.position);
            }
        }

        StartCoroutine(OrganiseQuestions());
    }

    IEnumerator OrganiseQuestions()
    {
        yield return new WaitForSeconds(1f);
        Destroy(instantiatedObjs[instantiatedObjs.Count - 1]);
        instantiatedObjs.RemoveAt(instantiatedObjs.Count - 1);

        var instantiatedObj = Instantiate(framePrefab, transform);
        instantiatedObj.name += $"_{++spawnCount}";
        instantiatedObj.transform.position = spawnPoints[0].transform.position;
        instantiatedObjs.Insert(0,instantiatedObj);

    }
}
