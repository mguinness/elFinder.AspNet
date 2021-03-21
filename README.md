# elFinder.AspNet

[elFinder](http://elfinder.org/) is an open-source file manager for web, written in JavaScript using jQuery and jQuery UI.  This repo is a 3rd party volume driver for .NET and is derived from project [elFinder.NetCore](https://github.com/gordon-matt/elFinder.NetCore) which itself was derived from the earlier work of the [Elfinder.NET](https://github.com/EvgenNoskov/Elfinder.NET) project.

![Screenshot](https://user-images.githubusercontent.com/1412630/47564735-f4af0a00-d960-11e8-9f89-d036d092c1b9.png)

This project is packaged as a .NET Standard 2.0 library which means it can be used with ASP.NET Core 2.x and later.  In addition it can also be used with MVC 5 projects using .NET Framework 4.6.x and later.

## Usage

Install the [elFinder.AspNet](https://www.nuget.org/packages/elFinder.AspNet/) NuGet package into your project.

For ASP.NET Core projects see the sample project, in particular the [FileSystemController](elFinder.AspNet.Web/Controllers/FileSystemController.cs) class.

For MVC 5 projects see [example](MVC5.md) that contains the FileSystemController class compatible for that framework.

## Logging

It's easy to log activity with the returned object from `ProcessAsync()` after checking that no error occurred.

```C#
var result = await connector.ProcessAsync(parameters);
if (result.Succeeded)
    await LogCmdAsync(parameters["cmd"], result.Value);
```

The following method is a basic example of some commands being logged.

```C#
private async Task LogCmdAsync(string command, object result)
{
    switch (command)
    {
        case "mkdir":
            var make = result as AddResponseModel;
            foreach (BaseModel item in make.Added)
                Trace.TraceInformation("elFinder created {name}", await connector.GetPathByHashAsync(item.Hash));
            break;
        case "paste":
            var replace = result as ReplaceResponseModel;
            foreach (BaseModel item in replace.Added)
                Trace.TraceInformation"elFinder pasted {name}", await connector.GetPathByHashAsync(item.Hash));
            break;
        case "rename":
            var rename = result as ReplaceResponseModel;
            var i = 0;
            foreach (BaseModel item in rename.Added)
                Trace.TraceInformation("elFinder renamed {old} to {new}", await connector.GetPathByHashAsync(rename.Removed[i++]), await connector.GetPathByHashAsync(item.Hash));
            break;
        case "rm":
            var remove = result as RemoveResponseModel;
            foreach (string item in remove.Removed)
                Trace.TraceInformation("elFinder removed {name}", await connector.GetPathByHashAsync(item));
            break;
        case "upload":
            var add = result as AddResponseModel;
            foreach (BaseModel item in add.Added.Where(a => a is FileModel))
                Trace.TraceInformation("elFinder added {name}", await connector.GetPathByHashAsync(item.Hash));
            break;
    }
}
```

## Custom Info

elFinder provides extensibility and one example is including additional information in the info dialog.  First add the following to the [commandsOptions](https://github.com/Studio-42/elFinder/wiki/Client-configuration-options-2.1#commandsOptions) section of client configuration options.

```JS
info: {
    custom: {
        created: {
            label: 'Created',
            tpl: '<div class="elfinder-info-created"><span class="elfinder-spinner"></span></div>',
            action: function (file, fm, dialog) {
                fm.request({
                    data: { cmd: 'info', target: file.hash },
                    preventDefault: true
                })
                .done(function (data) {
                    dialog.find('div.elfinder-info-created').html(fm.formatDate(file, data.timestamp));
                })
                .fail(function () {
                    dialog.find('div.elfinder-info-created').html(fm.i18n('unknown'));
                });
            }
        }
    }
}
```

Then add the following before `ProcessAsync()` in the `Connector` action of `FileSystemController` to return the file system created date of the object requested by elFinder.

```C#
if (parameters["cmd"] == "info")
{
    var path = await connector.GetPathByHashAsync(parameters["target"]);

    var dt = path.EndsWith(Path.DirectorySeparatorChar) ? Directory.GetCreationTimeUtc(path) : File.GetCreationTimeUtc(path);
    var ts = (long)dt.Subtract(DateTime.UnixEpoch).TotalSeconds;

    return Json(new { timestamp = ts });
}
```