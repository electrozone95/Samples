<?php
//Comportament: sterge persoana cu id-ul precizat
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$persID = $_POST["persoanaidPost"];

$sql="DELETE FROM Persoane WHERE PersoanaID='".$persID."';";
$result = $db->query($sql);
$db->close();