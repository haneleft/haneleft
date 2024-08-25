package application;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import javafx.event.EventHandler;
import javafx.geometry.Point2D;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.input.KeyEvent;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;

public class World{
	private double width;
	private double height;
	private int[][] map;
	private ArrayList<Brick> bricks = new ArrayList<>();
	private int bricksCountY;
	private int bricksCountX;
	private String filename;
	private Point2D player;
	private Point2D speed;
	private int maxKey;
	private boolean brick10Check;
	private boolean last;
	
	//svet bez klicu
	public World(double width, double height, ArrayList<Brick> bricks, String filename, Point2D speed) {
		this.width = width;
		this.height = height;
		this.filename = filename;
		
        this.map = new MapImporter(filename).getMapWithoutKey();
        this.bricksCountY = map.length;
		this.bricksCountX = 0;
		this.bricks = bricks;
		this.speed = speed;
		this.brick10Check = false;
		this.last = false;
		
	}
	
	//svet
	public World(double width, double height, String filename, Point2D speed) {
		this.width = width;
		this.height = height;
		this.filename = filename;
		this.speed = speed;
		
        this.map = new MapImporter(filename).getMap();
        this.bricksCountY = map.length;
		this.bricksCountX = 0;
		this.brick10Check = false;
		this.last = false;
		
		this.maxKey = 0;
		
		for (int i = 0; i < map.length; i++) {
			for (int j = 0; j < map[i].length; j++) {
				if(map[i].length > bricksCountX) {
					bricksCountX = map[i].length;
				}
				if(map[i][j] == 8) {
					player = new Point2D(j * (width /map[i].length), i * (height / map.length));
				}
				else if(map[i][j] != 0) {
					bricks.add(new Brick(new Point2D(j * (width /map[i].length), i * (height / map.length)), map[i][j], width /map[i].length, height / map.length,true));
				}
				if(map[i][j] == 4) {
					maxKey++;
				}
			}
		}
		
	}
	
	//vykresluje svet
	public void draw(GraphicsContext gc) {
		gc.clearRect(0, 0,width, height);
		gc.setFill(Color.BLACK);
		gc.fillRect(0, 0, width, height);
		gc.restore();
		for (Brick brick : bricks) {
			brick.draw(gc);
		}
	}
	
	//vykresluje uvodni svet
	public void draw(GraphicsContext gc, boolean start) {
		gc.clearRect(0, 0,width, height);
		gc.setFill(Color.BLACK);
		gc.fillRect(0, 0, width, height);
		for (Brick brick : bricks) {
			brick.draw(gc);
		}
		
		String s = "PRESS SPACE TO START";
		gc.setFill(Color.WHITE);
		gc.setFont(new Font("Times New Roman", (height/bricksCountX)*2));
		gc.fillText(s, width/bricksCountX , height/2);
		s = "THE RUINS OF";
		gc.fillText(s, width/(bricksCountX)*4 , (height/bricksCountX)*4);
		s = "MACHI ITCZA";
		gc.fillText(s, width/(bricksCountX)*4 , (height/bricksCountX)*4 + gc.getFont().getSize());
		
		gc.restore();
	}
	
	//vykresluje svet pro mapu
	public void draw(GraphicsContext gc, double width, double height, double x, double y) {
		gc.clearRect(x, y,width, height);
		gc.setFill(Color.BLACK);
		gc.fillRect(x, y, width, height);
		
		ArrayList<ScreenInterface> mapBricks = new ArrayList<>();
		
		for (int i = 0; i < map.length; i++) {
			for (int j = 0; j < map[i].length; j++) {
				if(map[i][j] != 0) {
					mapBricks.add(new Brick(new Point2D(j * (width /map[i].length) + x, i * (height / map.length) + y),  map[i][j], width /map[i].length, height / map.length, false));
				}
			}
		}
		
		for (ScreenInterface brick : mapBricks) {
			if(brick.getType() != 4 && brick.getType() != 9 && brick.getType() != 10) {
				brick.draw(gc);
			}
		}
		
		mapBricks.clear();
		
		gc.restore();
	}
	
	//vykresluje ukazatel pro svet na kterem je zrovna hrac na mape
	public void drawBlink(GraphicsContext gc, double width, double height, double x, double y) {
		gc.setFill(Color.WHITE);
		gc.fillRect(x, y, width, height/20);
		gc.fillRect(x, y, width/20, height);
		gc.fillRect(x, y+(height/20)*19, width, height/20);
		gc.fillRect(x+(width/20)*19, y, width/20, height);
		
		gc.restore();
	}
	
