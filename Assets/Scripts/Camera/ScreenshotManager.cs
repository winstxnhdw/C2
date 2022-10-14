using System;
using System.IO;
using System.Linq;
using UnityEngine;

// Only supports up to 2147483647 screenshots
public class ScreenshotManager : MonoBehaviour {
    const string directoryName = "Screenshots";
    DirectoryInfo directory;

    void Awake() {
        InputListener.onPrintScreenPress += this.Screenshot;

        string screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.productName, directoryName);
        Directory.CreateDirectory(screenshotPath);
        this.directory = new DirectoryInfo(screenshotPath);
    }

    void Screenshot() {
        ParallelQuery<int> validScreenshotNames =
            this.directory.GetFiles("*.png")
                          .AsParallel()
                          .Where(screenshot => Path.GetFileNameWithoutExtension(screenshot.Name).All(char.IsDigit))
                          .Select(screenshot => Int32.Parse(Path.GetFileNameWithoutExtension(screenshot.Name)));

        string screenshotName = validScreenshotNames.Any() ? $"{validScreenshotNames.Max() + 1}.png" : "1.png";
        ScreenCapture.CaptureScreenshot(Path.Combine(this.directory.FullName, screenshotName));
    }

    void OnDestroy() {
        InputListener.onPrintScreenPress -= this.Screenshot;
    }
}
