using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectModule : MonoBehaviour
{
    public Color ModuleColor;
    public GameObject TextureObject;
    public ObjectModule[] NearModules { get; set; }

    public event EventHandler OnModuleClick;
    public ModuleInfo Info { get; set; }

	// Use this for initialization
	void Start ()
	{
	    GetComponentInChildren<ModuleButton>().OnModuleClick += OnModuleClickDo;
	}

    public void ApplyModuleColor()
    {
        Sprite sprite = TextureObject.GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = new Texture2D(sprite.texture.width, sprite.texture.height);
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(i, j, ModuleColor);
            }
        }
        texture.Apply();
        TextureObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        TextureObject.transform.position = new Vector3(TextureObject.transform.position.x, TextureObject.transform.position.y, 1);
    }
    public void ApplyModuleColor(Color moduleColor)
    {
        Sprite sprite = TextureObject.GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = new Texture2D(sprite.texture.width, sprite.texture.height);
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(i, j, moduleColor);
            }
        }
        texture.Apply();
        TextureObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        TextureObject.transform.position = new Vector3(TextureObject.transform.position.x, TextureObject.transform.position.y, 1);
        this.ModuleColor = moduleColor;
    }
    public void ApplyModuleTitle(string title)
    {
        this.transform.Find("moduleTitle").GetComponent<TextMeshPro>().text = title;
    }


    void OnModuleClickDo(object obj, EventArgs e)
    {
        Debug.Log(string.Format("MODULE TYPE: {0}", Info.Type));

        if(OnModuleClick != null)
            OnModuleClick(this, EventArgs.Empty);
    }
}
