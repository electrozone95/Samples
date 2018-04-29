<?php
//Comportament: returneaza numele si prenumele persoanelor care se afla in grupuri comune cu o persoana precizada dupa ID-ul ei
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$persoanaID = $_POST["persoanaidPost"];

$sql="SELECT DISTINCT a.Nume,a.Prenume FROM Persoane a
INNER JOIN PersoaneGrupuri c ON (c.PersoanaID = a.PersoanaID)
INNER JOIN PersoaneGrupuri b ON (b.GrupID = c.GrupID) 
INNER JOIN Persoane d ON (d.PersoanaID = c.PersoanaID) AND ( a.PersoanaID = d.PersoanaID )
WHERE (b.PersoanaID = '".$persoanaID."')
ORDER BY a.Nume ASC, a.Prenume ASC";// intersercteaza multimile persoanelor cu acelasi grup id cu al utilizatorului,precizat prin variabila $persoanaID
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