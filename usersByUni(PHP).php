<?php
//Comportament: returneaza numele si prenumele persoanelor care se gasesc la facultatea, seria si grupa precizate
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$grupa = $_POST["grupaPost"];
$serie = $_POST["seriePost"];
$facultateID = $_POST["facultateidPost"];

$sql="SELECT a.Nume,a.Prenume FROM Persoane a
INNER JOIN Persoane b ON (b.FacultateID = '".$facultateID."') 
INNER JOIN Persoane c ON (c.Serie = '".$serie."') 
INNER JOIN Persoane d ON (d.Grupa = '".$grupa."') 
WHERE (a.PersoanaID = b.PersoanaID) AND (a.PersoanaID = c.PersoanaID) AND (a.PersoanaID = d.PersoanaID) 
ORDER BY a.Nume ASC, a.Prenume ASC";
$result = $db->query($sql);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo "Nume:". $row["Nume"].";<br>"."Prenume:".
		$row["Prenume"] . ";<br>";
    }
} else {
    echo "error;";
}
$db->close();

?>