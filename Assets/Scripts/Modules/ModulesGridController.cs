using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModulesGridController : MonoBehaviour
{
    public const float MODULE_SIZE = 2.55f;

    public GameObject ModulePrefab;
    public HashSet<ObjectModule> ModulesSet { get; set; }

    public event EventHandler ModuleCreated;
	// Use this for initialization
	void Start ()
    {
        ModulesSet = new HashSet<ObjectModule>();
        CreateBasicModule();
    }

    private void CreateBasicModule()
    {
        ModuleInfo info = ModuleInfo.InfoBasic();

        Color color = Color.white;
        ObjectModule basicModule = CreateNewModule(info);
        
    }
    private ObjectModule CreateNewModule(ModuleInfo info)
    {
        // Создание экземпляра объекта и получения скрипта модуля
        GameObject moduleObject = Instantiate(ModulePrefab);
        ObjectModule moduleScript = moduleObject.GetComponent<ObjectModule>();

        // Настройка отображения в игре
        moduleScript.ApplyModuleTitle(info.Title);
        moduleScript.ApplyModuleColor(info.Color);
        moduleObject.transform.position = new Vector3(info.PositionX * MODULE_SIZE, info.PositionY * MODULE_SIZE);
        moduleScript.transform.parent = this.transform;

        // Настройка скриптовой части
        ModulesSet.Add(moduleScript);
        moduleScript.Info = info;
        moduleScript.OnModuleClick += OnModuleClick;

        if (info.Type != "_temp")
            CreateModulesAround(moduleScript);

        if(ModuleCreated != null)
            ModuleCreated(moduleScript, EventArgs.Empty);

        return moduleScript;
    }

    private void CreateModulesAround(ObjectModule module)
    {
        Vector2 postion = module.gameObject.transform.position;
        module.NearModules = new ObjectModule[4];

        List<int[]> newCoords = new List<int[]>();

        newCoords.Add(new []{ module.Info.PositionX + 1, module.Info.PositionY });
        newCoords.Add(new[] { module.Info.PositionX - 1, module.Info.PositionY });
        newCoords.Add(new[] { module.Info.PositionX, module.Info.PositionY + 1 });
        newCoords.Add(new[] { module.Info.PositionX, module.Info.PositionY - 1 });

        for (int i = 0; i < ModulesSet.Count(); i++)
        {
            ObjectModule t = ModulesSet.ElementAt(i);

            int xC = t.Info.PositionX;
            int yC = t.Info.PositionY;

            newCoords.RemoveAll(x => x[0] == xC && x[1] == yC);
        }

        for (int i = 0; i < newCoords.Count; i++)
        {
            ModuleInfo info = ModuleInfo.InfoTemporary();

            info.PositionX = newCoords[i][0];
            info.PositionY = newCoords[i][1];

            module.NearModules[i] = CreateNewModule(info);
        }
        
    }
    private void OnModuleClick(object obj, EventArgs e)
    {
        ObjectModule module = obj as ObjectModule;

        if (module.Info.Type == "_temp")
        {
            ModuleInfo info = ModuleInfo.InfoBasic();
            info.PositionX = module.Info.PositionX;
            info.PositionY = module.Info.PositionY;

            ModulesSet.Remove(module);
            Destroy(module.gameObject);

            CreateNewModule(info);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
