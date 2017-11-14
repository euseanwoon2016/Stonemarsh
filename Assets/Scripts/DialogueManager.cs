﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public GameObject dialogueBox;

    public GameObject player;

    public Animator animator;

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueBox = GameObject.Find("DialogueBox");
        animator = dialogueBox.GetComponent<Animator>();
        sentences = new Queue<string>();
	}
	

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    
    IEnumerator TypeSentence (string sentence)
    {

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        player.GetComponent<PlayerController>().playerMovement = true;
        player.GetComponent<PlayerController>().iText.SetActive(true);
    }
}
