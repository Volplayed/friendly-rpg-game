using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //player amount panel
    public GameObject playerAmountPanel;

    //menu panel
    public GameObject menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //open player amount
    public void openPlayerAmount() {
        playerAmountPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    //close player amount
    public void closePlayerAmount() {
        playerAmountPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    
}
