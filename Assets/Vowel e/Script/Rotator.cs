using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
            var instantiatedObj = GetInstantiatedFrameObj();
            instantiatedObj.transform.position = spawnPoints[i].transform.position;
            instantiatedObjs.Add(instantiatedObj);

            if(i == 2){
                Utilities.Instance.ScaleObject(instantiatedObj.transform);
            }
        }
    }

    GameObject GetInstantiatedFrameObj()
    {
        var instantiatedObj = Instantiate(framePrefab, transform);

        instantiatedObj.name += $"_{++spawnCount}";
        instantiatedObj.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = _obj.GetSprite();
        instantiatedObj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(OnFrameClicked);

        return instantiatedObj;
    }

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

        var instantiatedObj = GetInstantiatedFrameObj();

        instantiatedObj.transform.position = spawnPoints[0].transform.position;
        instantiatedObjs.Insert(0,instantiatedObj);
    }

    public void OnFrameClicked()
    {
        var selectedObj = EventSystem.current.currentSelectedGameObject;
        Debug.Log($"Frame Buttonclicked.... {selectedObj.name}", selectedObj);

        if(_obj.EvaluateAnswer(selectedObj.transform.parent.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite.name))
        {
            selectedObj.transform.parent.GetChild(3).gameObject.SetActive(true);
            selectedObj.transform.parent.GetChild(3).GetComponent<ParticleSystem>().Play();
            _obj.DisplayAnswer(selectedObj.transform.parent.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite);
        }
    }
}
