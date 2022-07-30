<div align="center">
    <img src="resources/banner.png" />
</div>

# COMMAND & CONTROL

**COMMAND & CONTROL** is a single-player puzzle-simulation game for the [Singapore Army Open House 2022](https://www.mindef.gov.sg/web/portal/mindef/news-and-events/latest-releases/article-detail/2022/May/06may22_fs). See it briefly featured [here](https://www.facebook.com/plugins/video.php?height=250&href=https%3A%2F%2Fwww.facebook.com%2Foursingaporearmy%2Fvideos%2F1428028877712911%2F&show_text=false&t=150) at the Army Cyber Defence booth! The game intends to educate players on the importance of cyber and password security. Player scores are tracked in [C2-leaderboard](https://github.com/winstxnhdw/C2-leaderboard).

## Requirements

- Windows 10/11
- Unity 2020.3.29f1
- Microsoft .NET 4.6 Framework
- C# 8.0

## Setup

You will need to add your API URL and URI for players to submit their username and scores to the leaderboard server. First, create the following directory.

```bash
mkdir C2/Assets/Resources/API
```

### Serverless API Endpoint

Add a `.txt` file containing the URL to your online database.

```bash
$ echo https://type-your.api.com/url/here >> C2/Assets/Resources/API/url.txt
```

### Local API Endpoint

Add a `.txt` file containing the URL to your local database.


```bash
$ echo https://type-your.api.com/url/here >> C2/Assets/Resources/API/url_local.txt
```

If you do not have plans to use a local database, simply create an empty file instead.

```bash
touch C2/Assets/Resources/API/url_local.txt
```

### Server URI Endpoints

Add a `.txt` file containing the URI endpoint for your REST requests.

> e.g. /api/somesecretkeythatyoudontwantotherstoknow/submit

```bash
$ echo https://type-your.api.com/url/here >> C2/Assets/Resources/API/uri.txt
```

## Namespace Issues

Occasionally, Visual Studio Code may run into the following namespace issue.

> The type or namespace name '\<namespace\>' could not be found \(are you missing a using directive or an assembly reference?\)\[Assembly-CSharp\]

To resolve this, simply close Unity and VSCode, and remove the `Library` directory from the project folder. Then, reopen Unity to rebuild the `Library` directory.

```bash
rm -r Library
```

## Themes

<div align="center">
    <img src="resources/preview.gif" />
</div>

## Credits

Many thanks to the following civilians for their contributions:

[Alyssa](https://github.com/alyssaxchua) - Algorithm & design

[Ting Ying](https://github.com/LTYGUY) - Debug & testing
