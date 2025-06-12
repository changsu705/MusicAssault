using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        // �����Ϳ��� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���忡�� ����
        Application.Quit();
#endif
    }
}
