using UnityEngine;

public class CSVImporterWithRotation : MonoBehaviour
{
    public GameObject globePointerPrefab; // プレハブを指定
    public string fileName = "Globe"; // CSVファイル名

    void Start()
    {
        // CSVファイルを読み込み、行ごとに分割する
        TextAsset csvData = Resources.Load<TextAsset>(fileName);
        string[] lines = csvData.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if(parts.Length < 8) continue; // フィールドが足りない行は無視

            string date = parts[1];
            string name = parts[2];
            string country = parts[3];
            string city = parts[4];
            float x = float.Parse(parts[5])/25;
            float y = float.Parse(parts[6])/25;
            float z = float.Parse(parts[7])/25;

            // オブジェクトを生成し、位置を設定
            GameObject cityObject = Instantiate(globePointerPrefab, new Vector3(x, y, z), Quaternion.identity);
            cityObject.name = name;

            // 円錐の頂点が原点を向くように回転させる
            Vector3 directionToOrigin = -cityObject.transform.position.normalized;
            Quaternion rotationToOrigin = Quaternion.LookRotation(directionToOrigin, Vector3.up);
            cityObject.transform.rotation = rotationToOrigin;

/*             // 北を示す突起が北極方向を指すように回転調整
            cityObject.transform.Rotate(0, 90, 0); // 必要に応じて角度を調整 */

            // テキストを設定
            GlobePopUp globePopUpComponent = cityObject.GetComponent<GlobePopUp>();
            if(globePopUpComponent != null)
            {
                globePopUpComponent.SetData($"{date}, {name},\n {country}, {city}");
            }
        }
    }
}
