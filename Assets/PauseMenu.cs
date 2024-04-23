using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.pauseMenuStateObject = this.gameObject;
        GameManager.instance.pauseMenuStateObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
