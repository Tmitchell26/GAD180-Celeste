using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;

    public float fadeTime;

    public GameObject leftCharacter, rightCharacter;
    public Image lPortrait, rPortrait;
    public Text lName, rName;
    public Text dialogueText;

    public GameObject nextText;
    public UnityEvent DialougeEnd;


    private int lineIndex = 0;
    private bool canClick;
    
    // Start is called before the first frame update
    void Start()
    {
        FirstLine();
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canClick)
        {
            lineIndex++;
            StartCoroutine(NextLine());
        }

        if(canClick)
        {
            nextText.SetActive(true);
        }
        else
        {
            nextText.SetActive(false);
        }
    }

    private IEnumerator NextLine()
    {
        if (lineIndex > dialogue.lines.Length - 1)
        {
            DialougeEnd.Invoke();
            yield break;
        }

        canClick = false;

        //fade out all elements
        float fadeAmount = 1f;
        while(dialogueText.color.a > 0)
        {
            dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, fadeAmount);
            lPortrait.color = new Color(lPortrait.color.r, lPortrait.color.g, lPortrait.color.b, fadeAmount);
            rPortrait.color = new Color(rPortrait.color.r, rPortrait.color.g, rPortrait.color.b, fadeAmount);
            fadeAmount -= 0.01f;
            yield return new WaitForSeconds(fadeTime / 100);
        }

        leftCharacter.SetActive(false);
        rightCharacter.SetActive(false);

        //set text and stuff
        if (dialogue.lines[lineIndex].leftChar)
        {
            leftCharacter.SetActive(true);
            lPortrait.sprite = dialogue.lines[lineIndex].portrait;
            lName.text = dialogue.lines[lineIndex].name;
        }
        else
        {
            rightCharacter.SetActive(true);
            rPortrait.sprite = dialogue.lines[lineIndex].portrait;
            rName.text = dialogue.lines[lineIndex].name;
        }

        dialogueText.text = dialogue.lines[lineIndex].text;

        //fade in the text
        fadeAmount = 0f;
        while (dialogueText.color.a < 1)
        {
            dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, fadeAmount);
            lPortrait.color = new Color(lPortrait.color.r, lPortrait.color.g, lPortrait.color.b, fadeAmount);
            rPortrait.color = new Color(rPortrait.color.r, rPortrait.color.g, rPortrait.color.b, fadeAmount);
            fadeAmount += 0.01f;
            yield return new WaitForSeconds(fadeTime / 100);
        }

        canClick = true;
    }

    private void FirstLine()
    {
        //set text and stuff
        if (dialogue.lines[lineIndex].leftChar)
        {
            leftCharacter.SetActive(true);
            lPortrait.sprite = dialogue.lines[lineIndex].portrait;
            lName.text = dialogue.lines[lineIndex].name;
        }
        else
        {
            rightCharacter.SetActive(true);
            rPortrait.sprite = dialogue.lines[lineIndex].portrait;
            rName.text = dialogue.lines[lineIndex].name;
        }

        dialogueText.text = dialogue.lines[lineIndex].text;
    }
}
