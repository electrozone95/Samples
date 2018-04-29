<?php
//Cauta si returneaza informatiile persoanei in functie de numele ei de utilizator si parola
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';// baza de date folosita

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$username = $_POST["usernamePost"];// numele de utilizator primit din interfata
$password = $_POST["parolaPost"];// parola primita din interfata

//echo"great work!!!";

$sql1="SELECT * FROM Persoane WHERE Password='".$password."'AND Username='".$username."';";
$result = $db->query($sql1);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {// luam informatia rezultata si o formatam pentru a putea fi preluata si "inteleasa" de programul nostru
        echo "<br><br>"."PersoanaID:". $row["PersoanaID"].";<br>". "Nume:" .
		$row["Nume"].";<br>"."Prenume:". $row["Prenume"].";<br>"."Data nasterii:".
		$row["DataNasterii"].";<br>"."Sex:". $row["Sex"].";<br>"."Locatie:".
		$row["Locatie"].";<br>"."Serie:". $row["Serie"].";<br>"."Grupa:".
		$row["Grupa"].";<br>"."AvatarID:". $row["AvatarID"].";<br>"."Username:". 
		$row["Username"].";<br>"."Password:". $row["Password"].";<br>"."FacultateID:". $row["FacultateID"]. ";";// se observa ca la finalul fiecarui rand avem ";"
    }
} else {
    echo "error;";
}
$db->close();

?>