using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects.Clear
{
    public class ReturnToTitle:MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)| Input.GetKey(KeyCode.Mouse0))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}