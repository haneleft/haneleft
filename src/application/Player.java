package application;

import javafx.geometry.Point2D;
import javafx.geometry.Rectangle2D;
import javafx.scene.Scene;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.input.KeyEvent;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;

import java.util.ArrayList;
import java.util.List;

import javafx.event.EventHandler;

//hrac
public class Player extends Shape{

	private World world;
	private Point2D position;
	private Point2D speed;
	private Point2D leftSpeed;
	private Point2D rightSpeed;
	private Point2D upSpeed;
	private Rectangle hitBox;
	private double width;
	private double height;
	private boolean left, right, up;
	private double jump;
	private ArrayList<Brick> bricks = new ArrayList<>();
	private int side;
	private Brick keyBrick;
	private boolean ghost;
	private Point2D positionStart;
	private boolean positionReset;
	private World resetWorld;
	
	public Player(Point2D position, Point2D speed, World world, double width, double height, Scene scene) {
		super(position, 8,width,height, false);
		this.position = position;
		this.positionStart = position;
		this.speed = speed;
		this.world = world;
		this.resetWorld = world;
		this.width = width;
		this.height = height;
		this.jump = 0;
		this.side = 0;
		this.ghost = false;
		
		this.bricks = world.getBricks();
		
		
		
	}
	
	//vykresluje postavu, a prepina vykreslovani podle pohybu postavy
	public void draw(GraphicsContext gc) {
		gc.save();
		Color col = Color.BLUE;
		gc.setFill(col);
		if(speed.getX() < 0) {
			this.setLeft(true);
			this.setRight(false);
			this.drawPerson(gc,col);
		}
		else if(speed.getX() > 0) {
			this.setLeft(false);
			this.setRight(true);
			this.drawPerson(gc,col);
		}
		else {
			this.setLeft(false);
			this.setRight(false);
			this.drawPerson(gc,col);
		}
		
		gc.restore();
	}
	
