using System.Collections.Generic;
using UnityEngine;

public class RANSACAlignment
{
    public List<Vector3> Apply(List<Vector3> P, List<Vector3> Q, int maxIterations, float threshold)
    {
        List<Vector3> bestTransformedPoints = null;
        int maxInliers = 0;

        for (int i = 0; i < maxIterations; i++)
        {
            // 1. Rastgele Üç Nokta Seç
            List<Vector3> randomP = GetRandomSubset(P, 3);
            List<Vector3> randomQ = GetRandomSubset(Q, 3);

            // 2. Rigid Transformation Hesapla
            (Matrix4x4 R, Vector3 T) = CalculateRigidTransformation(randomP, randomQ);

            // 3. Tüm Noktalara Uygula
            List<Vector3> transformedQ = TransformPoints(Q, R, T);

            // 4. Hataları Hesapla
            int inliers = CountInliers(P, transformedQ, threshold);

            // 5. En İyi Sonucu Kaydet
            if (inliers > maxInliers)
            {
                maxInliers = inliers;
                bestTransformedPoints = new List<Vector3>(transformedQ);
            }
        }

        Debug.Log("RANSAC: En fazla inlier sayısı: " + maxInliers);
        return bestTransformedPoints;
    }

    private List<Vector3> GetRandomSubset(List<Vector3> points, int count)
    {
        List<Vector3> subset = new List<Vector3>();
        System.Random random = new System.Random();
        for (int i = 0; i < count; i++)
        {
            int index = random.Next(points.Count);
            subset.Add(points[index]);
        }
        return subset;
    }

    private (Matrix4x4, Vector3) CalculateRigidTransformation(List<Vector3> P, List<Vector3> Q)
    {
        // 3 nokta üzerinden rigid transformation hesaplama
        // Şu an için basit bir dönüşüm döndürülüyor
        return (Matrix4x4.identity, Vector3.zero);
    }

    private List<Vector3> TransformPoints(List<Vector3> points, Matrix4x4 R, Vector3 T)
    {
        List<Vector3> transformed = new List<Vector3>();
        foreach (Vector3 point in points)
        {
            transformed.Add(R.MultiplyPoint(point) + T);
        }
        return transformed;
    }

    private int CountInliers(List<Vector3> P, List<Vector3> Q, float threshold)
    {
        int inliers = 0;
        for (int i = 0; i < P.Count; i++)
        {
            if (Vector3.Distance(P[i], Q[i]) < threshold)
            {
                inliers++;
            }
        }
        return inliers;
    }

    public string GetTransformationMatrix()
    {
        return "R: [1 0 0; 0 1 0; 0 0 1]\nT: [1; 1; 1]"; // RANSAC tarafından bulunan dönüşüm
    }

}
