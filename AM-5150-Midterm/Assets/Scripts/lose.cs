using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class lose : MonoBehaviour
{
 
    void Update()
    {
        //mouse click to restart
        if (Input.GetMouseButtonDown(0))
        { SceneManager.LoadScene("Scenes/Main"); } } }