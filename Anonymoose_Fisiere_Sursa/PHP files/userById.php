<?php
//Comportament: afiseaza informtiile unui utilizator in functie de id-ul sau
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$persoanaID = $_POST["persoanaidPost"];

//echo"great work!!!";

$sql1="SELECT * FROM Persoane WHERE PersoanaID='".$persoanaID."';";
$result = $db->query($sql1);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo "PersoanaID:". $row["PersoanaID"].";<br>". "Nume:" .
		$row["Nume"].";<br>"."Prenume:". $row["Prenume"].";<br>"."Data nasterii:".
		$row["DataNasterii"].";<br>"."Sex:". $row["Sex"].";<br>"."Locatie:".
		$row["Locatie"].";<br>"."Serie:". $row["Serie"].";<br>"."Grupa:".
		$row["Grupa"].";<br>"."AvatarID:". $row["AvatarID"].";<br>"."Username:". 
		$row["Username"].";<br>"."Password:". $row["Password"].";<br>"."FacultateID:". $row["FacultateID"]. ";";
    }
} else {
    echo "error;";
}
$db->close();

?>