/*
 * 版本1.0
 * 用于Excel读写，可以读
 * 问题文本配置文件放在：Resources/Config 目录下
 * 生成目录在 StreamingAssets/CSV/xx.csv
 * 写入可以临时写入，也可以写入后创建文件。
 * 刘佳编写2018-9-13
                       .::::.
                     .::::::::.
                    :::::::::::  美女保佑无Bug
                ..:::::::::::'
              '::::::::::::'
                .::::::::::
           '::::::::::::::..
                ..::::::::::::.
              ``::::::::::::::::
               ::::``:::::::::'        .:::.
              ::::'   ':::::'       .::::::::.
            .::::'      ::::     .:::::::'::::.
           .:::'       :::::  .:::::::::' ':::::.
          .::'        :::::.:::::::::'      ':::::.
         .::'         ::::::::::::::'         ``::::.
     ...:::           ::::::::::::'              ``::.
    ```` ':.          ':::::::::'                  ::::..
                       '.:::::'                    ':'````..
*/

using RedScarf.EasyCSV;
using System.IO;
using System.Text;
using UnityEngine;
public class CSVManager
{
    int row = 0;
    int column = 0;
    CsvTable table;
    string fileName;

    public CSVManager(string ResourcePathName)
    {
        fileName = ResourcePathName;
        string content = Resources.Load<TextAsset>("CSV/" + ResourcePathName).text;
        table = CsvHelper.Create("LJ", content);
    }
    public CSVManager(string content, string name)
    {
        fileName = name;
        table = CsvHelper.Create("LJ", content);
    }
    public string ReadData(string address)
    {
        StringToInt(address);
        return table.Read(row, column);
    }
    public void WriteData(string address, string value)
    {
        StringToInt(address);
        table.Write(row, column, value);
    }
    void StringToInt(string value)
    {
        char[] arrayChar = value.ToCharArray();
        StringBuilder tempBuilder = new StringBuilder(arrayChar.Length - 1);
        for (int i = 1; i < arrayChar.Length; i++)
        {
            tempBuilder.Append(arrayChar[i]);
        }
        byte[] arrayBytes = Encoding.ASCII.GetBytes(arrayChar);
        column = (arrayBytes[0]) - 65;
        row = int.Parse(tempBuilder.ToString()) - 1;
    }



    /// <summary>
    /// 写入表，然后再写入文件
    /// </summary>
    /// <param name="address"></param>
    /// <param name="value"></param>
    public void WriteDataToFile(string address, string value)
    {
        StringToInt(address);
        table.Write(row, column, value);
        WriteToFile();
    }
    /// <summary>
    /// IO写入文件
    /// </summary>
    void WriteToFile()
    {
        StringBuilder contentMes = new StringBuilder();
        FileStream file;
        for (int i = 0; i < table.m_RawDataList.Count; i++)
        {
            for (int j = 0; j < table.m_RawDataList[i].Count; j++)
            {
                if (j == table.m_RawDataList[i].Count - 1)
                    contentMes.Append(table.m_RawDataList[i][j] + "\n");
                else
                    contentMes.Append(table.m_RawDataList[i][j] + ",");
            }
        }
        string dir = Application.streamingAssetsPath + "/CSV/";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        if (File.Exists(dir + fileName + "csv"))
        {
            file = new FileStream(dir + fileName + ".csv", FileMode.Open);
        }
        else
        {
            file = new FileStream(dir + fileName + ".csv", FileMode.Create);
        }
        StreamWriter streamWriter = new StreamWriter(file);
        streamWriter.Write(contentMes);
        streamWriter.Flush();
        streamWriter.Close();
        file.Close();
    }
}
