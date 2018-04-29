Girtofan-Pop Victor Cristian, 334AC
Algoritmul ce rezolva labirintul este de tipul Wall follower( urmeaza peretele de la dreapta). L-am implementat cu urmatoarele 8 stari:
0: init - initializam pozitia, state-urile actuale, urmatoare si ulterioare, verificatorii si semnalele de enable/disable
1: read - dezactivam scrierea celulei curente si activam citirea acesteia
2: waiting - asteptam un semnal de ceas dupa schimbarea pozitiei pentru citire
3: write - dezactivam citirea celulei curente si activam scrierea acesteia
4 -> 7: go_down/right/up/left - mutam indicii de pozitie intr-una dintre celulele vecine pentru a verifica daca este perete sau cale libera

Flow: Algoritmul verifica pozitia curenta; daca este perete, incearca urmatoarea pozitie la dreapta fata de pozitia anterioara. In caz contrar, scrie(parcurge) celula actuala, actualizeaza directia de mers( retinand in 'prev_state' ultimul state de deplasare) si se deplaseaza in urmatoarea celula la dreapta fata de directia actualizata. La realizarea fiecarei deplasari, algoritmul verifica daca indicii depasesc dimensiunea tabelului pentru a stii cand a gasit iesirea, caz in care 'done' devine 1 ).