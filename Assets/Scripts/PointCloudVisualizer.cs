using System.Collections.Generic;
using UnityEngine;

public class PointCloudVisualizer : MonoBehaviour
{
    public GameObject pointPrefab; // Noktayı temsil eden prefab (örneğin bir küre)
    public float pointScale = 0.1f; // Noktaların boyutu
    private List<GameObject> instantiatedPoints = new List<GameObject>(); // Oluşturulan noktaların referansları

    public void SetPoints(List<Vector3> points, Color pointColor, List<Vector3> originalPoints = null)
    {
        // Mevcut noktaları temizle
        ClearPoints();

        // Yeni noktaları oluştur ve görselleştir
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 point = points[i];

            // Noktayı oluştur
            GameObject pointObject = Instantiate(pointPrefab, point, Quaternion.identity, transform);
            pointObject.transform.localScale = Vector3.one * pointScale; // Noktanın boyutu
            pointObject.GetComponent<Renderer>().material.color = pointColor; // Noktanın rengi
            instantiatedPoints.Add(pointObject); // Oluşturulan noktayı listeye ekle

            // Eğer orijinal noktalar varsa, bu nokta ile çizgi çiz
            if (originalPoints != null && i < originalPoints.Count)
            {
                Debug.DrawLine(originalPoints[i], point, Color.red, 5f); // Çizgiyi kırmızı renkle çiz (5 saniye görünsün)
            }
        }
    }

    /// <summary>
    /// Mevcut tüm noktaları temizler.
    /// </summary>
    public void ClearPoints()
    {
        foreach (GameObject pointObject in instantiatedPoints)
        {
            Destroy(pointObject);
        }
        instantiatedPoints.Clear();
    }

    /// <summary>
    /// Test amaçlı rastgele bir nokta kümesini görselleştirir.
    /// </summary>
    void Start()
    {
        // Test için bir nokta kümesi oluştur
        List<Vector3> testPoints = new List<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1),
            new Vector3(2, 2, 2)
        };

        SetPoints(testPoints, Color.red); // Test noktalarını kırmızı renkle görselleştir
    }
}
