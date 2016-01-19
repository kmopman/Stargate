using UnityEngine;
using System.Collections;

public class HideCursor : MonoBehaviour {


	void Update ()
    {
        /*
         * 
         * 
         * 
         * 
         * Here it comes...
         * 
         * 
         * 
         */

        HideTheCursor();
	}

    void HideTheCursor()
    {
        Cursor.visible = false; // In case you didn't know, this hides the cursor.
    }
}
