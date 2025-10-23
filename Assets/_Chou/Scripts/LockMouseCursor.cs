using UnityEngine;

/// <summary>
/// ChatGPTより。
/// EditorのGameビューでマウスクリックするとフォーカスでき、ESCキーで戻ることができる。
/// </summary>
public class LockMouseCursor : MonoBehaviour
{
    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }

        if (Input.GetMouseButtonDown(0))
        {
            LockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
