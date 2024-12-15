using System.Collections.Generic;
using UnityEngine;

public class PointCloudLoader : MonoBehaviour
{
    public List<Vector3> LoadPointCloud(string filePath)
    {
        Debug.Log("Dosya yolu: " + filePath);

        List<Vector3> points = new List<Vector3>();

        TextAsset fileData = Resources.Load<TextAsset>(filePath);
        if (fileData == null)
        {
            Debug.LogError("Dosya bulunamadı: " + filePath);
            return points;
        }

        string[] lines = fileData.text.Split('\n');
        int numPoints = int.Parse(lines[0].Trim());

        for (int i = 1; i <= numPoints; i++)
        {
            string[] coords = lines[i].Trim().Split(' ');
            float x = float.Parse(coords[0]);
            float y = float.Parse(coords[1]);
            float z = float.Parse(coords[2]);
            points.Add(new Vector3(x, y, z));
        }

        return points;
    }

    public (List<Vector3>, List<Vector3>) LoadTwoPointClouds(string filePath1, string filePath2)
    {
        List<Vector3> P = LoadPointCloud(filePath1);
        List<Vector3> Q = LoadPointCloud(filePath2);
        return (P, Q);
    }
}
