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

Test frontend web page can find in test directory.

