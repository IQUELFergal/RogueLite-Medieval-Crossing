using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public Text ValueText;
    // Start is called before the first frame update
    void Start()
    {
        ValueText.text = PersistantManager.Instance.Value.ToString();
    }

    public void GoToFirstScene()
    {
        SceneManager.LoadScene("main");
    }
}
