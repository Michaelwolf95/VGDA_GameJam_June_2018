using UnityEngine;
using UnityEngine.SceneManagement;

namespace VGDA.UI
{
    public class LoadSceneOnClick :MonoBehaviour
    {
        public int SceneIndex = 0;
        public void LoadScene()
        {
            SceneManager.LoadScene(SceneIndex);
        }
        public void LoadScene(float delay)
        {
            Invoke("LoadScene", delay);//SceneManager.LoadScene(SceneIndex));
        }
    }
}