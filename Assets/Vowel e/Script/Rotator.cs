using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    GameObject parentObj;
    public GameObject framePrefab;
    public GameObject[] spawnPoints;

    void Start()
    {
        SpawnObjects();
    }

    void Update()
    {
        
    }

    void SpawnObjects()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var instantiatedObj = Instantiate(framePrefab, spawnPoints[i].transform);

            Utilities.Instance.ANIM_ShowBounceNormal(instantiatedObj.transform);
            if(i == 1){
                Utilities.Instance.ScaleObject(instantiatedObj.transform);
            }
            // instantiatedObj.GetComponent<RectTransform>().anchorMin = spawnPoints[i].GetComponent<RectTransform>().anchorMin;
            // instantiatedObj.GetComponent<RectTransform>().anchorMax = spawnPoints[i].GetComponent<RectTransform>().anchorMax;
        }
    }

    void MvoeObjects()
    {
        
    }
}
