using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        // 에디터에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드에서 종료
        Application.Quit();
#endif
    }
}
