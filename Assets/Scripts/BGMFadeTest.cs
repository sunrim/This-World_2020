using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMFadeTest : MonoBehaviour
{
    BGMManager BGM;

    // Start is called before the first frame update
    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(abc());
    }

    IEnumerator abc()
    {
        BGM.FadeOutMusic();

        yield return new WaitForSeconds(3f);
    }
}