using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesController : MonoBehaviour {

    public Dictionary<Resource, float[]> Warehouse { get; set; }
    public ModulesGridController ModulesController;


    public Text ResourceListText;

    void Start()
    {
        List<Resource> container = LoadResourcesFromXml();

        Warehouse = new Dictionary<Resource, float[]>();

        for (int i = 0; i < container.Count; i++)
        {
            Warehouse.Add(container[i], new float[]{ 0, 0});
        }

        ModulesController.ModuleCreated += OnModuleCreated;
        InvokeRepeating("ExtractResources", 0, 1.0f);
    }
    private static List<Resource> LoadResourcesFromXml()
    {
        string path = string.Format("{0}/Data/Resources.xml", Application.dataPath);

        XmlSerializer serializer = new XmlSerializer(typeof(List<Resource>));

        var stream = new FileStream(path, FileMode.Open);
        var container = serializer.Deserialize(stream) as List<Resource>;
        stream.Close();
        return container;
    }

    private void OnModuleCreated(object sender, EventArgs e)
    {
        ObjectModule module = sender as ObjectModule;

        if(module.Info.ModuleResource == null)
            return;

        Warehouse[module.Info.ModuleResource][1] += module.Info.ModuleResource.PerUnit;


    }

    void Update()
    {
        
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < Warehouse.Count; i++)
        {
            float percentage = (Warehouse.ElementAt(i).Value[0] / Warehouse.ElementAt(i).Value[1]);
            if (float.IsNaN(percentage))
            {
                continue;
            }

            builder.Append(string.Format("{0} : {1:p2}%\n", Warehouse.ElementAt(i).Key.Title, percentage));
        }

        ResourceListText.text = builder.ToString();

    }

    void ExtractResources()
    {
        foreach (var module in ModulesController.ModulesSet)
        {
            if (module.Info.ModuleResource == null)
                continue;

            Resource res = module.Info.ModuleResource;

            Warehouse[res][0] += res.ExtractionPerTime;

            if (Warehouse[res][0] > Warehouse[res][1])
            {
                Warehouse[res][0] = Warehouse[res][1];
            }
        }
    }
}
public class Resource
{
    public string Title { get; set; }
    public float Valuable { get; set; }
    public float ExtractionPerTime { get; set; }
    public float PerUnit { get; set; }
}
