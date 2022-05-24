<div align="center">
    <img src="resources/banner.png" />
</div>

# COMMAND & CONTROL

**COMMAND & CONTROL** is a single-player puzzle-simulation game for the [Singapore Army Open House 2022](https://www.mindef.gov.sg/web/portal/mindef/news-and-events/latest-releases/article-detail/2022/May/06may22_fs). The game intends to educate players on the importance of cyber and password security.

## Requirements

- Windows 10/11
- Unity 2020.3.29f1
- Microsoft .NET 4.6 Framework
- C# 8.0
- Docker (Leaderboard only)

## Setup

You will need to add your API URL for players to submit their username and scores to the leaderboard server.

```bash
$ mkdir Passwordle/Assets/Resources/API
$ echo https://type-your.api.com/url/here >> Passwordle/Assets/Resources/API/api.txt
```

## Namespace Issues

Occasionally, Visual Studio Code may run into the following namespace issue.

> The type or namespace name '\<namespace\>' could not be found \(are you missing a using directive or an assembly reference?\)\[Assembly-CSharp\]

To solve this issue, simply close Unity and VSCode, and remove the `Library` directory from the project folder. Then, reopen Unity to rebuild the `Library` directory.

```bash
rm -r Library
```

## Leaderboard

Due to the nature of the event, the leaderboard is hosted separately from the game.

### Installation

The installation script only supports Arch or Ubuntu. If you are on any other platform, you will have to install Docker manually [here](https://docs.docker.com/get-docker/).

```bash
sh Leaderboard/requirements.sh
```

### Development

```bash
sh Leaderboard/launch.sh
```

## Themes

<div align="center">
    <img src="resources/preview.gif" />
</div>