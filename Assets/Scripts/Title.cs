using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject highScore;
    [SerializeField] GameObject title;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject currentScore;
    [SerializeField] GameObject player;
    public void StartBtn()
    {
        highScore.SetActive(false);
        currentScore.SetActive(true);
        title.SetActive(false);
        Instantiate(player).transform.position = Vector3.zero;
    }
}
