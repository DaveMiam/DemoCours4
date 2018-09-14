using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    [SerializeField] public GameObject dialogPrefab;
    [SerializeField] public GameObject mainCanvas;

    private bool actionAxisInUse = true;
    private GameObject player;
    private bool dialogInitiated = false;
    private DialogText currentDialog;
    private DialogDisplayer currentdialogDisplayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        ProcessInput();

    }

    public void StartDialog(DialogText newDialog)
    {
        dialogInitiated = true;
        player.GetComponent<PlayerMovement>().DisableControl();
        currentDialog = newDialog;
        GameObject currentDialogObject = Instantiate(dialogPrefab, mainCanvas.transform);
        currentdialogDisplayer = currentDialogObject.GetComponent<DialogDisplayer>();
        currentdialogDisplayer.SetDialogText(currentDialog.GetDialogText());
    }

    public void ProcessInput()
    {
        if (ShouldProccessInput())
        {
            actionAxisInUse = true;
            if(currentDialog.IsNextDialog())
            {
                currentDialog = currentDialog.GetNextDialog();
                currentdialogDisplayer.SetDialogText(currentDialog.GetDialogText());
            }
            else
            {
                EndDialog();
            }
            
        }
        ValideAxisInUse();
    }

    public void EndDialog()
    {
        dialogInitiated = false;
        currentdialogDisplayer.CloseDialog();
        player.GetComponent<PlayerMovement>().EnableControl();
        currentDialog = null;

    }

    private bool ShouldProccessInput()
    {
        if(dialogInitiated)
        {
            if (!actionAxisInUse &&  Input.GetAxis("Jump") != 0)
            {
                return true;
            }

        }
        return false;
    }

    private void ValideAxisInUse()
    {
        if (Input.GetAxis("Jump") != 0)
        {
            actionAxisInUse = true;
        }
        else
        {
            actionAxisInUse = false;
        }
    }
}
