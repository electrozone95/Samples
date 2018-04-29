`timescale 1ns / 1ps
//TINE MINTE: dreapta e relativa, se schimba cu fiecare miscare pe care o faci. daca mergi intr-o directie schimba dreapta
module maze(
 		clk,
 	starting_col, starting_row, 		// indicii punctului de start
		maze_in, 							// oferă informații despre punctul de coordonate [row, col]
 row, col, 							// selectează un rând si o coloană din labirint
	maze_oe,							// output enable (activează citirea din labirint la rândul și coloana date) - semnal sincron	
	maze_we, 							// write enable (activează scrierea în labirint la rândul și coloana  date) - semnal sincron
		done);		 						// ieșirea din labirint a fost gasită; semnalul rămane activ 

input 		clk;
input [5:0] 	starting_col;
input [5:0] 	starting_row;
input		maze_in;
output reg[5:0] row;
output reg[5:0] col;
output reg maze_oe;
output reg maze_we;
output reg done;

//TODO implementare
`define init			'd0        
`define read			'd1// citim valoarea dupa scriere pentru verificare si inainte de aceasta
`define waiting		'd2// stare intermediara pentru a astepta sa citeasca valorile( cum maze este executate inainte de maze_structure)        
`define write			'd3// scriem valoarea 2 in celula curenta daca nu este ocupata de perete         
`define go_down		'd4          
`define go_right		'd5         
`define go_up			'd6       
`define go_left		'd7

reg [2 : 0] cstate = `init, next_state, go_state, prev_state;//go_state ne indica directia care urmeaza sa fie urmata iar prev_state, ultima directie scrisa(parcursa)
reg isGoingBack;// tells us if we are going back
	
always @(posedge clk) begin
    if(!done) begin
        cstate <= next_state;
	 end
end

always @(*) begin

	 case(cstate)
		`init: begin 
			isGoingBack = 0;
			done = 0;
			row = starting_row;
			col = starting_col;
			go_state = `waiting;
			prev_state = `init;
			next_state = `read;
			maze_we = 1;
			maze_oe = 1;
		end
		`read: begin
			maze_we = 0;
			maze_oe = 1;
			next_state = `write;
		end
		`waiting: begin
			case(go_state)
				`go_up: begin
					next_state = `go_up;
				end
				`go_down: begin
					next_state = `go_down;
				end
				`go_left: begin
					next_state = `go_left;
				end
				`go_right: begin
					next_state = `go_right;
				end
			endcase
		end
		`write: begin
			if( prev_state == `go_right || prev_state == `init) begin
				/*if( isGoingBack == 1 ) begin
					isGoingBack = 0;
				end
				else begin*/
					row = row + 1;
				//end
				if( row > 63 ) begin
					done = 1;// algoritmul gaseste iesirea cand unul dintre indicii de pozitie ai celulei curente(row/col) depaseste dimensiunea tabelului
				end
				else begin
					next_state = `waiting;
					go_state = `go_down;
					maze_we = 0;
					maze_oe = 1;
				end
			end
			else if( prev_state == `go_down ) begin
				/*if( isGoingBack == 1 ) begin
					isGoingBack = 0;
				end
				else begin*/
					col = col - 1;
				//end
				if( col < 0 ) begin
					done = 1;
				end
				else begin
					next_state = `waiting;
					go_state = `go_left;
					maze_we = 0;
					maze_oe = 1;
				end
			end
			else if( prev_state == `go_left )begin
				/*if( isGoingBack == 1 ) begin
					isGoingBack = 0;
				end
				else begin*/
					row = row - 1;
				//end
				if( row < 0 ) begin
					done = 1;
				end
				else begin
					next_state = `waiting;
					go_state = `go_up;
					maze_we = 0;
					maze_oe = 1;
				end
			end
			else if( prev_state == `go_up )begin
				/*if( isGoingBack == 1 ) begin
					isGoingBack = 0;
				end
				else begin*/
					col = col + 1;
				//end
				if( col > 63 ) begin
					done = 1;
				end
				else begin
					next_state = `waiting;
					go_state = `go_right;
					maze_we = 0;
					maze_oe = 1;
				end
			end
		end
		`go_down: begin// pentru fiecare din starile de deplasare(go_down/right/up/left) va incerca sa le parcurga daca sunt goale sau
			if( maze_in != 1 ) begin
				maze_we = 1;
				maze_oe = 1;
				prev_state = `go_down;
				next_state = `write;
			end
			else begin// sau sa se mute in urmatoarea pozitie la dreapta fata de pozitia anterioara
				if( col == 63 ) begin
					done = 1;
				end
				else begin
					next_state = `waiting;
					go_state = `go_right;
					maze_oe = 1;
					maze_we = 0;
					if(prev_state == `go_left) begin
						col = col + 1;
						prev_state = `go_right;
						go_state = `go_down;
						isGoingBack = 1;
					end 
					else begin
						row = row - 1;
						col = col + 1;
						go_state = `go_right;
					end
				end
			end
		end
		`go_right: begin
			if( maze_in != 1 ) begin
				maze_we = 1;
				maze_oe = 1;
				prev_state = `go_right;
				next_state = `write;
			end
			else begin
				if( row == 0 ) begin
					done = 1;
				end
				else begin
					next_state = `waiting;
					maze_oe = 1;
					maze_we = 0;
					if(prev_state == `go_down) begin
						row = row - 1;
						prev_state = `go_up;
						go_state = `go_right;
						isGoingBack = 1;
					end 
					else begin
						row = row - 1;
						col = col - 1;
						go_state = `go_up;
					end
				end
			end
		end
		`go_up: begin
			if( maze_in != 1 ) begin
				maze_we = 1;
				maze_oe = 1;
				prev_state = `go_up;
				next_state = `write;
			end
			else begin
				if( col == 0 ) begin
					done = 1;
				end
				else begin
					next_state = `waiting;
					maze_oe = 1;
					maze_we = 0;
					if(prev_state == `go_right) begin
						col = col - 1;
						prev_state = `go_left;
						go_state = `go_up;
						isGoingBack = 1;
					end 
					else begin
						go_state = `go_left;
						row = row + 1;
						col = col - 1;
					end
				end
			end
		end
		`go_left: begin
			if( maze_in != 1 ) begin
				maze_we = 1;
				maze_oe = 1;
				prev_state = `go_left;
				next_state = `write;
			end
			else begin
				if( row == 63 ) begin
					done = 1;
				end
				else begin
					next_state = `waiting;
					maze_oe = 1;
					maze_we = 0;
					if(prev_state == `go_right) begin
						row = row + 1;
						prev_state = `go_down;
						go_state = `go_left;
						isGoingBack = 1;
					end 
					else begin
						go_state = `go_down;
						row = row + 1;
						col = col + 1;
					end
				end
			end
		end
		
	 endcase
end

endmodule