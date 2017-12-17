using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;


public class ModuleInfo
{
    public string Title { get; set; }
    public string Type { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public Color Color { get; set; }
    public Resource ModuleResource { get; set; }

    public static ModuleInfo InfoBasic()
    {
        ModuleInfo info = new ModuleInfo();
        info.Color = UnityEngine.Color.white;
        info.Type = "_basic";
        

        var controller = GameObject.Find("ResourcesController").GetComponent<ResourcesController>();
        int resInd = Random.Range(0, controller.Warehouse.Count);

        info.ModuleResource = controller.Warehouse.ElementAt(resInd).Key;
        info.Title = string.Format("Базовый модуль ({0})", info.ModuleResource.Title);

        return info;
    }

    public static ModuleInfo InfoTemporary()
    {
        ModuleInfo info = new ModuleInfo();
        info.Color = UnityEngine.Color.gray;
        info.Type = "_temp";
        info.Title = "Временный модуль";
        return info;
    }
}
