using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void Inventory()
    {
        SceneManager.LoadScene("Inventory");
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Unknown");
    }
}
