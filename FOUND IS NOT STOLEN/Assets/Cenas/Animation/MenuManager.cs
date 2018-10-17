using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
        Cursor.visible = true;
    }

  
    public void LoadSceneGame (string name)
    {

        SceneManager.LoadScene(name);
       
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
