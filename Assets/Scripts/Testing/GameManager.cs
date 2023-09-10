using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound()
    {
        GameEffectManager.Instance.PlayEffect<SFXObject>("CloseShot", Vector3.zero);
        Debug.Log(EConstants.TEST_INT_C1);
    }

    public void SwapScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToTest()
    {
        SceneManager.LoadScene("Test");
    }

    public void TakeObj()
    {
        Pooling.Instance.GetObj(PrefabManager.Instance.GetReferenceType("DefaultObject"));        
    }
}
