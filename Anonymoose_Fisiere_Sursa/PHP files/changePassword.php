<?php
//Comportament: returneaza numele si prenumele persoanelor care se afla in grupuri comune cu o persoana precizada dupa ID-ul ei
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$username = $_POST["usernamePost"];
$password = $_POST["parolaPost"];

$sql="UPDATE Persoane
SET Password = '".$password."'
WHERE Username = '".$username."'";
$result = $db->query($sql);
echo "All good!";
$db->close();

?>