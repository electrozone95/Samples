// main game source for tic tac toe game - OLD SCRIPT
#include "Game.h"
#include<stdlib.h>
#include<time.h>

Game::Game( HWND hWnd,const KeyboardServer& kServer )
:	gfx ( hWnd ),
	kbd( kServer ),
	cursorX(1),
	cursorY(1),
	KeysPressedLastFrame(false),
	active_player(X),
	inactive_player(O),
	n_turns(0)

{
	for(int index=0 ; index < 9 ; index++)
	{
		SetSquareState( index , EMPTY );
	}
	srand((unsigned int)time(NULL));
}



void Game::Go()
{
	gfx.BeginFrame();
	ComposeFrame();
	gfx.EndFrame();
}


void Game::DrawXWins( int x,int y )
{
	//code not relevant
}

void Game::DrawOWins( int x,int y )
{
	//code not relevant
}

void Game::DrawTie( int x,int y )
{
	//code not relevant
}

void Game::DrawToilet( int x,int y )
{
	//code not relevant
}

void Game::DrawGrid( int x,int y )
{
	gfx.DrawLine( x + 0,y+99,x+299,y+99,255,255,255);
	gfx.DrawLine( x + 0,y+199,x+299,y+199,255,255,255);
	gfx.DrawLine( x + 99,y+0,x+99,y+299,255,255,255);
	gfx.DrawLine( x + 199,y+0,x+199,y+299,255,255,255);
}

void Game::DrawX( int x, int y )
{
	gfx.DrawLine(x+19,y+19,x+79,y+79,0,0,255);
	gfx.DrawLine(x+19,y+79,x+79,y+19,0,0,255);
}

void Game::DrawO( int x,int y )
{
	gfx.DrawCircle( x+49,y+49,40,255,0,0);
}

void Game::DrawCursor( int x,int y)
{
	gfx.DrawLine(x+2,y+2,x+49,y+2,0,255,0);
	gfx.DrawLine(x+2,y+2,x+2,y+49,0,255,0);
	gfx.DrawLine(x+50,y+97,x+97,y+97,0,255,0);
	gfx.DrawLine(x+97,y+50,x+97,y+97,0,255,0);
}

void Game::DrawEndScreen( int x,int y,XOState state )
{
	DrawToilet(x,y);
	if(state == X)
	{
		DrawXWins( x + 18 , y + 57);
	}
	else if(state == O)
	{
		DrawOWins( x + 18 , y + 57);
	}
	else
	{
		DrawTie( x + 18 , y + 57);
	}
}


Game::XOState Game::GetSquareState(int index)
{
	switch( index )
	{
	case 0:
		return s0;
	case 1:
		return s1;
	case 2:
		return s2;
	case 3:
		return s3;
	case 4:
		return s4;
	case 5:
		return s5;
	case 6:
		return s6;
	case 7:
		return s7;
	case 8:
		return s8;
	default:
		return EMPTY;
	}
}

void Game::SetSquareState( int index,XOState state)
{
	switch( index )
	{
	case 0:
		s0 = state;
		break;
	case 1:
		s1 = state;
		break;
	case 2:
		s2 = state;
		break;
	case 3:
		s3 = state;
		break;
	case 4:
		s4 = state;
		break;
	case 5:
		s5 = state;
		break;
	case 6:
		s6 = state;
		break;
	case 7:
		s7 = state;
		break;
	case 8:
		s8 = state;
		break;
	default:
		break;
	}
}

Game::XOState Game::GetSquareState(int ix,int iy)
{
	int index=iy*3+ix;
		return GetSquareState(index);
}

void Game::SetSquareState( int ix,int iy,XOState state)
{
	int index=iy*3+ix;
	SetSquareState( index,state );
}

