# MakeLove 
![Travis Build Status](https://api.travis-ci.org/instilledbee/MakeLove.svg?branch=master)

Automatically create builds for your LOVE game, using C# and .NET Core

# [Installation](#installation)
* Download the latest build on the [Releases](https://github.com/instilledbee/MakeLove/releases) page
    * You may also [build](#building) the app from source if you prefer.
* Edit the [configuration](#configuration) file
* Run the application
* Work on your LOVE project
* Any changes in your source directory will trigger new builds (only Windows and the LOVE zip format are supported for now)

# [Configuration](#configuration)
The following values are configurable by editing the `MakeLove.App.config` file in a text editor:
* `sourcePath` - Absolute path where your project's source and asset files are stored, which will be monitored by MakeLove for changes. All the contents of the source directory will be included in the builds.
* `buildPath` - Absolute path where the build artifacts will be placed by MakeLove
* `buildName` - The filename to use for the builds created, without the file extension. The files will be created as `<buildName>.love` or `<buildName>.exe`.
* `loveLibPath` - Absolute path where the LOVE SDK is installed on the machine, so the needed dependencies can be copied during build creation.
* `useBuildNumber` - Set this to true, so MakeLove can append auto-incremented build numbers on the created files, e.g. `<buildName>001.love`.
* `buildNumber` - The starting build number to use when `useBuildNumber` is true.
* `buildTargets` - Comma-separated values of platform targets to create builds for. As of 0.1.0, only `windows` is supported.

# [Building](#building)
* Install the [.NET Core SDK](https://www.microsoft.com/net/learn/get-started), and your IDE of choice:
    * [Visual Studio](https://www.visualstudio.com/thank-you-downloading-visual-studio/?sku=Community&rel=15)
    * [Visual Studio Code (with the C# extension)](https://code.visualstudio.com)
    * [Jetbrains Rider](https://www.jetbrains.com/rider/)
* Open the `.sln` file
* Run `dotnet restore` to restore the NuGet packages.
* Run `dotnet build` to create a runnable build of the application.

# [Contributing](#contributing)
* Please feel free to submit pull requests for bug fixes or additional features. Some feature ideas:
    * Creating builds for other target platforms that are supported by LOVE for [distribution](https://love2d.org/wiki/Game_Distribution)
         * Mac OS
         * Linux
         * Android
         * iOS
         * Web (via love.js)

