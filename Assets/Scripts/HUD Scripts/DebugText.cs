using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DebugText : MonoBehaviour
{
    bool changed = false;
    public TextMeshProUGUI debugText;
    string debugState = "";

    private void Start()
    {
        // Start out with debug.Text hidden
        debugText.alpha = 0f;
        GameManager.Instance.DebugSelection = ""; // Reset debug selection
    }

    // Update the debug info on change
    void Update()
    {
        // If the debug input is different than the stored one, there was an input change
        changed = (debugText.text != GameManager.Instance.DebugSelection);
        CheckForDebugInput(changed);
        debugText.alpha -= 1.5f * Time.deltaTime;
    }

    private void CheckForDebugInput(bool changed)
    {
        if (changed)
        {
            debugText.alpha = 1f;
            print("Changed was set!");
            debugText.enabled = true;
            debugText.text = GameManager.Instance.DebugSelection;
            changed = false;
        }
    }

    private void HideDebugText()
    {
        debugText.enabled = false;
    }
}
