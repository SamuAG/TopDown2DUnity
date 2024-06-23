using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour, IInteractive
{
    [SerializeField, TextArea(1, 5)] private string[] sentences;
    [SerializeField] private float timeBetweenCharacters;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameManagerSO gM;
    private bool isTalking = false;
    private int currentSentenceIndex = -1;

    private NPC npc;
    private bool isItem = false;
    private bool destroyItem = false;

    public string[] Sentences { get => sentences; set => sentences = value; }
    public bool DestroyItem { get => destroyItem; set => destroyItem = value; }
    public event Action OnDialogueComplete;

    private void Start()
    {
        npc = GetComponent<NPC>();
        isItem = GetComponent<Item>() != null;
    }

    public void Interact()
    {
        if (!isItem) StartDialogue();
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        if (!isTalking)
        {
            NextSentence();

        }
        else
        {
            EndCurrentSentence();
        }
    }

    private void NextSentence()
    {
        currentSentenceIndex++;
        if(currentSentenceIndex >= sentences.Length)
        {
            EndDialogue();
        }
        else
        {
            StartCoroutine(WriteSentence());
        }
        
    }

    private void EndDialogue()
    {
        isTalking = false;
        if (npc) npc.CanMove = true;
        gM.Player.CanMove = true;
        currentSentenceIndex = -1;
        dialogueText.text = "";
        dialogueBox.SetActive(false);
        if (destroyItem) Destroy(gameObject);
        OnDialogueComplete?.Invoke();
    }

    private IEnumerator WriteSentence()
    {
        isTalking = true;
        if (npc) npc.CanMove = false;
        gM.Player.CanMove = false;
        dialogueText.text = "";
        char[] sentenceChars = sentences[currentSentenceIndex].ToCharArray();
        foreach(char character in sentenceChars)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
        isTalking = false;
    }

    private void EndCurrentSentence()
    {
        StopAllCoroutines();
        dialogueText.text = sentences[currentSentenceIndex];
        isTalking = false;
    }
}
