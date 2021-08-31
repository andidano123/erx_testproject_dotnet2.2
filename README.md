# ERX test project, dotnet core 2.2.

## How to run it locally
* Install SQL Server
* Create Db as name 'SampleApi'
* Restore Db dump file SampleApi001_DataSeed.sql in 'Database' directory
* Configure the SQL server connection string.
* Start the service
```
dotnet run
```
* Verify the service is running. Open your favorite browser and navigate to follow link.
```
https://localhost:5001/swagger
```

You can find simple test frontend web page in test directory.
```
//index.html
...
var current_qtype = 0;
    var api_url = "http://localhost:27224";   // you can change this url.
    $("#btn_start").click(function(){        
        var email = $("#txt_email").val();
...
```
