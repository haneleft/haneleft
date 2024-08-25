package application;
	
import java.io.File;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.Iterator;

import javafx.animation.AnimationTimer;
import javafx.application.Application;
import javafx.event.EventHandler;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.BorderPane;

	


public class Main extends Application {
	public static void main(String[] args) {
		launch(args);
	}
	
	private Canvas canvas;
	private AnimationTimer animationTimer;
	private WholeMap wmap;
	
	//inicializovani hry
	@Override
	public void start(Stage primaryStage) {
		try {
			Group root = new Group();
			canvas = new Canvas(760,600);
			root.getChildren().add(canvas);
			Scene scene = new Scene(root,canvas.getWidth(),canvas.getHeight());
			scene.getStylesheets().add(getClass().getResource("application.css").toExternalForm());
			primaryStage.setScene(scene);
			primaryStage.resizableProperty().set(false);
			primaryStage.setTitle("The Ruins of Machi Itcza");
			primaryStage.show();
			
			wmap = new WholeMap(canvas, scene, "Maps");
			
			animationTimer = new AnimationTimer() {
				private Long previous;
				
				@Override
				public void handle(long now) {
					if (previous == null) {
						previous = now;
					} else {
						drawScene((now - previous)/1e9);
						previous = now;
					}
				}
				
				
			};
			
			
			animationTimer.start();
			primaryStage.setOnCloseRequest(this::exitProgram);
			
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	//vykresluje hru (uvodni obrazovku a mapy)
	private void drawScene(double deltaT) {
		if(wmap.getStart() != true) {
			wmap.initiate(deltaT);
		}
		else {
			wmap.starting(deltaT);
		}
		
		
	}
	
	private void exitProgram(WindowEvent evt) {
		animationTimer.stop();
		System.exit(0);
	}
	
	public AnimationTimer getAnimationTimer() {
		return animationTimer;
	}
	
	public WholeMap getWholeMap() {
		return wmap;
	}
	
	
	
}