	//simuluje collisiony s kostkami a pohyb
	public void simulate(double deltaT) {
		this.updatePosition(position);
		this.positionReset = false;
		keyBrick = null;
		side = 0;
		this.ghost = false;
		
		if(left && right || !left && !right) {
			setSpeed(speed.getX()*0.8, speed.getY());
		}
		else if(left && !right) {
			setSpeed(speed.getX()-1, speed.getY());
			
		}
		else if(!left && right) {
			setSpeed(speed.getX()+1, speed.getY());
		}
		
		if(speed.getX() > 0 && speed.getX() < 0.75) {
			setSpeed(0, speed.getY());
		}
		if(speed.getX() < 0 && speed.getX() > -0.75) {
			setSpeed(0, speed.getY());
		}
		
		if(speed.getX() > 2) {
			setSpeed(2, speed.getY());
		}
		if(speed.getX() < -2) {
			setSpeed(-2, speed.getY());
		}
		
		
		if(up) {
			if((jump > -60) && (jump < -2)) {
				setSpeed(speed.getX(), -2);
				jump += speed.getY();
			}
			else {
			jump = 0;
				for (Brick brick : bricks) {
					if(((position.getX() + width + speed.getX()) > brick.getHitBox().getX() &&  (position.getX() + speed.getX())  < (brick.getHitBox().getX() + brick.getWidth())) && 
							((position.getY() + height + speed.getY()) > brick.getHitBox().getY() &&  (position.getY() + height + speed.getY()) < (brick.getHitBox().getY() + speed.getY())) 
							&& brick.getType() == 6) {
						setSpeed(speed.getX(), -4);
						jump = speed.getY();
						
					}
					else if(this.getHitBoxP1(0.0,speed.getY()).getBoundsInParent().intersects(brick.getHitBox().getBoundsInParent()) == true && (brick.getType() != 4 && brick.getType() != 9 && brick.getType() != 10))  {
						setSpeed(speed.getX(), -4);
						jump = speed.getY();
					}
					
					
				}
			}
		}
		else {
			jump = 0;
		}
		
		
		double promX = position.getX();
		double promY = position.getY();
		
		for (Brick brick : bricks) {
			if(speed.getY() >= 2) {
				if(((position.getX() + width + speed.getX()) > brick.getHitBox().getX() &&  (position.getX() + speed.getX())  < (brick.getHitBox().getX() + brick.getWidth())) && 
					((position.getY() + height + speed.getY()) > brick.getHitBox().getY() &&  (position.getY() + height + speed.getY()) < (brick.getHitBox().getY() + speed.getY())) 
					&& brick.getType() == 6)
				{
					while(!brick.getHitBox().getBoundsInParent().intersects(this.getHitBox().getBoundsInParent()) && ((position.getY() - promY) > 0 && (position.getY() - promY) < 0.5)) {
						promY += Math.signum(speed.getY());
					}
					setSpeed(speed.getX(), 0);
					position = new Point2D(position.getX(), promY);
				}
				else if(((position.getX() + width + speed.getX()) > brick.getHitBox().getX() &&  (position.getX() + speed.getX()) < (brick.getHitBox().getX() + brick.getWidth())) && 
					((position.getY() + height + speed.getY()) > brick.getHitBox().getY()) &&  ((position.getY() + speed.getY()) < (brick.getHitBox().getY() + speed.getY())) 
						&& brick.getType() == 6)
				{
					ghost = true;
				}
			}
			else{
				if(((position.getX() + width + speed.getX()) > brick.getHitBox().getX() &&   (position.getX() + speed.getX())  < (brick.getHitBox().getX() + brick.getWidth())) && 
					((position.getY() + height + speed.getY()) > brick.getHitBox().getY()) &&  ((position.getY() + speed.getY()) < (brick.getHitBox().getY() - speed.getY())) 
						&& brick.getType() == 6)
				{
					ghost = true;
				}
				
			}
		}
		
		if(ghost == false) {
			for (Brick brick : bricks) {
				if(this.getHitBoxP1(speed.getX(),0.0).getBoundsInParent().intersects(brick.getHitBox().getBoundsInParent()) && brick.getType() == 4){
					keyBrick = brick;
				}
				else if(this.getHitBoxP1(speed.getX(), speed.getY()).getBoundsInParent().intersects(brick.getHitBox().getBoundsInParent()) && (brick.getType() == 9 || brick.getType() == 10)) {
					world.getBricks().get(world.getBricks().indexOf(brick)).resetPos();
					positionReset = true;
				}
				if(((this.position.getX() + width) > brick.getPosition().getX()) && ((this.position.getX()) < (brick.getPosition().getX()+ brick.getWidth())) &&  brick.getType() == 5) {
					for (Brick brick2 : resetWorld.getBricks()) {
						if(brick2.getPosition() == positionStart) {
							brick2.setSpawn(false);
						}
					}
					
					positionStart = brick.getPosition();
					resetWorld = world;
					brick.setSpawn(true);
				}
				
				if(this.getHitBoxP1(speed.getX(),0.0).getBoundsInParent().intersects(brick.getHitBox().getBoundsInParent()) == true)  {
					while(!brick.getHitBox().getBoundsInParent().intersects(this.getHitBox().getBoundsInParent()) && ((position.getX() - promX) > 0 && (position.getX() - promX) < 0.5)) {
						promX += Math.signum(speed.getX());
						System.out.println(position.getX() - promX + " x");
					}
					setSpeed(0, speed.getY());
					position = new Point2D(promX, position.getY());
				}
			}
			for (Brick brick : bricks) {
				if(this.getHitBoxP1(0.0,speed.getY()).getBoundsInParent().intersects(brick.getHitBox().getBoundsInParent()) && brick.getType() == 4){
					keyBrick = brick;
				}
				else if(this.getHitBoxP1(speed.getX(), speed.getY()).getBoundsInParent().intersects(brick.getHitBox().getBoundsInParent()) && (brick.getType() == 9 || brick.getType() == 10)) {
					world.getBricks().get(world.getBricks().indexOf(brick)).resetPos();
					this.resetPos();
				}
				if(this.getHitBoxP1(0.0,speed.getY()).getBoundsInParent().intersects(brick.getHitBox().getBoundsInParent()) == true)  {
					
					while(!brick.getHitBox().getBoundsInParent().intersects(this.getHitBox().getBoundsInParent()) && ((position.getY() - promY) > 0 && (position.getY() - promY) < 0.5)) {
						promY += Math.signum(speed.getY());
					}
					setSpeed(speed.getX(), 0);
					position = new Point2D(position.getX(), promY);
				}
			}
		}
		
		position = position.add(speed);
		
		if(position.getY() + height/2 > world.getHeight()) {
			position = new Point2D(position.getX(), 0);
			side = 4;
		}
		else if((position.getY() + height) < 0 ) {
			position = new Point2D(position.getX(), world.getHeight() - height);
			jump = -4;
			side = 2;
		}
		else if(position.getX() > world.getWidth()) {
			position = new Point2D(-width/2 , position.getY());
			side = 3;
		}
		else if(position.getX() + width < 0) {
			position = new Point2D(world.getWidth() - width/2, position.getY());
			side = 1;
		}
		
		if(jump == 0) {
			setSpeed(speed.getX(), 2);
		}
		
	}
	
	//get,set 
	
	public void setSpeed(double x, double y) {
		speed = new Point2D(x,y);
	}
	
	public Rectangle getHitBox() {
		return new Rectangle(position.getX(), position.getY(), width, height);
	}
	
	public Rectangle getHitBoxP1(Double x, Double y) {
		return new Rectangle(position.getX()+x, position.getY() +y, width, height);
	}
	
	public Rectangle getHitSideY(Double x, Double y, double w, double h) {
		return new Rectangle(position.getX()+ x + w, position.getY() + y + h, width, 0);
	}
	
	public void setWorld(World world) {
		this.world = world;
		this.bricks = world.getBricks();
	}
	
	public int getSide() {
		return side;
	}
	
	public Brick getKeyBrick() {
		return keyBrick;
	}
	
	public boolean getResetPosition() {
		return positionReset;
	}
	
	public void setRespawn(Point2D position) {
		positionStart = position;
	}
	
	public void resetPos() {
		position = positionStart;
	}
	
	public World getResetWorld() {
		return resetWorld;
	}
	
	public void setResetWorld(World world) {
		resetWorld = world;
	}
	
	public void setUp(boolean up) {
		this.up = up;
	}
	
	public void setLeftMove(boolean left) {
		this.left = left;
	}
	
	public void setRightMove(boolean right) {
		this.right = right;
	}
	
	
	
}
