using System.Collections.Generic;
using UnityEngine;

public class RigidTransformation
{
    public List<Vector3> Apply(List<Vector3> P, List<Vector3> Q)
    {
        // 1. Merkezleri Hesapla
        Vector3 centerP = CalculateCentroid(P);
        Vector3 centerQ = CalculateCentroid(Q);

        // 2. Merkezden Kaydır
        List<Vector3> alignedP = SubtractCentroid(P, centerP);
        List<Vector3> alignedQ = SubtractCentroid(Q, centerQ);

        // 3. Rotasyon Matrisi Hesapla (R)
        Matrix4x4 R = CalculateRotationMatrix(alignedP, alignedQ);

        // 4. Translasyon Hesapla (T)
        Vector3 T = centerQ - R.MultiplyPoint(centerP);

        // 5. Yeni Noktaları Hesapla
        List<Vector3> transformedQ = new List<Vector3>();
        foreach (Vector3 point in Q)
        {
            transformedQ.Add(R.MultiplyPoint(point) + T);
        }

        return transformedQ;
    }

    private Vector3 CalculateCentroid(List<Vector3> points)
    {
        Vector3 centroid = Vector3.zero;
        foreach (Vector3 point in points)
        {
            centroid += point;
        }
        return centroid / points.Count;
    }

    private List<Vector3> SubtractCentroid(List<Vector3> points, Vector3 centroid)
    {
        List<Vector3> result = new List<Vector3>();
        foreach (Vector3 point in points)
        {
            result.Add(point - centroid);
        }
        return result;
    }

    private Matrix4x4 CalculateRotationMatrix(List<Vector3> P, List<Vector3> Q)
    {
        // Basitleştirilmiş bir rotasyon matrisi döndürülüyor
        return Matrix4x4.identity;
    }

    public string GetTransformationMatrix()
    {
        return "R: [1 0 0; 0 1 0; 0 0 1]\nT: [0; 0; 0]"; // Örnek bir dönüşüm matrisi
    }

}
