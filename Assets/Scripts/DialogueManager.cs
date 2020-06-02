using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialogueWindow;

    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialogueWindows;

    private int count;

    public Animator animSprite;
    public Animator animDialogueWindow;

    public string typeSound;
    public string enterSound;

    private AudioManager theAudio;
    private OrderManager theOrder;

    public bool talking = false;
    private bool keyActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        text.text = "";
        listSentences = new List<string>();
        listSprites = new List<Sprite>();
        listDialogueWindows = new List<Sprite>();
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        talking = true;

        theOrder.NotMove();

        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]);
            listSprites.Add(dialogue.sprites[i]);
            listDialogueWindows.Add(dialogue.dialogueWindows[i]);
        }

        animSprite.SetBool("Appear", true);
        animDialogueWindow.SetBool("Appear", true);
        StartCoroutine(StartDialogueCoroutine());
    }

    public void ExitDialogue()
    {
        text.text = "";
        count = 0;
        listSentences.Clear();
        listSprites.Clear();
        listDialogueWindows.Clear();
        animSprite.SetBool("Appear", false);
        animDialogueWindow.SetBool("Appear", false);
        talking = false;
        theOrder.Move();
    }

    IEnumerator StartDialogueCoroutine()
    {
        if(count > 0)
        {
            if(listDialogueWindows[count] != listDialogueWindows[count - 1])
            {
                animDialogueWindow.SetBool("Appear", false);
                yield return new WaitForSeconds(0.2f);
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                animDialogueWindow.SetBool("Appear", true);
            }
            else
            {
                if(listSprites[count] != listSprites[count - 1])
                {
                    animDialogueWindow.SetBool("Appear", false);
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                    animDialogueWindow.SetBool("Appear", true);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else
        {
            rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
        }

        keyActivated = true;
        for(int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i];
            if(i % 7 == 1)
            {
                theAudio.Play(typeSound);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                count++;
                text.text = "";
                theAudio.Play(enterSound);

                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
        
    }
}
