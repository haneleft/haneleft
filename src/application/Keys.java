package application;

import java.util.Random;

import javafx.geometry.Point2D;
import javafx.scene.Scene;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;

public class Keys extends Shape{
	private int number;
	private int max;
	private int maxShow;
	private Scene scene;
	private double width;
	private double height;
	private Color randomColor;
	private int changeTime;
	private Point2D position;
	
	
	public Keys(double width, double height, int max, int maxShow, Scene scene) {
		super(new Point2D(0.0,0.0), 4,width, height, false);
		this.max = max;
		this.number = 0;
		this.maxShow = maxShow;
		this.scene = scene;
		this.width = width;
		this.height = height;
		this.randomColor = Color.YELLOW;
		this.changeTime = 0;
		this.position = new Point2D(0.0,0.0);
	}
	
	//vykresluje klice ktere hrac sesbiral / ma sesbirat
	public void draw(GraphicsContext gc) {
		gc.save();
		
		String s = String.format("%d/%d", number, maxShow);
		
		gc.setFill(randomColor);
		gc.setFont(new Font("Times New Roman", height-height/12));
		gc.fillText(s, width/2, height);
		this.position = new Point2D(((s.length() * gc.getFont().getSize()) / 2 + width/2), height/5);
		this.updatePosition(position);
		this.drawShape(gc);
		
		
		gc.restore();
	}
	
	//meni barvu klice
	public void simulate(double deltaT) {
		if(changeTime > 15) {
			Random rand = new Random();
			
		    int r = rand.nextInt(255);
		    int g = rand.nextInt(255);
		    int b = rand.nextInt(255);
		    this.randomColor = Color.rgb(b, r, g);
		    
		    changeTime = 0;
		}
		
		changeTime++;
	}
	
	//get,set
	
	public int getMax() {
		return max;
	}
	
	public int getMaxShow() {
		return maxShow;
	}
	
	public void increaseNumber() {
		number++;
		if(max < maxShow) {
			maxShow = max + 1;
		}
	}
	
	public int getNumber() {
		return number;
	}
	
	public void setMaxShow(int maxShow) {
		this.maxShow = maxShow;
		if(this.maxShow > max) {
			this.maxShow = max + 1;
		}
	}
}
