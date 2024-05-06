using UnityEngine;

namespace Source.Title.Entity
{
    public class WaitClickEntity
    {
        //Advise: もし、WaitKeyをboolからReactivePropertyにしたいなら、InputSystemの導入がオススメ（知ってたらごめんね！）
        public bool WaitKey => Input.GetKeyDown(KeyCode.Mouse0) | Input.GetKeyDown(KeyCode.Space);
    }
}