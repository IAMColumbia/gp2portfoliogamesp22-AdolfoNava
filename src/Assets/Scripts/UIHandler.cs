using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIHandler : MonoBehaviour
{
    public GameObject UI;
    public TMP_Text Health;
    public Player player;

    private void FixedUpdate()
    {
        Health.text = $"Health: {player.Health}";
    }
    public void SwitchUIDisplay()
    {
        if (UI.activeInHierarchy)
        {
            UI.SetActive(false);
        }
        else
        {
            UI.SetActive(true);
        }

    }
}