	//vykresluje prazdny svet pro mapu
	public void drawVoid(GraphicsContext gc, double width, double height, double x, double y) {
		gc.clearRect(x, y,width, height);
		gc.setFill(Color.BLACK);
		gc.fillRect(x, y, width, height);
		gc.restore();
	}
	
	//vykresluje sileny svet
	public void drawMad(GraphicsContext gc) {
		gc.clearRect(0, 0,width, height);
		gc.setFill(Color.BLACK);
		gc.fillRect(0, 0, width, height);
		gc.restore();
		if(last == false) {
			last = true;
			bricks.add(new Brick(new Point2D(0.0, 0.0), 4, width /map[0].length, height / map.length, false));
		}
		for (Brick brick : bricks) {
			brick.drawMad(gc, width, height);
		}
	}
	
	//vykresluje sileny svet pro mapu
	public void drawMad(GraphicsContext gc, double width, double height, double x, double y) {
		gc.clearRect(x, y,width, height);
		gc.setFill(Color.BLACK);
		gc.fillRect(x, y, width, height);
		
		ArrayList<ScreenInterface> mapBricks = new ArrayList<>();
		
		for (int i = 0; i < map.length; i++) {
			for (int j = 0; j < map[i].length; j++) {
				if(map[i][j] != 0) {
					mapBricks.add(new Brick(new Point2D(j * (width /map[i].length) + x, i * (height / map.length) + y),  map[i][j], width /map[i].length, height / map.length, false));
				}
			}
		}
		
		for (ScreenInterface brick : mapBricks) {
			if(brick.getType() != 4 && brick.getType() != 9 && brick.getType() != 10) {
				brick.drawMad(gc, true);
			}
		}
		
		mapBricks.clear();
		
		gc.restore();
	}
	
	//simuluje nepratele
	public void simulate(double deltaT) {
		for (Brick brick : bricks) {
			if(brick.getType() == 9) {
				for (Brick brick2 : bricks) {
					if(brick.getHitBox().getBoundsInParent().intersects(brick2.getHitBox().getBoundsInParent()) && brick2.getType() != 9) {
						brick.resetPos();
					}
				}
				brick.simulate(deltaT, speed);
			}
			if(brick.getType() == 10) {
				if((brick.getHitBox().getBoundsInParent().getMinY() + brick.getHitBox().getBoundsInParent().getHeight())< (brick.getResetPos().getY() - 2*(height / map.length))&& brick10Check == false) {
					brick.simulate(deltaT, new Point2D(speed.getY(), speed.getX()));
					brick10Check = true;
				}
				else if(brick10Check == true) {
					brick.simulate(deltaT, new Point2D(speed.getY(), (speed.getX()*2)/3));
				}
				else if(brick10Check == false) {
					brick.simulate(deltaT, new Point2D(speed.getY(), -(speed.getX()*2)/3));
				}
					
				if((brick.getResetPos().getY() < (brick.getHitBox().getBoundsInParent().getMinY()) + speed.getY()) ) {
					brick10Check = false;
				}
			}
			
		}
	}
	
	//get,set

	public double getHeight() {
		return height;
	}
	
	public double getWidth() {
		return width;
	}
	
	public int getBricksCountX(){
		return bricksCountX;
	}
	
	public int getBricksCountY(){
		return bricksCountY;
	}
	
	public ArrayList<Brick> getBricks(){
		return bricks;
	}
	
	public void setBricks(ArrayList<Brick> bricks) {
		this.bricks = bricks;
	}
	
	//vyvojarske metody pro zjisteni nacitani map
	/*public void currentBricks() {
		int i = 0;
		for (Brick brick : bricks) {
			i++;
		}
		System.out.println(i);
	}
	
	public void drawMap() {
		for (int i = 0; i < map.length; i++) {
			for (int j = 0; j < map[i].length; j++) {
				System.out.print(map[i][j] + " ");
				
			}
			System.out.println();
		}
	}*/
	
	public void removeBrick(Brick brick) {
		bricks.remove(brick);
		
	}
	
	//nova mapa bez klicu
	public World getMap() {
		return new World(width, height, bricks, filename, speed);
	}
	
	public int[][] getIntMap(){
		return map;
	}
	
	public Point2D getPlayer() {
		return player;
	}
	
	public int getMapKeys() {
		return maxKey;
	}
	
}
