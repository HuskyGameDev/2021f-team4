using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox; // I don't know if we even need a text box for the dialog - can delete if necessary

    public TextMesh theText; // I don't know how to get this to work for using TMP and not normal text

    public TextAsset textFile;

    public string[] textLines;

    public int currentLine;

    public int endAtLine;

    public bool isActive;

    public CharacterMovement player;

    public bool stopPlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMovement>().;

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n')); // splitting up the lines of text into elements in array

        }

        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        
        }

        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return)) // press enter key to scroll through different lines of text
        {
            currentLine = currentLine + 1;
        }

        if (currentLine > endAtLine) // turns off text box at end of lines we want to see
        {
            DisableTextBox();
        }
    }

    public void EnableTextBox() 
    {
        textBox.SetActive(true);

        if (stopPlayerMovement) // stops movement when interacting 
        {
            player.canMove = false;
        }
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);

        player.canMove = true; // allows character to move again
    }
}
