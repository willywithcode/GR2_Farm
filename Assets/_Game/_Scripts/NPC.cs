using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject DialogPanel;
    [SerializeField] private TextMeshProUGUI DialogText;
    public string[] Dialogue;

    private int index = 0;

    private bool PlayerIsClose;
    public float wordSpeed;

    private void Start()
    {
        DialogText.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
        {
            if (!DialogPanel.activeInHierarchy)
            {
                DialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (DialogText.text.CompareTo(Dialogue[index]) ==0)
            {
                NextChat();
            }

        }
        if( Input.GetKeyDown(KeyCode.Q) && DialogPanel.activeInHierarchy)
        {
            ClearChat();
        }
    }


    public void ClearChat()
    {
        DialogText.text = "";
        index = 0;
        DialogPanel.SetActive(false);
    }

    public void NextChat()
    {
        if(index < Dialogue.Length-1) 
        {
            index++;
            DialogText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ClearChat();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerIsClose = false;
            ClearChat();
        }
    }

    IEnumerator Typing()
    {
        foreach(char letter in Dialogue[index].ToCharArray())
        {
            DialogText.text+= letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
}
