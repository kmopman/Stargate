using UnityEngine;
using System.Collections;

public class WinOrLose : MonoBehaviour {

	

    public void TryAgain()
    {
        Application.LoadLevel("Master");
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