void Game::DoUserInput()
{
		if(!KeysPressedLastFrame)
	{
		if(kbd.DownIsPressed()&&cursorY<=1)
		{
			KeysPressedLastFrame = true;
			cursorY++;
		}
		if(kbd.UpIsPressed()&&cursorY>=1)
		{
			KeysPressedLastFrame = true;
			cursorY--;
		}
		if(kbd.LeftIsPressed()&&cursorX>=1)
		{
			KeysPressedLastFrame = true;
			cursorX--;
		}
		if(kbd.RightIsPressed()&&cursorX<=1)
		{
			KeysPressedLastFrame = true;
			cursorX++;
		}

	}
	else if(!(kbd.DownIsPressed()||
			  kbd.UpIsPressed()||
			  kbd.LeftIsPressed()||
			  kbd.RightIsPressed()))
	{
		KeysPressedLastFrame = false;
	}

	if(kbd.EnterIsPressed() && GetSquareState(cursorX,cursorY) == EMPTY)
	{
		SetSquareState(cursorX,cursorY,active_player);
		EndTurn();
	}
}

// when a turn ends we check for winners
void Game::EndTurn()
{
	if(active_player == X)
	{
		active_player = O;
		inactive_player = X;
	}
	else
	{
		active_player = X;
		inactive_player = O;
	}
	n_turns++;
}

Game::XOState Game::CheckForVictory()
{
	if(s0==s1 && s1==s2 && s0!=EMPTY)
	{
		return s0;
	}
	else if(s3==s4 && s4==s5 && s3!=EMPTY)
	{
		return s3;
	}
	else if(s6==s7 && s7==s8 && s6!=EMPTY)
	{
		return s6;
	}
	else if(s0==s3 && s3==s6 && s0!=EMPTY)
	{
		return s0;
	}
	else if(s1==s4 && s4==s7 && s1!=EMPTY)
	{
		return s1;
	}
	else if(s2==s5 && s5==s8 && s2!=EMPTY)
	{
		return s2;
	}
	else if(s0==s4 && s4==s8 && s0!=EMPTY)
	{
		return s0;
	}
	else if(s2==s4 && s4==s6 && s2!=EMPTY)
	{
		return s2;
	}
	else
	{
		return EMPTY;
	}
}

