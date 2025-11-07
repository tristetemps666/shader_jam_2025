using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsTitleScreen = true;
    public bool IsPauseMenu = false;

    [SerializeField] UiManager uiManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(IsTitleScreen && Input.anyKeyDown)
        {
            IsTitleScreen = false;

            Invoke("DeleteTitleScreen", 3);
        }

        if(IsTitleScreen == false && IsPauseMenu == false && Input.GetKeyDown(KeyCode.Escape))
        {
            ActivatePauseMenu();
        }

        if (IsTitleScreen == false && IsPauseMenu == true && Input.GetKeyDown(KeyCode.Escape)) 
        {
            DesactivatePauseMenu();
        }

    }

    void DeleteTitleScreen()
    {
        uiManager.Disappear(uiManager.TitleCanvas);

    }

    void ActivatePauseMenu()
    {

    }

    void DesactivatePauseMenu()
    {

    }


}
