<?php

    include'includes/dbh.inc.php';
    include'includes/userData.inc.php';

    $table = $_GET["table"];

    // Check if there are any columns in the table
    $columnCheckQuery = "SHOW COLUMNS FROM ". $table;
    $columns = mysqli_query($conn, $columnCheckQuery) or die("MySQL_ERROR 5: ColumnCheck Query failed") ;

    // if there are no columns return an error
    if (mysqli_num_rows($columns) == 0) {
        die( "MySQL_ERROR 6: Could not find any columns in table");
    }

    // Check if there are any entries in the table
    $selectQuery = "SELECT * FROM ". $table;
    $select = mysqli_query($conn, $selectQuery) or die("MySQL_ERROR 6: RowSelect Query failed");

    if(mysqli_num_rows($select) > 0){

        // create a new array of UserData to send as a response
        $userDataArray = new UserDataArray();

        while($row = mysqli_fetch_assoc($select)){
            $newUserData = new UserData();
            $newUserData->set_userData($row["username"],$row ["data"]);
            $userDataArray->add_element($newUserData); // add the element to the list
        }
        echo json_encode($userDataArray);
    }

    else{
        echo("MySQL_ERROR 7: No entries in table");
    }

    exit();

?>
