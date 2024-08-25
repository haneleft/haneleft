package application;

import java.io.FileInputStream;
import java.util.Random;

import javafx.geometry.Point2D;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;

//urcuje jak se budou kostky vykreslovat
public class Shape {
	private int type;
	private Point2D position;
	private double width;
	private double height;
	private int random;
	private boolean left;
	private boolean right;
	
	public Shape(Point2D position, int type, double width, double height, boolean rand) {
		this.type = type;
		this.position = position;
		this.width = width;
		this.height = height;
		if(rand == true) {
			Random r =  new Random();
			random = r.nextInt(2);
		}
		else {
			random = 0;
		}
		this.left = false;
		this.right = false;
	}
	
	//vykresluje tvar kostek
	public void drawShape(GraphicsContext gc) {
		if(type == 1 && random == 0) {
			gc.fillRect(position.getX(), position.getY(), width - width/5 - width/10, height/5);
			
			gc.fillRect(position.getX(), position.getY() + height/5, width/10, height/4);
			gc.fillRect(position.getX() + width/5, position.getY() + height/5, width/5, height/3);
			gc.fillRect(position.getX() + (width/5)*2 + width/10, position.getY() + height/5, width/5 + width/10, height/3);
			
			gc.fillRect(position.getX() + width/10, position.getY() + (height/2), width/10, height/3);
			gc.fillRect(position.getX() + (width/5)*2, position.getY() + (height/2), width/5, height/3);
			gc.fillRect(position.getX() + (width/5)*4, position.getY() + (height/2), width/10, height/2 - (height/12));
		}
		else if(type == 1 && random == 1) {
			gc.fillRect(position.getX(), position.getY(), width - (width/5)*4, height/5);
			gc.fillRect(position.getX() + (width/5)*3, position.getY(), width/5 + width/10, height/5);
			
			gc.fillRect(position.getX() + width/5, position.getY() + height/5, width/5, height/3);
			gc.fillRect(position.getX() + (width/5)*2 + width/10, position.getY() + height/5, width/5 + width/10, height/3);
			
			gc.fillRect(position.getX(), position.getY() + (height/2), width/5, height/3);
			gc.fillRect(position.getX() + (width/5)*2, position.getY() + (height/2), (width/5)*3 - width/10, height/2 - (height/12));
			gc.fillRect(position.getX() + (width/5)*4, position.getY() + (height/2), width/10, height/2 - (height/12));
		}
		else if(type == 2){
			gc.fillRect(position.getX(), position.getY(), width - width/5 - width/10, height/5);
			
			gc.fillRect(position.getX(), position.getY() + height/5, width/10, height/4);
			gc.fillRect(position.getX() + width/5, position.getY() + height/5, width/5, height/3);
			gc.fillRect(position.getX() + (width/5)*2 + width/10, position.getY() + height/5, width/5 + width/10, height/3);
			
			gc.fillRect(position.getX() + width/10, position.getY() + (height/2), width/10, height/3);
			gc.fillRect(position.getX() + (width/5)*2, position.getY() + (height/2), width/5, height/3);
			gc.fillRect(position.getX() + (width/5)*4, position.getY() + (height/2), width/10, height/2 - (height/12));
			
		}
		else if(type == 3 && random == 0){
			gc.fillRect(position.getX(), position.getY(), (width/5)*2 , (height/3 - height/5)  + height/3 + height/10);
			gc.fillRect(position.getX() + (width/5)*2 + width/10, position.getY(), (width/5)*3 - width/10 , (height/3 - height/5) + height/3 + height/10);
			
			gc.fillRect(position.getX(), position.getY()  +( height/3)*2, width - width/5, height/3);
			gc.fillRect(position.getX() + (width/5)*4 + width/10 , position.getY()  +( height/3)*2, width - (width/10)*9, height/3);
		}
		else if(type == 3 && random == 1){
			gc.fillRect(position.getX(), position.getY(), width, height/10);
			gc.fillRect(position.getX() + width/10, position.getY() + height/10, width - width/5, height/10);
			
			gc.fillRect(position.getX() + width/5, position.getY() + height/5, width - (width/5)*2, (height/10)*6);
			
			gc.fillRect(position.getX() + width/10, position.getY() + (height/10)*8, width - width/5, height/10);
			gc.fillRect(position.getX(), position.getY()  +( height/10)*9, width, height/10);
		}
		else if(type == 4) {
			gc.fillRect(position.getX() + width/5, position.getY(), width - width/5 - width/10, height/10);
			
			gc.fillRect(position.getX() + width/10, position.getY() + height/10, (width/10), (height/10)*3);
			gc.fillRect(position.getX() + width - width/10, position.getY() + height/10, (width/10), (height/10)*3);
			
			gc.fillRect(position.getX() + width/5, position.getY() + (height/10)*4, width - width/5 - width/10, height/10);
			
			gc.fillRect(position.getX() + width - (width/10)*3, position.getY() + height/2 + height/10, (width/10), height-height/2 - height/10);
			gc.fillRect(position.getX() + (width/10)*3, position.getY() + height/2 + height/5, (width/10)*3, height/10);
			gc.fillRect(position.getX() + (width/10)*3, position.getY() + height/2 + (height/5)*2, (width/10)*3, height/10);
		}
		else if(type == 5) {
			gc.fillRect(position.getX(), position.getY(), width, height - 2);
		}
		else if(type == 6) {
			gc.fillRect(position.getX(), position.getY(), width, height/2);
		}
		else if(type == 7){
			gc.fillRect(position.getX(), position.getY(), width, height);
		}
		else if(type == 9) {
			gc.fillRect(position.getX(), position.getY(), width, height/3);
		}
		else if(type == 10) {
			gc.fillRect(position.getX(), position.getY(), width/3, height/3);
		}
		else {
			gc.fillRect(position.getX(), position.getY(), width, height);
		}
	}
	
