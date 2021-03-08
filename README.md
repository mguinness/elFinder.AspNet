# elFinder.AspNet

[elFinder](http://elfinder.org/) is an open-source file manager for web, written in JavaScript using jQuery and jQuery UI.  This repo is a 3rd party volume driver for .NET and is derived from project [elFinder.NetCore](https://github.com/gordon-matt/elFinder.NetCore) which itself was derived from the earlier work of the [Elfinder.NET](https://github.com/EvgenNoskov/Elfinder.NET) project.

![Screenshot](https://user-images.githubusercontent.com/1412630/47564735-f4af0a00-d960-11e8-9f89-d036d092c1b9.png)

This project is packaged as a .NET Standard 2.0 library which means it can be used with ASP.NET Core 2.x and later.  In addition it can also be used with MVC 5 projects using .NET Framework 4.6.x and later.

## Usage

Install the [elFinder.AspNet](https://www.nuget.org/packages/elFinder.AspNet/) NuGet package into your project.

For ASP.NET Core projects see the sample project, in particular the [FileSystemController](elFinder.AspNet.Web/Controllers/FileSystemController.cs) class.

For MVC 5 projects see [example](MVC5.md) that contains the FileSystemController class compatible for that framework.