<?php
//Comportament: insereaza un raspuns in lista de raspunsuri, in functie de id-ul unei discutii, al unui utilizator si un mesaj
//Variabile logare
$servername = 'localhost';
$serveruser = 'root';
$serverpass = '';
$db = 'socialnet';

//Variabile program
$threadID = $_POST["threadidPost"];
$OPID = $_POST["opidPost"];// poate fi nul, caz in care cel ce posteaza este marcat cu "Anonymoose"
$mesaj = $_POST["mesajPost"];



$db = new mysqli($servername,$serveruser,$serverpass,$db) or die("Unable to connect");

$sql="INSERT INTO Raspunsuri(ThreadID,PosterID,Mesaj) VALUES ('".$threadID."','".$OPID."','".$mesaj."')";
$result = mysqli_query($db,$sql);
if(!$result)
{
	echo "error;";
}
else
{
	echo "All good!";
}



$db->close();

?>