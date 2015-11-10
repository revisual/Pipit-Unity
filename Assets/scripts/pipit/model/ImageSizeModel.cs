using UnityEngine;
using System.Collections;
using System;

public class ImageSizeModel  {

   // private readonly int[] HEIGHT_VALUES = new int[] { 320, 512, 662, 800, 1080 };
    private readonly int[] WIDTH_VALUES = new int[] { 480, 768, 992, 1200, 1620 };



    public string GetImageSize()
    {
        int largestDim = Math.Max(Screen.width, Screen.height);

        int len = WIDTH_VALUES.Length;
        for (int i = 0; i < len; i++)
        {
           if(largestDim <= WIDTH_VALUES[i])
            {
                return WIDTH_VALUES[i].ToString();
            }
        }

        return WIDTH_VALUES[WIDTH_VALUES.Length-1].ToString();
    }
}
