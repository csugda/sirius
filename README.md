# Project Sirius
![alt text](https://travis-ci.org/csugda/sirius.svg?branch=master "Travis CI")

##### CSU Game Developers Association - Spring 2018 project

## Installation
- Install Unity version 2017.3.x

## Build

### Build from UI:
- File > Build Settings > Build

### Build from command line:
To build for Windows:
```
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -nographics -silent-crashes -projectPath <siriusPath> -buildWindowsPlayer "<userHome>/Build/windows/sirius.exe" -quit
```

To build for Mac:
```
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -nographics -silent-crashes -projectPath <siriusPath> -buildOSXPlayer "<userHome>/Build/osx/sirius.app" -quit
```

To build for Linux:
```
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -nographics -silent-crashes -projectPath <siriusPath> -buildOSXPlayer "<userHome>/Build/linux/sirius.exe" -quit
```

