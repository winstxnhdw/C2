using System;
using System.IO;
using System.Linq;
using UnityEngine;

// Only supports up to 18,446,744,073,709,551,615 screenshots
public class ScreenshotManager : MonoBehaviour {
    const string DirectoryName = "Screenshots";
    DirectoryInfo Directory { get; set; }

    void Awake() {
        InputListener.onPrintScreenPress += this.Screenshot;

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string screenshotPath = Path.Combine(documentsPath, Application.productName, DirectoryName);
        System.IO.Directory.CreateDirectory(screenshotPath);
        this.Directory = new DirectoryInfo(screenshotPath);
    }

    void Screenshot() {
        ParallelQuery<ulong> validScreenshotNames =
            this.Directory.GetFiles("*.png")
                          .AsParallel()
                          .Where(screenshot => Path.GetFileNameWithoutExtension(screenshot.Name).All(char.IsDigit))
                          .Select(screenshot => ulong.Parse(Path.GetFileNameWithoutExtension(screenshot.Name)));

        string screenshotName = validScreenshotNames.Any() ? $"{validScreenshotNames.Max() + 1}.png" : "1.png";
        ScreenCapture.CaptureScreenshot(Path.Combine(this.Directory.FullName, screenshotName));
    }

    void OnDestroy() {
        InputListener.onPrintScreenPress -= this.Screenshot;
    }
}
