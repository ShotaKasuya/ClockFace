using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects.Clear
{
    //MEMO: Entity,Logic,View のどれにも該当していないのは理由がある？
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