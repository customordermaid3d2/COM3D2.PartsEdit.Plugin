using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Reflection;

using UnityEngine;

internal class PngData {
    byte[] pngByteData;

    public PngData(string name) {
        Debug.Log("test1 : "+ PluginInfo.NameSpace + ".PngResource." + name);
        // COM3D2.PartsEdit.Plugin.CM3D2.PartsEdit.Plugin.PngResource.GearIcon.png
        Stream pngStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(PluginInfo.NameSpace + ".PngResource." + name);
        Debug.Log("test2");
        int length = (int)pngStream.Length;
        Debug.Log("test3");
        pngByteData = new byte[length];
        Debug.Log("test4");
        pngStream.Read(pngByteData, 0, length);
        Debug.Log("test5");
    }

    public byte[] GetData() {
        return pngByteData;
    }
}
