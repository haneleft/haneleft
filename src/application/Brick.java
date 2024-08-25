package application;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.Random;

import javafx.geometry.Bounds;
import javafx.geometry.Point2D;
import javafx.geometry.Rectangle2D;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;

public class Brick extends Shape implements ScreenInterface {

	private Point2D position;
	private int type;
	private double width;
	private double height;
	private boolean key;
	private Rectangle hitBox;
	private Color color;
	private Image img;
	private Point2D original;
	private boolean spawn;
	private int changeTime;
	private double xp;
	private double yp;
	
	public Brick(Point2D position, int type, double width, double height, boolean rand) {
		super(position,type,width,height, rand);
		this.position = position;
		this.type = type;
		this.width = width;
		this.height = height;
		this.key = false;
		this.spawn = false;
		this.changeTime = 0;
		
		
		hitBox = new Rectangle(0,0,0,0);
		
		if(type != 5) hitBox = new Rectangle(position.getX(), position.getY(), width, height);
		
		
		if(type == 1) {
			this.color = Color.rgb(129,70,29);
		}
		else if(type == 2){
			this.color = Color.rgb(42,158,29);
		}
		else if(type == 3){
			this.color = Color.GREY;
		}
		else if(type == 4) {
			this.color = Color.YELLOW;
			key = true;
		}
		else if(type == 5) {
			this.color = Color.LIGHTBLUE;
		}
		else if(type == 6) {
			this.color = Color.rgb(98,71,25);
			this.height = height / 2;
			hitBox = new Rectangle(position.getX(), position.getY(), width, 0);
		}
		else if(type == 7){
			this.color = Color.BLUE;
			try {
				this.img = new Image(new FileInputStream("Graphics/night.png"));
			} catch (Exception e) {
				System.out.println("Image not loaded");
			}
		}
		else if(type == 9) {
			this.color = Color.WHITE;
			this.height = height / 3;
			this.position = new Point2D(position.getX(), position.getY() + this.height);
			original = this.position;
			hitBox = new Rectangle(position.getX(), position.getY() + this.height, width, this.height);
		}
		else if(type == 10) {
			this.color = Color.WHITE;
			this.height = height /3;
			this.width = width / 3;
			this.position = new Point2D(position.getX() + this.width, position.getY() + this.height*2);
			original = this.position;
			hitBox = new Rectangle(position.getX() + this.width, position.getY() + this.height*2, this.width, this.height);
		}
		else {
			this.color = Color.RED;
		}
		
		this.xp = position.getX();
		this.yp = position.getY();
		
	}
	
	//nakresli danou kostku
	@Override
	public void draw(GraphicsContext gc) {
		gc.save();
		
		gc.setFill(this.color);
		if(type!=5) {
			drawShape(gc);
		}
		else if(spawn == false){
			gc.fillRect(position.getX() + width/3, position.getY(), width/3, height - (height/12));
			gc.fillRect(position.getX(), position.getY() + height/3, width, height/3);
		}
		else {
			gc.fillRect(position.getX() + width/3, position.getY() + height/3 - 2, width/3, height/3 - (height/12));
			gc.fillRect(position.getX(), position.getY() + (height/3)*2, width, height/3);
		}
		
		if(img != null) {
			gc.drawImage(img,position.getX(), position.getY(), width, height);
		}
		
		gc.restore();
	}
	
	//meni barvy pri vykreslovani
	@Override
	public void drawMad(GraphicsContext gc, double w, double y) {
		gc.save();
		
		if(changeTime > 30) {
			 Random rand = new Random();
				
				int r = rand.nextInt(255);
				int g = rand.nextInt(255);
				int b = rand.nextInt(255);
				
				this.color = Color.rgb(b, r, g);
				
				xp = rand.nextFloat();
				yp = rand.nextFloat();
		    changeTime = 0;
		}
		
		changeTime++;
 
		
		if(type == 4) {
			color = Color.YELLOW;
		}
		
		
		
		gc.setFill(this.color);
		if(type == 4) {
			this.drawMadShape(gc, w*xp, y*yp);
		}
		else if(type!=5) {
			this.drawShape(gc);
		}
		else if(spawn == false){
			gc.fillRect(position.getX() + width/3, position.getY(), width/3, height - (height/12));
			gc.fillRect(position.getX(), position.getY() + height/3, width, height/3);
		}
		else {
			gc.fillRect(position.getX() + width/3, position.getY() + height/3 - 2, width/3, height/3 - (height/12));
			gc.fillRect(position.getX(), position.getY() + (height/3)*2, width, height/3);
		}
		
		if(img != null) {
			gc.drawImage(img,position.getX(), position.getY(), width, height);
		}
		
		gc.restore();
	}
	
	//meni barvy pri vykreslovani u mapy
	@Override
	public void drawMad(GraphicsContext gc, boolean small) {
		gc.save();
		
		 Random rand = new Random();
				
		int r = rand.nextInt(255);
		int g = rand.nextInt(255);
		int b = rand.nextInt(255);
				
	    this.color = Color.rgb(b, r, g);
				
		xp = rand.nextFloat();
		yp = rand.nextFloat();
		
		
		gc.setFill(this.color);
		if(type!=5) {
			this.drawShape(gc);
		}
		else if(spawn == false){
			gc.fillRect(position.getX() + width/3, position.getY(), width/3, height - 2);
			gc.fillRect(position.getX(), position.getY() + height/3, width, height/3);
		}
		else {
			gc.fillRect(position.getX() + width/3, position.getY() + height/3 - 2, width/3, height - 2);
			gc.fillRect(position.getX(), position.getY() + (height/3)*2, width, height/3);
		}
		
		if(img != null) {
			gc.drawImage(img,position.getX(), position.getY(), width, height);
		}
		
		gc.restore();
	}
	
	//vykresluje mensi kostky pro mapu
	@Override
	public void draw(GraphicsContext gc,Point2D pos, double mapWidth, double mapHeight) {
		gc.save();
		
		gc.setFill(this.color);
		gc.fillRect(pos.getX(), pos.getY(),mapWidth, mapHeight);
		
		if(img != null) {
			gc.drawImage( img, pos.getX(), pos.getY(),mapWidth, mapHeight);
		}
		
		gc.restore();
	}
	
	//simuluje nepratelske jednotky (strely)
	@Override
	public void simulate(double deltaT, Point2D speed) {
		position = position.add(speed);
		hitBox = new Rectangle(position.getX(), position.getY(), width, height);
		this.updatePosition(position);
	}
	
	//get,sety
	
	@Override
	public int getType() {
		return type;
	}
	
	public Rectangle getHitBox() {
		return hitBox;
	}
	
	public double getWidth() {
		return width;
	}
	
	
	
	public void resetPos() {
		position = original;
	}
	
	public Point2D getResetPos() {
		return original;
	}
	
	public void setResetPos(Point2D pos) {
		original = pos;
	}
	
	public Point2D getPosition() {
		return position;
	}
	
	public void setSpawn(Boolean spawn) {
		this.spawn = spawn;
	}
}
