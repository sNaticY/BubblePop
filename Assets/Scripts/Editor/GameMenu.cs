using UnityEditor;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [MenuItem("Game/Clear All Save Data")]
    private static void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
