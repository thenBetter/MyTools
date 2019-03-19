using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 页码管理器 
/// 当前的页码和6相比，跳转是从跳转开始累加。
/// 点击左右键的时候，把页码跳转为6的整数倍。
/// </summary>

public class PageContentManger : MonoBehaviour
{
    public int maxPageIndex;            //最大页码

    public UIPage[] uiPageArray { get; set; }

    private int direction;

    public System.Action<int> OnUpdateIndex;

    public Button leftBtn;
    public Button rightBtn;

    public static PageContentManger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PageContentManger>();
            }
            return instance;
        }
    }
    private static PageContentManger instance;

    private void Awake()
    {
        uiPageArray = this.GetComponentsInChildren<UIPage>();
        PageIndex = 1;
    }

    public void InitMax(int count)
    {
        maxPageIndex = count;
        leftBtn.interactable = false;
        InitMaxPageNumber();
        ActivatUIPageImage(uiPageArray[0].gameObject);
    }

    /// <summary>
    /// 在程序初始化出入最大页码的时候初始化
    /// </summary>
    private void InitMaxPageNumber()
    {
        int headPageIndex = 1;

        int n_pageHeadIndex = headPageIndex;

        //页数大于UIPage数（大于6）
        if (maxPageIndex > uiPageArray.Length)
        {
            foreach (var item in uiPageArray)
            {
                item.gameObject.SetActive(true);
                if (headPageIndex - n_pageHeadIndex == uiPageArray.Length - 1)
                    item.transform.GetChild(1).GetComponent<Text>().text = "...";
                else
                    item.transform.GetChild(1).GetComponent<Text>().text = headPageIndex.ToString();

                item.gameObject.SetActive(true);
                headPageIndex++;
            }
        }
        //页数等于UIPage数
        else if (maxPageIndex == uiPageArray.Length)
        {
            foreach (var item in uiPageArray)
            {
                item.transform.GetChild(1).GetComponent<Text>().text = headPageIndex.ToString();
                item.gameObject.SetActive(true);
                headPageIndex++;
            }
        }
        else
        {
            for (int i = 0; i < maxPageIndex; i++)
            {
                uiPageArray[i].transform.GetChild(1).GetComponent<Text>().text = headPageIndex.ToString();
                uiPageArray[i].gameObject.SetActive(true);
                headPageIndex++;
            }
            for (int i = maxPageIndex; i <= uiPageArray.Length - 1; i++)
                uiPageArray[i].gameObject.SetActive(false);
        }

    }

    //根据跳转页码刷新
    public void UpdateJumpNumber(string number)
    {
        UpdatePageByNumber(int.Parse(number), true);

        UIPage uiPage = PageContentManger.Instance.FindUIPageWithText(number);

        if (uiPage)
            PageContentManger.Instance.ActivatUIPageImage(uiPage.gameObject);
    }

    /// <summary>
    /// 在点击左右键的时候刷新页码
    /// </summary>
    private void UpdatePageIndexByButton(string number)
    {
        if (maxPageIndex <= 6) return;
        int updateNumber = 0;
        int pageIndex = int.Parse(number);
        int mod = direction > 0 ? (pageIndex - 1) % 6 : pageIndex % 6;    //判断当前页码也6余的数

        if (mod == 0)     //为6的倍数
        {
            if (direction > 0)
            {
                if (maxPageIndex <= 6) return;
                UpdatePageByNumber(pageIndex, true);
                return;
            }
            else
                updateNumber = pageIndex <= 6 ? 6 : pageIndex;
        }
        else
        {
            if (direction > 0)
            {
                updateNumber = pageIndex - mod + 5;
                if (updateNumber >= maxPageIndex) return;
            }
            else
            {
                updateNumber = pageIndex - mod + 6;
                if (updateNumber > maxPageIndex)
                {
                    UpdatePageByNumber(updateNumber - 5, true);
                    return;
                }
            }

        }
        UpdatePageByNumber(updateNumber, false);
    }

    /// <summary>
    /// 根据页码刷新
    /// </summary>
    /// <param name="number">页码</param>
    /// <param name="state">是正序还是倒序</param>
    private void UpdatePageByNumber(int number, bool state)
    {
        if (state) OrderByUp(number);
        else OrderByDown(number);
    }

    /// <summary>
    /// 正序显示
    /// </summary>
    /// <param name="number">页码</param>
    private void OrderByUp(int number)
    {
        for (int i = 0; i < uiPageArray.Length; i++)
        {
            uiPageArray[i].transform.GetChild(1).GetComponent<Text>().text = number.ToString();
            number++;
            if (i == uiPageArray.Length - 1)
                uiPageArray[i].transform.GetChild(1).GetComponent<Text>().text = "...";

            if (number > maxPageIndex + 1 && i < uiPageArray.Length)
                uiPageArray[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 倒序显示
    /// </summary>
    /// <param name="number">页码</param>
    private void OrderByDown(int number)
    {
        for (int i = uiPageArray.Length - 1; i >= 0; i--)
        {
            if (!uiPageArray[i].gameObject.activeSelf)
                uiPageArray[i].gameObject.SetActive(true);

            if (i == uiPageArray.Length - 1)
                uiPageArray[i].transform.GetChild(1).GetComponent<Text>().text = "...";
            else
            {
                uiPageArray[i].transform.GetChild(1).GetComponent<Text>().text = number.ToString();
                number--;
            }
        }
    }

    public int PageIndex
    {
        get { return pageIndex; }
        private set { pageIndex = value; }
    }
    private int pageIndex;

    //需要和服务器交互TODO
    public void ActivatUIPageImage(GameObject uiPage)
    {
        //将全部uiPage的Image取消激活
        foreach (var item in uiPageArray)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }

        uiPage.transform.GetChild(0).gameObject.SetActive(true);
        pageIndex = int.Parse(uiPage.GetComponent<UIPage>().GetText.text);

        leftBtn.interactable = pageIndex > 1;
        rightBtn.interactable = pageIndex < maxPageIndex;

        if (OnUpdateIndex != null)
        {
            OnUpdateIndex(pageIndex);
        }
    }

    /// <summary>
    /// 按文本内容查询
    /// </summary>
    /// <param name="text"></param>
    public UIPage FindUIPageWithText(string text)
    {
        foreach (var item in uiPageArray)
        {
            if (item.GetText.text == text)
            {
                return item;
            }
        }

        return null;
    }

    private UIPage FindUIPageWithImage()
    {
        foreach (var item in uiPageArray)
        {
            if (item.GetImage.gameObject.activeSelf)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 跳转
    /// </summary>
    /// <param name="str"></param>
    public void JumpPage(string str)
    {
        //获取输入信息
        string number = str;
        UpdatePageIndexByButton(number);
        UIPage uiPage = PageContentManger.Instance.FindUIPageWithText(number);
        if (uiPage)
            PageContentManger.Instance.ActivatUIPageImage(uiPage.gameObject);
    }

    /// <summary>
    /// 页面选择按钮
    /// </summary>
    /// <param name="selection">（向左：-1）（ 向右：1）</param>
    public void OnBtnRight(string selection)
    {
        UIPage uiPage = PageContentManger.Instance.FindUIPageWithImage();

        if (uiPage)
        {
            //当前页面是最后一页或者是第一页
            if (int.Parse(uiPage.GetText.text) == maxPageIndex && selection == "1" || int.Parse(uiPage.GetText.text) == 1 && selection == "-1")
            {
                leftBtn.interactable = selection.Equals("1") ? true : false;
                rightBtn.interactable = selection.Equals("-1") ? true : false;
                return;
            }
            else
            {
                //跳转页面
                direction = int.Parse(selection);
                string value = (int.Parse(uiPage.transform.GetChild(1).GetComponent<Text>().text) + int.Parse(selection)).ToString();
                int index = int.Parse(value);
                leftBtn.interactable = index > 1;
                rightBtn.interactable = index < maxPageIndex;
                JumpPage(value);
            }
        }
    }
}
