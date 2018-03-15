//Efficient functions for drawing lines and circles written in C++

void D3DGraphics::DrawLine(int x1,int y1,int x2,int y2,int r,int g,int bl)
{		
	int dx=x2-x1;
	int dy=y2-y1;
	if(abs(dy)>abs(dx))
	{
		if(y1>y2)
		{
			int temp=y1;
			y1=y2;
			y2=temp;
			temp=x1;
			x1=x2;
			x2=temp;
		}
		float m=(float)dx/(float)dy;
		float b=x1-m*y1+0.5f;
		for( int y = y1 ; y <= y2 ; y++ )
		{
			int x=m*y+b;
			PutPixel(x,y,r,g,bl);
		}
	}
	else
	{
		if(x1>x2)
		{
			int temp=y1;
			y1=y2;
			y2=temp;
			temp=x1;
			x1=x2;
			x2=temp;
		}
		float m=(float)dy/(float)dx;
		float b=y1-m*x1+0.5f;
		for( int x = x1 ; x <= x2 ; x++ )
		{
			int y=m*x+b;
			PutPixel(x,y,r,g,bl);
		}
	}
}

void D3DGraphics::DrawCircle( int cx,int cy,int r,int re,int g,int b )
{
	int i,j;
	float radsquare=pow((double)r,2.0);
	float i0=0.7071068f*r+0.5f;
	for(i=0;i<=i0;i++)
	{
		j=sqrt(radsquare-pow((double)i,2.0))+0.5f;
				PutPixel(cx+i,cy+j,re,g,b);
				PutPixel(cx+i,cy-j,re,g,b);
				PutPixel(cx-i,cy+j,re,g,b);
				PutPixel(cx-i,cy-j,re,g,b);
				PutPixel(cx+j,cy+i,re,g,b);
				PutPixel(cx-j,cy+i,re,g,b);
				PutPixel(cx+j,cy-i,re,g,b);
				PutPixel(cx-j,cy-i,re,g,b);
	}
}