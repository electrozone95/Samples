<?php
//FISIER DE TEST: afiseaza toti utilizatorii din tabelul Persoane
//Variabile logare
$user = 'root';
$pass = '';
$db = 'socialnet';


$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");
//echo"great work!!!";


$sql1="SELECT * FROM Persoane";
$result = $db->query($sql1);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo "<br><br>"."PersoanaID: ". $row["PersoanaID"].";<br>". "Nume: " .
		$row["Nume"].";<br>"."Prenume: ". $row["Prenume"].";<br>"."Data nasterii: ".
		$row["DataNasterii"].";<br>"."Sex: ". $row["Sex"].";<br>"."Locatie: ".
		$row["Locatie"].";<br>"."Serie: ". $row["Serie"].";<br>"."Grupa: ".
		$row["Grupa"].";<br>"."AvatarID: ". $row["AvatarID"].";<br>"."Username: ". 
		$row["Username"].";<br>"."Password: ". $row["Password"]. ";";
		
		$ind = $row["FacultateID"];
		$sql2="SELECT Nume FROM Facultati WHERE FacultateID = $ind";
		$result2 = $db->query($sql2);
		$row2 = $result2->fetch_assoc();
		echo "<br>Facultate: " . $row2["Nume"] . ";";
    }
} else {
    echo "0 results";
}
$db->close();

?>