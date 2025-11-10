using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private bool AlreadyClick = false;
    public bool IsTitleScreen = true;
    public bool IsPauseMenu = false;
    public bool IsWin = false;

    public int SnackCounts;
    public int StartColliderCounts;

    [SerializeField] UiManager uiManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SnackCounts = chercher le collectible et la lenght
        SnackCounts = GameObject.FindGameObjectsWithTag("Snacks").Length;
        StartColliderCounts = GameObject.FindGameObjectsWithTag("StartCollider").Length;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(StartColliderCounts == 0 && AlreadyClick == false)
        {
            ActivateClickScreen();  
            AlreadyClick = true;
        }
        
        if(IsTitleScreen && Input.anyKeyDown && StartColliderCounts ==0)
        {
            IsTitleScreen = false;
            DestroyColliderCanvas();
            DesactivateClickScreen();
            Invoke("DeleteTitleScreen", 3);
        }

        if(IsTitleScreen == false && IsPauseMenu == false && Input.GetKeyDown(KeyCode.Escape))
        {
            ActivatePauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            DesactivatePauseMenu();

        }

        if (SnackCounts == 0) 
        {
            ActivateWinScreen();

        }

    }

    public void DeleteTitleScreen()
    {
        uiManager.Disappear(uiManager.TitleCanvas);
    }

    void ActivatePauseMenu()
    {
        
        uiManager.Appear(uiManager.PauseCanvas);
        IsPauseMenu = true;
    }

    public void DesactivatePauseMenu()
    {
        if (IsPauseMenu)
        {
            uiManager.Disappear(uiManager.PauseCanvas);
            IsPauseMenu = false;

        }
    }

    public void ActivateWinScreen()
    {
        uiManager.Appear(uiManager.WinCanvas);
        IsWin = true;
    }

    void DesactivateClickScreen()
    {
        uiManager.Disappear(uiManager.ClickCanvas);
        
    }

    void ActivateClickScreen()
    {
        uiManager.Appear(uiManager.ClickCanvas);
    }


    public void QuitGame()
    {
        if (IsPauseMenu)
        {
            Application.Quit();
        }

    }

    public void RestartGame()
    {
        if (IsWin)
        {
            SceneManager.LoadScene("TPLampinScene");
        }
    }

    public void QuitGameOnWin()
    {
        if (IsWin)
        {
            Application.Quit();
        }
    }

    public void DecreaseColliderCount()
    {
        StartColliderCounts--;
    }

    void DestroyColliderCanvas()
    {
        Destroy(uiManager.ColliderCanvas);
    }

}
