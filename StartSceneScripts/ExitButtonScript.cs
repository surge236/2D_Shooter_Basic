using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour {

    private Button exitButton;

    // Use this for initialization
    void Start() {
        exitButton = this.GetComponent<Button>();
        exitButton.onClick.AddListener(onExitClick);
    }

    // Update is called once per frame
    void Update() {

    }

    void onExitClick() {
        Application.Quit();
    }

}