using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject InGameMainCanvus;
    [SerializeField] private GameObject ResultGameCanvus;

    public void UIInit()
    {
        InGameMainCanvus.SetActive(true);
        ResultGameCanvus.SetActive(false);
    }

    public void ResultUI()
    {
        InGameMainCanvus.SetActive(false);
        ResultGameCanvus.SetActive(true);
    }
}
