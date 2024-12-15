using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegistrationManager : MonoBehaviour
{
    public PointCloudVisualizer visualizer; // Noktaları görselleştiren script
    public TMP_Text transformationText;
    public PointCloudLoader loader; // PointCloudLoader script'i
    public string fileP = "point_cloud_p"; // P kümesi için dosya adı
    public string fileQ = "point_cloud_q"; // Q kümesi için dosya adı
    public List<Vector3> P; // Birinci nokta kümesi
    public List<Vector3> Q; // İkinci nokta kümesi

    void Start()
    {
        // Null kontrolü: Visualizer atanmış mı?
        if (visualizer == null)
        {
            Debug.LogError("Visualizer atanmadı! Inspector'dan atayın.");
            return;
        }

        // P ve Q noktalarını PointCloudLoader üzerinden yükle
        P = loader.LoadPointCloud(fileP);
        Q = loader.LoadPointCloud(fileQ);

        // Dosya yükleme kontrolü
        if (P == null || Q == null || P.Count == 0 || Q.Count == 0)
        {
            Debug.LogError("P veya Q dosyaları yüklenemedi!");
            return;
        }

        Debug.Log("P ve Q noktaları başarıyla yüklendi.");
    }

    public void ApplyRigidTransformation()
    {
        Debug.Log("Rigid Transformation uygulanıyor...");
        RigidTransformation rigidTransformation = new RigidTransformation();
        List<Vector3> transformedQ = rigidTransformation.Apply(P, Q);
        visualizer.SetPoints(transformedQ, Color.green, Q); // Noktaları yeşil renkle güncelle
    
        transformationText.text = "Transformation: Rigid Transformation\nMatrix: \n" + rigidTransformation.GetTransformationMatrix();
    }

    public void ApplyRANSACAlignment()
    {
        Debug.Log("RANSAC Alignment uygulanıyor...");
        RANSACAlignment ransacAlignment = new RANSACAlignment();
        List<Vector3> transformedQ = ransacAlignment.Apply(P, Q, 100, 0.5f); // 100 iterasyon ve hata eşiği 0.5
        if (transformedQ == null || transformedQ.Count == 0)
        {
            Debug.LogError("RANSAC sonucu boş bir liste döndü!");
            return;
        }
        
        if (visualizer == null)
        {
            Debug.LogError("Visualizer atanmadı! Inspector'da kontrol edin.");
            return;
        }
        visualizer.SetPoints(transformedQ, Color.blue, Q); // Noktaları mavi renkle güncelle
    
         transformationText.text = "Transformation: RANSAC Alignment\nMatrix: \n" + ransacAlignment.GetTransformationMatrix();
    }
}
