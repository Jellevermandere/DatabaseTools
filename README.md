# Database Tools
A package to communicate from Unity to a mysql database using PHP

```cs
 namespace JelleVer.DatabaseTools
```


## Installation

This can be imported as a UnityPackage in any existing Unity project through the [Package manager](https://docs.unity3d.com/Manual/Packages.html) with the Git url.

## Documentation

You can find the full documentation here: [jellevermandere.github.io/DatabaseTools](https://jellevermandere.github.io/DatabaseTools)


## Unity side

The connection is made through the `DB_Connect`script. 
Fill in the correct url's and table name in the inspector.
There are callbacks in the form of UnityEvents when a request is completed

### Uploading data

Data can be send to the database using:

```cs
public void TryUploadData(string username, Object data){}
public void TryUploadData(string username, string data){}
```
Where the username is used to identify the player and the data can be anything you want.
If the data is passed as an object make sure it's `[System.Serializable]`, it will get serialized to JSON before being send.

### Downloading data

The data can be downloaded using:
```cs
 public void TryDownloadData(){}
```
It will return all the entries in the database as an array of:
```cs
public class UserData
{
  public string username = "";
  public string data = "";
}
```

The data is received as a `UserDataArray` as a wrapper class, but the UnityEvent passes a regular `UserData[]`


## Server side

You will need a mysql database and a server that supports PHP.
Fill in the database credentials in `dbh.inc.php` in the `ServerFiles`folder:
```php
$dbServername = "";//Database  server name;
$dbUsername =  "";//Database username;
$dbPassword = "";//Database password;
$dbName = "";//Database name;
```
Put the whole folder on your server. and fill in the link in the `DB_Connect` in Unity.

## Licensing

The code in this project is licensed under MIT license.