using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField]
    private int dotsNumber = 30;
    [SerializeField]
    private float dotSpacing = 0.2f;
    private float timeStamp;
    [SerializeField]
    private GameObject dotsParent;
    [SerializeField]
    private GameObject dotsPrefab;

    Transform[] dotsList;

    Vector2 pos;

    private void Start()
    {
        Hide();
        PrepareDots();
    }

    public void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotsPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;
        }
    }

    public void UpdateDots(Vector3 playerPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (playerPos.x + forceApplied.x * timeStamp);
            pos.y = (playerPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
