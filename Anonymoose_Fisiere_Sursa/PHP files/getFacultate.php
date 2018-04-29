<?php
//Returneaza numele facultatii in functie de id-ul acesteia
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$facID = $_POST["facultateidPost"];

$sql1="SELECT * FROM Facultati WHERE FacultateID='".$facID."';";
$result = $db->query($sql1);
if ($result->num_rows > 0) {
	$row = $result->fetch_assoc();
	echo $row["Nume"];
} else {
    echo "error;";
}
$db->close();