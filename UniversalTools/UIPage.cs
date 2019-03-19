using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 具体的页码
/// </summary>
public class UIPage : MonoBehaviour
{
    public UnityEngine.UI.Image GetImage
    {
        get
        {
            if (image == null)
                image = transform.Find("Mask").GetComponent<UnityEngine.UI.Image>();
            return image;
        }
        set
        {
            image = value;
        }
    }
    private UnityEngine.UI.Image image;

    public Text GetText
    {
        get
        {
            if (text == null)
            {
                text = transform.Find("Text").GetComponent<Text>();
            }
            return text;
        }
        set
        {
            text = value;
        }
    }
    private Text text;

    private UIPage currentGame;

    private void Start()
    {
       UGUIEventTriggerListern.Get(gameObject).onClick = ClickBack;
    }

    private void ClickBack(GameObject go)
    {
        string text = this.transform.GetChild(1).GetComponent<Text>().text;
        UIPage uiPage = PageContentManger.Instance.FindUIPageWithText(text);
        currentGame = uiPage;
        if (text.Equals("..."))
        {
            GetImage.gameObject.SetActive(true);
            //UIManger.Instance.OpenPanel<JumpPanel>().SetPosition(Input.mousePosition);
            //UIManger.Instance.OpenPanel<JumpPanel>().Open(callback, PageContentManger.Instance.maxPageIndex);
            return;
        }

        if (uiPage)
            PageContentManger.Instance.ActivatUIPageImage(this.gameObject);
    }

    private void callback(int index)
    {
        if (index <= 0)
        {
            currentGame.GetImage.gameObject.SetActive(false);
            return;
        }
        PageContentManger.Instance.UpdateJumpNumber(index.ToString());
    }
}