	//vykresluje kostky podle specificke pozice (pouzite jen pro klic)
	public void drawMadShape(GraphicsContext gc, double x, double y) {
		if(type == 4) {
			gc.fillRect(x + width/5, y, width - width/5 - width/10, height/10);
			
			gc.fillRect(x + width/10, y + height/10, (width/10), (height/10)*3);
			gc.fillRect(x + width - width/10, y + height/10, (width/10), (height/10)*3);
			
			gc.fillRect(x + width/5, y + (height/10)*4, width - width/5 - width/10, height/10);
			
			gc.fillRect(x + width - (width/10)*3, y + height/2 + height/10, (width/10), height-height/2 - height/10);
			gc.fillRect(x + (width/10)*3, y + height/2 + height/5, (width/10)*3, height/10);
			gc.fillRect(x+ (width/10)*3, y + height/2 + (height/5)*2, (width/10)*3, height/10);
		}
	}
	
	//pomoci teto metody simuluje vykreslovani nepratel
	public void updatePosition(Point2D position) {
		this.position = position;
	}
	
	//vykresluje postavu
	public void drawPerson(GraphicsContext gc, Color col) {
		if(type == 8 && ((left && right) || (!left && !right))) {
			
			gc.setFill(Color.RED);
			gc.fillRect(position.getX(), position.getY() + height/3 + height/12, width, height - (height/3 + height/12));
			gc.setFill(col);
			
			gc.fillRect(position.getX() + width/4, position.getY(), width/2, height/2);
			
			gc.setFill(Color.RED);
			gc.fillRect(position.getX() + width/4, position.getY()+height/5, width/5, height/5);
			gc.fillRect(position.getX() + width/4 + width/2 - width/5, position.getY()+height/5, width/5, height/5);
			gc.setFill(col);
			
			gc.fillRect(position.getX(), position.getY() + height/3, width, height/12);
			gc.fillRect(position.getX()+ width/2 - width/10, position.getY() + height/2, width/5, height/2);
			
			gc.fillRect(position.getX(), position.getY() + (height/3)*2, width/5, height/3);
			gc.fillRect(position.getX() + (width/5)*4, position.getY() + (height/3)*2, width/5, height/3);
		}
		else if(type == 8 && left){
			gc.fillRect(position.getX(), position.getY(), width/2, height/2);
			
			gc.setFill(Color.RED);
			gc.fillRect(position.getX() + width/5, position.getY()+height/5, width/5, height/5);
			gc.fillRect(position.getX() + width/2 , position.getY() + height/2, width/4, height/2);
			gc.setFill(col);
			
			gc.fillRect(position.getX() + width/3, position.getY() + height/3, width/3, height/3);
			gc.fillRect(position.getX() + (width/5)*3, position.getY() + height/3, width/5, (height/3)*2);
			
			gc.fillRect(position.getX(), position.getY() + (height/3)*2, width/5, height/3);
		}
		else if(type == 8 && right) {
			gc.fillRect(position.getX() + width/2, position.getY(), width/2, height/2);
			
			gc.setFill(Color.RED);
			gc.fillRect(position.getX() + width/2 + width/5, position.getY()+height/5, width/5, height/5);
			gc.fillRect(position.getX() + width/2 - width/5, position.getY() + height/2, width/4, height/2);
			gc.setFill(col);
			
			gc.fillRect(position.getX() + width/3, position.getY() + height/3, width/3 + width/10, height/3);
			gc.fillRect(position.getX() + width/3 - width/10, position.getY() + height/3, width/5, (height/3)*2);
			
			gc.fillRect(position.getX() + (width/5)*4, position.getY() + (height/3)*2, width/5, height/3);
		}
		
	}
	
	//get,set
	
	public void setLeft(boolean s) {
		left = s;
	}
	public void setRight(boolean s) {
		right = s;
	}
	
	
}
