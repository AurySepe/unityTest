using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Whiteboard : MonoBehaviour
{
    [HideInInspector]
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048,2048);
    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y,TextureFormat.ARGB32,true);
        r.material.mainTexture = texture;
        
        Color[] colors = new Color[(int)textureSize.x];
        int mipCount = Mathf.Min((int)textureSize.x, texture.mipmapCount);

        // tint each mip level
        for (int mip = 0; mip < mipCount; ++mip)
        {
            Color[] cols = texture.GetPixels(mip);
            for (int i = 0; i < cols.Length; ++i)
            {
                cols[i] = r.material.color;
            }
            texture.SetPixels(cols, mip);
        }
        // actually apply all SetPixels, don't recalculate mip levels
        texture.Apply(false);
    }
}
