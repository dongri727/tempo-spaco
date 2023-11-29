using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    public GameObject panel; // PanelのGameObjectへの参照

    // 主画面のボタンが押されたときに呼び出されるメソッド
    public void ShowPanel()
    {
        panel.SetActive(true); // Panelを表示する
    }

    // PanelView内のボタンが押されたときに呼び出されるメソッド
    public void HidePanel()
    {
        panel.SetActive(false); // PanelViewを非表示にする
    }
}