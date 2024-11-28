using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject levelUpPopup; // Assign the UI pop-up prefab in the inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Show the level-up popup
    public void ShowLevelUpPopup()
    {
        levelUpPopup.SetActive(true);
    }

    // Hide the level-up popup
    public void HideLevelUpPopup()
    {
        levelUpPopup.SetActive(false);
    }
    
    
    
}