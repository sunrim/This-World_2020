using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkOrCancel : MonoBehaviour
{
    private AudioManager theAudio;
    public string key_sound;
    public string enter_sound;
    public string cancel_sound;

    public GameObject up_Panel;
    public GameObject down_Panel;
    

    public bool activated;
    private bool keyInput;
    private bool result;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    public void Selected()
    {
        theAudio.Play(key_sound);
        result = !result;
        if (result)
        {
            up_Panel.gameObject.SetActive(false);
            down_Panel.gameObject.SetActive(true);
        }
        else
        {
            up_Panel.gameObject.SetActive(true);
            down_Panel.gameObject.SetActive(false);
        }
    }

    public void ShowTwoChoice()
    {
        activated = true;
        result = true;

        up_Panel.gameObject.SetActive(true);
        down_Panel.gameObject.SetActive(true);

        StartCoroutine(ShowTwoChoiceCoroutine());
    }

    public bool GetResult()
    {
        return result;
    }

    IEnumerator ShowTwoChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        keyInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(key_sound);
                result = !result;
                Selected();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(key_sound);
                result = !result;
                Selected();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(enter_sound);
                keyInput = false;
                activated = false;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                theAudio.Play(cancel_sound);
                keyInput = false;
                activated = false;
                result = false;
            }
        }
    }
}
