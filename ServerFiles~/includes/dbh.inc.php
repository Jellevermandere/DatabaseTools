<?php

$origin = $_SERVER['HTTP_ORIGIN'];
$allowed_domains = [
    // add any cross domains here
];

if (in_array($origin, $allowed_domains)) {
    header('Access-Control-Allow-Origin: ' . $origin);
}

$dbServername = "";//Database  server name;
$dbUsername =  "";//Database username;
$dbPassword = "";//Database password;
$dbName = "";//Database name;

$conn = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

if(mysqli_connect_errno()){
    echo 'MySQL_ERROR 1: Connection Failed';
    exit();
}

?>