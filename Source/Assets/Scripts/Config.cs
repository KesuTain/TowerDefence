using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Linq;

public class Config : MonoBehaviour
{
    private string Path;
    
    void Awake()
    {
        Path = Application.dataPath + "/DefaulConfig.xml";
        Load();
    }
    
    void Save()
    {
        XElement Parameters = ResourceSystem.SaveParameters();

        XDocument SaveDoc = new XDocument(Parameters);
        File.WriteAllText(Path, SaveDoc.ToString());
        Debug.Log("Succesful " + Path);
    }

    void Load()
    {
        XElement Parameters = null;

        if (File.Exists(Path))
        {
            Parameters = XDocument.Parse(File.ReadAllText(Path)).Element("WaveParameters");
            SetParameters(Parameters);
        } else
        {
            Debug.Log("File not found... Create");
            Save();
            Load();
        }
        
    }

    void SetParameters(XElement Parameters)
    {
        ResourceSystem.IntervalWave = int.Parse(Parameters.Attribute("WaveInterval").Value);
        Debug.Log("Между волнами интервал = " + ResourceSystem.IntervalWave + " секунд");
    }
}
