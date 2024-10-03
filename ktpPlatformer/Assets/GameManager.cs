using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> groundTiles;
    [SerializeField] private Controler playerController;
    [SerializeField] private GameObject victoryScreen; 

    private void Start()
    {
        victoryScreen.SetActive(false);
    }

    public void CheckTilesColor()
    {
        for (int i = 0; i < groundTiles.Count; i++)
        {
            if (groundTiles[i].color != playerController.accentColor)
            {
                return;
            }
        }
        
        OnAllTilesColored();
    }

    private void OnAllTilesColored()
    {
        victoryScreen.SetActive(true);
    }
}