<?php
    include'includes/dbh.inc.php';

    $table = $_POST["table"];
    $username  = $_POST["username"];
    $data = $_POST["data"];

    //add the data to the database
    $insertQuery = "INSERT INTO `". $table ."` (username, data) VALUES ('" . $username . "', '" . $data . "');";
    mysqli_query($conn, $insertQuery) or die ("MySQL_ERROR 4: Insert Query failed") ; 

    echo ("UserData added to the Database");
?>