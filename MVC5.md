# ASP.NET MVC 5

The example controller below demonstrates the usage of the .NET Standard library within MVC 5 projects.

```C#
[RoutePrefix("el-finder/file-system")]
public class FileSystemController : Controller
{
    [HttpGet, Route("connector")]
    public async Task<ActionResult> Connector()
    {
        var connector = GetConnector();

        var parameters = Request.QueryString.Keys.Cast<string>().ToDictionary(k => k, v => (StringValues)Request.QueryString[v]);

        var result = (await connector.ProcessAsync(parameters)).Value;
        if (result is FileContent)
        {
            var file = result as FileContent;
            return File(file.ContentStream, file.ContentType);
        }
        else
        {
            return Content(JsonSerializer.Serialize(result), "application/json");
        }
    }

    [HttpPost, Route("connector")]
    public async Task<ActionResult> ConnectorPost()
    {
        var connector = GetConnector();

        var parameters = Request.Form.Keys.Cast<string>().ToDictionary(k => k, v => (StringValues)Request.Form[v]);

        if (Request.Files.Count > 0)
        {
            var files = new List<FileContent>();
            //https://stackoverflow.com/questions/1760510/foreach-on-request-files
            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];
                files.Add(new FileContent
                {
                    Length = file.ContentLength,
                    ContentStream = file.InputStream,
                    ContentType = file.ContentType,
                    FileName = file.FileName
                });
            }
            return Content(JsonSerializer.Serialize((await connector.ProcessAsync(parameters, files)).Value), "application/json");
        }
        else
        {
            return Content(JsonSerializer.Serialize((await connector.ProcessAsync(parameters)).Value), "application/json");
        }
    }

    [HttpGet, Route("thumb/{hash}")]
    public async Task<ActionResult> Thumbs(string hash)
    {
        var connector = GetConnector();

        var result = (await connector.GetThumbnailAsync(hash)).Value;
        if (result is ImageWithMimeType)
        {
            var file = result as ImageWithMimeType;
            return File(file.ImageStream, file.MimeType);
        }
        else
        {
            return Content(JsonSerializer.Serialize(result), "application/json");
        }
    }

    private Connector GetConnector()
    {
        var driver = new FileSystemDriver();

        var root = new RootVolume(
            Server.MapPath("~/Files"),
            $"{Request.Url.Scheme}://{Request.Url.Authority}/Files/",
            $"{Request.Url.Scheme}://{Request.Url.Authority}/el-finder/file-system/thumb/")
        {
            //IsReadOnly = !User.IsInRole(AccountController.SuperUser)
            IsReadOnly = false, // Can be readonly according to user's membership permission
            IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
            Alias = "Files", // Beautiful name given to the root/home folder
            //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
            //LockedFolders = new List<string>(new string[] { "Folder1" })
        };

        driver.AddRoot(root);

        return new Connector(driver)
        {
            // This allows support for the "onlyMimes" option on the client.
            MimeDetect = MimeDetectOption.Internal
        };
    }
}
```