void Game::AIGetNextMoveRandom()
{

	do
	{
	AIMoveX=rand() % 3;
	AIMoveY=rand() % 3;
	}while( GetSquareState(AIMoveX,AIMoveY) != EMPTY );//da valoare aleatorie pt pozitia luata de AI

	for( int i=0;i<3;i++ )
	{	
		for( int j=0;j<3;j++)
		{
			if( GetSquareState(i,j) == EMPTY)
			{
				if ( (GetSquareState(i,j+1) == GetSquareState(i,j+2)&&GetSquareState(i,j+2)==inactive_player&&j==0) ||//jos
					 (GetSquareState(i,j-1) == GetSquareState(i,j-2)&&GetSquareState(i,j-2)==inactive_player&&j==2) ||//sus
					 (GetSquareState(i+1,j) == GetSquareState(i+2,j)&&GetSquareState(i+2,j)==inactive_player&&i==0) ||//dreapta
					 (GetSquareState(i-1,j) == GetSquareState(i-2,j)&&GetSquareState(i-2,j)==inactive_player&&i==2) ||//stanga
					 (GetSquareState(i,j+1) == GetSquareState(i,j-1)&&GetSquareState(i,j-1)==inactive_player&&j==1) ||
					 (GetSquareState(i+1,j) == GetSquareState(i-1,j)&&GetSquareState(i-1,j)==inactive_player&&i==1) ||
					 (GetSquareState(i+1,j+1) == GetSquareState(i+2,j+2)&&GetSquareState(i+2,j+2)==inactive_player&&i==0&&j==0) ||
					 (GetSquareState(i-1,j-1) == GetSquareState(i-2,j-2)&&GetSquareState(i-2,j-2)==inactive_player&&i==2&&j==2) ||
					 (GetSquareState(i+1,j-1) == GetSquareState(i+2,j-2)&&GetSquareState(i+2,j-2)==inactive_player&&i==0&&j==2) ||
					 (GetSquareState(i-1,j+1) == GetSquareState(i-2,j+2)&&GetSquareState(i-2,j+2)==inactive_player&&i==2&&j==0) ||
					 (GetSquareState(i-1,j-1) == GetSquareState(i+1,j+1)&&GetSquareState(i+1,j+1)==inactive_player&&i==1&&j==1) ||
					 (GetSquareState(i-1,j+1) == GetSquareState(i+1,j-1)&&GetSquareState(i+1,j-1)==inactive_player&&i==1&&j==1))
				{
					AIMoveX=i;
					AIMoveY=j;
				}
			}
		}
	}

	for( int i=0;i<3;i++ )
	{	
		for( int j=0;j<3;j++)
		{
			if( GetSquareState(i,j) == EMPTY)
			{
				if ( (GetSquareState(i,j+1) == GetSquareState(i,j+2)&&GetSquareState(i,j+2)==active_player&&j==0) ||//jos
					 (GetSquareState(i,j-1) == GetSquareState(i,j-2)&&GetSquareState(i,j-2)==active_player&&j==2) ||//sus
					 (GetSquareState(i+1,j) == GetSquareState(i+2,j)&&GetSquareState(i+2,j)==active_player&&i==0) ||//dreapta
					 (GetSquareState(i-1,j) == GetSquareState(i-2,j)&&GetSquareState(i-2,j)==active_player&&i==2) ||//stanga
					 (GetSquareState(i,j+1) == GetSquareState(i,j-1)&&GetSquareState(i,j-1)==active_player&&j==1) ||
					 (GetSquareState(i+1,j) == GetSquareState(i-1,j)&&GetSquareState(i-1,j)==active_player&&i==1) ||
					 (GetSquareState(i+1,j+1) == GetSquareState(i+2,j+2)&&GetSquareState(i+2,j+2)==active_player&&i==0&&j==0) ||
					 (GetSquareState(i-1,j-1) == GetSquareState(i-2,j-2)&&GetSquareState(i-2,j-2)==active_player&&i==2&&j==2) ||
					 (GetSquareState(i+1,j-1) == GetSquareState(i+2,j-2)&&GetSquareState(i+2,j-2)==active_player&&i==0&&j==2) ||
					 (GetSquareState(i-1,j+1) == GetSquareState(i-2,j+2)&&GetSquareState(i-2,j+2)==active_player&&i==2&&j==0) ||
					 (GetSquareState(i-1,j-1) == GetSquareState(i+1,j+1)&&GetSquareState(i+1,j+1)==active_player&&i==1&&j==1) ||
					 (GetSquareState(i-1,j+1) == GetSquareState(i+1,j-1)&&GetSquareState(i+1,j-1)==active_player&&i==1&&j==1))
				{
					AIMoveX=i;
					AIMoveY=j;
				}
			}
		}
	}

}

// AI does random move when there's no best move
void Game::DoAITurnRandom()
{
	AIGetNextMoveRandom();
	SetSquareState( AIMoveX,AIMoveY,active_player );
	EndTurn();
}

// draw the whole frame(grid+checked spaces)
void Game::ComposeFrame()
{
	const int baseX=250;
	const int baseY=150;
	const int square_size=100;

	XOState victory_state=CheckForVictory();

	if(victory_state == EMPTY && n_turns<9)
	{
		if(active_player == X)
		{
			DoAITurnRandom();
		}
		else
		{
			DoUserInput();
		}
		DrawCursor(baseX+cursorX*square_size,baseY+cursorY*square_size);
	}
	else
	{
		DrawEndScreen( 358,495,victory_state );
	}

	DrawGrid(baseX,baseY);
	for(int iy=0;iy<3;iy++)
	{
		for(int ix=0;ix<3;ix++)
		{
			if(GetSquareState(ix,iy)==X)
			{
				DrawX( baseX + ix*square_size,baseY + iy*square_size );
			}
			else if(GetSquareState(ix,iy)==O)
			{
				DrawO( baseX + ix*square_size,baseY + iy*square_size );
			}
		}
	}

}