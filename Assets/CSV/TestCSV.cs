using UnityEngine;

public class TestCSV : MonoBehaviour
{

    CSVManager csvManager ;
    private void Start()
    {     //q 是名字,QQ 是 Resourse路径
        csvManager = new CSVManager("QQ");
    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect  (0,0,100,30),"读表"))
        {
            string tempStr= csvManager.ReadData("A1");
            print(tempStr);
        }
        if (GUI.Button(new Rect(0, 30, 100, 30), "写表"))
        {
            csvManager.WriteData("E1", "Test");//写入临时表，不修改原表
        }
        if (GUI.Button(new Rect(0, 60, 100, 30), "创建表"))
        {
            csvManager.WriteDataToFile("E1", "Test");//写入临时表，修改原表，生成目录在 StreamingAssets/CSV/xx.csv
        }
    }
}