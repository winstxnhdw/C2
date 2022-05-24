using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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
        FileInfo[] screenshots = this.directory.GetFiles("*.png");
        List<int> validScreenshotNames = new List<int>(screenshots.Length);

        foreach (FileInfo screenshot in screenshots) {
            string name = Path.GetFileNameWithoutExtension(screenshot.Name);

            if (int.TryParse(name, out int validName)) {
                validScreenshotNames.Add(validName);
            }
        }

        string screenshotName = validScreenshotNames.Any() ? $"{validScreenshotNames.Max() + 1}.png" : "1.png";
        ScreenCapture.CaptureScreenshot(Path.Combine(this.directory.FullName, screenshotName));
    }

    void OnDestroy() {
        InputListener.onPrintScreenPress -= this.Screenshot;
    }
}
