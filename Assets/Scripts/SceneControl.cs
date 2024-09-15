using UnityEngine;
using UnityEngine.SceneManagement;

namespace Henry
{
    public class SceneControl : MonoBehaviour
    {
        public void LoadScene(string scene)
        {
            // 載入指定的場景
            SceneManager.LoadScene(scene);
        }

        public void QuitGame() 
        {
            // 應用程式的關閉
            // Unity 跟網頁版部會有作用，要發布 PC 或手機板
            Application.Quit();
        }
    }
}
