using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class ProvinceContourExtractor
{
    // 8 Nachbarn (Moore neighborhood, im Uhrzeigersinn)
    private static readonly Vector2Int[] neighbors =
    {
        new (-1, -1),
        new (0, -1),
        new (1, -1),
        new (1, 0),
        new (1, 1),
        new (0, 1),
        new (-1, 1),
        new (-1, 0),
    };

    public static List<Vector2> TraceProvince(Texture2D tex, string hex) {
        ColorUtility.TryParseHtmlString(hex, out Color targetColor);

        // 1. Startpixel finden
        Vector2Int? start = FindStartPixel(tex, targetColor);
        if (start == null)
            return new List<Vector2>();

        var contour = new List<Vector2>();
        Vector2Int current = start.Value;
        int dir = 0; // Start-Richtung

        contour.Add(current);

        do
        {
            // Im Uhrzeigersinn Nachbarn durchsuchen
            bool foundNext = false;
            for (int i = 0; i < neighbors.Length; i++)
            {
                int checkDir = (dir + i) % neighbors.Length;
                Vector2Int n = current + neighbors[checkDir];

                if (IsInside(tex, n) && tex.GetPixel(n.x, n.y) == targetColor)
                {
                    current = n;
                    contour.Add(current);
                    dir = (checkDir + neighbors.Length - 2) % neighbors.Length; 
                    foundNext = true;
                    break;
                }
            }

            if (!foundNext) break;

        } while (current != start.Value);

        // 2. Pixel in Mitte verschieben (optional, schöner für Triangulation)
        for (int i = 0; i < contour.Count; i++)
        {
            contour[i] += new Vector2(0.5f, 0.5f);
        }

        // 3. Bereinigen
        contour = CleanContour(contour);

        return contour;
    }

    private static Vector2Int? FindStartPixel(Texture2D tex, Color targetColor)
    {
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                if (tex.GetPixel(x, y) == targetColor && IsBoundary(tex, x, y, targetColor))
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return null;
    }

    private static bool IsBoundary(Texture2D tex, int x, int y, Color targetColor)
    {
        foreach (var n in neighbors)
        {
            int nx = x + n.x;
            int ny = y + n.y;
            if (IsInside(tex, new Vector2Int(nx, ny)))
            {
                if (tex.GetPixel(nx, ny) != targetColor)
                    return true;
            }
        }
        return false;
    }

    private static bool IsInside(Texture2D tex, Vector2Int p)
    {
        return p.x >= 0 && p.y >= 0 && p.x < tex.width && p.y < tex.height;
    }

    /// <summary>
    /// Entfernt doppelte und collineare Punkte aus der Kontur
    /// </summary>
    private static List<Vector2> CleanContour(List<Vector2> contour)
    {
        if (contour.Count < 3) return contour;

        // Duplikate entfernen
        var cleaned = contour.Distinct().ToList();

        // Collinearität prüfen
        List<Vector2> result = new List<Vector2>();
        for (int i = 0; i < cleaned.Count; i++)
        {
            Vector2 prev = cleaned[(i - 1 + cleaned.Count) % cleaned.Count];
            Vector2 curr = cleaned[i];
            Vector2 next = cleaned[(i + 1) % cleaned.Count];

            Vector2 dir1 = (curr - prev).normalized;
            Vector2 dir2 = (next - curr).normalized;

            // Wenn nicht fast in einer Linie, dann behalten
            if (Vector2.Angle(dir1, dir2) > 1f)
                result.Add(curr);
        }

        return result;
    }
}
