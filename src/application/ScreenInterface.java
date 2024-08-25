package application;

import javafx.geometry.Point2D;
import javafx.scene.canvas.GraphicsContext;
//interface pro kostky

public interface ScreenInterface {
	public void draw(GraphicsContext gc);
	public void draw(GraphicsContext gc,Point2D pos, double mapWidth, double mapHeight);
	public void simulate(double deltaT, Point2D speed);
	public void drawMad(GraphicsContext gc, double w, double y);
	public void drawMad(GraphicsContext gc, boolean small);
	public int getType();
}
