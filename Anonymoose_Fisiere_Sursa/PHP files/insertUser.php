<?php
//Inseram un utilizator in tabelul Persoane
//Variabile logare
$servername = 'localhost';
$serveruser = 'root';
$serverpass = '';
$db = 'socialnet';

//Variabile ce preiau informatia despre noul utilizator din interfata
$nume = $_POST["numePost"];
$prenume = $_POST["prenumePost"];
$datanasterii = $_POST["datanasteriiPost"];
$sex = $_POST["sexPost"];
$locatie = $_POST["locatiePost"];
$serie = $_POST["seriePost"];
$grupa = $_POST["grupaPost"];
$avatarid = $_POST["avataridPost"];
$facultateid = $_POST["facultateidPost"];
$username = $_POST["usernamePost"];
$password = $_POST["parolaPost"];



$db = new mysqli($servername,$serveruser,$serverpass,$db) or die("Unable to connect");
//echo"great work!!!";

$sql1="INSERT INTO Persoane(Nume,Prenume,DataNasterii,Sex,Locatie,Serie,Grupa,AvatarID,FacultateID,Username,Password) VALUES ('".$nume."','".$prenume."','".$datanasterii."','".$sex."','".$locatie."','".$serie."','".$grupa."','".$avatarid."','".$facultateid."','".$username."','".$password."')";
$result = mysqli_query($db,$sql1);// verificam daca s-a realizat inserarea
if(!$result)
{
	echo "ERROR: query failed!!!";
}
else
{
	echo "All good!";
}



$db->close();

?>