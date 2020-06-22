using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapTextureExtractor
{
    public static int[] GetTextureData(Texture2D texture, int widthOffset = 0, int heightOffset = 0)
    {
        return GetTextureData(texture, texture.width, texture.height, widthOffset, heightOffset);
    }

    public static int[] GetTextureData(Texture2D texture, int width, int height, int widthOffset = 0, int heightOffset = 0)
    {
        int[] result = new int[width * height];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = (texture.GetPixel((i % width) + widthOffset, (i / height) + heightOffset) == Color.black ? 1 : 0); //Mettre un switch pour plusieurs couleurs
            Debug.Log(result[i]);
        }
        return result;
    }

    static int[] FillRectangle(int value, int width, int height, int widthOffset = 0, int heightOffset = 0)
    {
        int[] result = new int[width * height];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = value;
        }
        return result;
    }
}
