package application;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Scanner;

import javafx.animation.AnimationTimer;
import javafx.event.EventHandler;
import javafx.geometry.Point2D;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.input.KeyEvent;
import javafx.scene.paint.Color;

public class WholeMap {
	private ArrayList<World[]> world =  new ArrayList<>();
	private World firstScreen;
	private Player player;
	private World currentWorld;
	private World startingWorld;
	private int currentY;
	private Canvas canvas;
	private int mapWidth;
	private int startingX;
	private ArrayList<Integer[]> keysArr = new ArrayList<Integer[]>();
	private Keys key;
	private boolean mapCheck;
	private Scene scene;
	private int randomI;
	private int[] stageKeys;
	private int stage;
	private boolean start;
	
	public WholeMap(Canvas canvas, Scene scene, String nameMap) {
		this.canvas = canvas;
		this.mapWidth = 3;
		this.mapCheck = false;
		this.scene = scene;
		this.randomI = 0;
		this.mapWidth = 1;
		this.start = true;
		
		boolean breakUsed = false;
		int i = 0;
		File file;
		
		//nacte se vylikost mapy
		
		String s = String.format("%s/MapWidth.txt",nameMap);
		if(!(file = new File(s)).exists()) {
			System.out.println("Map width not loaded");
			System.exit(1);
		}
		else{
			try {
				BufferedReader buffer = new BufferedReader(new FileReader (s));
				       Scanner in = new Scanner(buffer);
				        
					while (in.hasNextLine()) {
						   Scanner lineIn = new Scanner(in.nextLine());
						   if(lineIn.hasNext()) {
						    	this.mapWidth = lineIn.nextInt();
						   }
					}
						
					if(this.mapWidth % 2 == 0) {
						System.out.println("Width of map must be odd");
						System.out.println("Initilizing preset value: 3");
						this.mapWidth = 3;
					}
			}
			catch (Exception e) {
				System.out.println("Width of map failed to load");
				System.out.println("Initilizing preset value: 3");
				this.mapWidth = 3;
			}
		}
		
		//nacte se uvodni obrazovka
		
		s = String.format("%s/StartingMap.txt",nameMap);
		if(!(file = new File(s)).exists()){
			System.out.println("No starting maps loaded");
			System.exit(1);
		}
		else {
			firstScreen = new World(canvas.getWidth(), canvas.getHeight(), s, new Point2D(2,0));
		}
		
		//nactou se mapy
		
		s = String.format("%s/map%d.txt",nameMap,i);
		if(!(file = new File(s)).exists()){
			System.out.println("No maps loaded");
			System.exit(1);
		}
		else {
			while((file = new File(s)).exists()) {
				World[] worlds = new World[mapWidth];
				Integer[] keysMap = new Integer[mapWidth];
				
				for(int x = 0; x < mapWidth; x++) {
					worlds[x] = new World(canvas.getWidth(), canvas.getHeight(), s, new Point2D(2,0));
					keysMap[x] = (worlds[x].getMapKeys());
					i++;
					s = String.format("%s/map%d.txt",nameMap,i);
					if(!(file = new File(s)).exists()){
						break;
					}
				}
				if(i > mapWidth * mapWidth) {
					System.out.println("Number of maps must be exactly  'MapWidth' * 'MapWidth', remove maps");
					System.exit(1);
				}
				world.add(worlds);
				keysArr.add(keysMap);
			}
		}
		
		
		
		if(world.size() % mapWidth != 0) {
			System.out.println("There is not enought files, dont forget that there must be exactly 'MapWidth' * 'MapWidth' files");
			System.exit(1);
		}
		
		//nahraje se startovni mapa a hrac
		
	    this.currentY = 0;
		
	    int startKeys = 0;
	    int maxKeys = 0;
	    
	    int mid = mapWidth/2;
	    
	    this.stage = mid;
	    this.startingX = mid;
	    this.currentWorld = world.get(mid)[mid];
		this.startingWorld =  world.get(mid)[mid];
		startKeys = currentWorld.getMapKeys();
		if(currentWorld.getPlayer() == null) {
			System.out.println("Player must be set in starting map");
			System.exit(1);
		}
		
		this.player = new Player(currentWorld.getPlayer(), new Point2D(0,2), currentWorld, (canvas.getWidth() /currentWorld.getBricksCountX()) * 2/3 , (canvas.getHeight() / currentWorld.getBricksCountY()) * 2/3, scene);
		
		
		int currentX = 0;
		int currentWidth = mapWidth;
		
		//nahrajou se klice urovni
		
		stageKeys = new int[startingX+1];
		
		for(int x = 0; x < startingX+1; x++) {
			for(int j = currentX; j < currentWidth; j++) {
				stageKeys[x] += keysArr.get(x)[j];
				if((j==currentX || j==currentWidth-1) && (currentX != currentWidth-1 )) {
					for(int k = currentX+1; k < currentWidth-1; k++) {
						stageKeys[x] += keysArr.get(k)[j];
					}
				}
				if(currentX != currentWidth-1){
					stageKeys[x] += keysArr.get(mapWidth-x-1)[j];
				}
				
			}
			currentWidth--;
			currentX++;
			
		}
		
		int check=0;
		for (int l : stageKeys) {
			if(l != 0) {
				check++;
			}
		}
		
		if(check < stageKeys.length) {
			System.out.println("There must be atleast 1 key for every stage. Stage starts with layer[0] and row[0]");
			System.out.println("That means Map - 0, before + 1 + mapWidth, before + 1 + mapWidth, ... , (MapWidth / 2)");
			System.exit(1);
		}
		
		
		
		
		for (Integer[] ka : keysArr) {
			for(int x = 0; x < mapWidth; x++) {
				maxKeys += ka[x];
			}
		}
		
		this.key = new Keys(canvas.getWidth() / currentWorld.getIntMap()[0].length, canvas.getHeight() / currentWorld.getIntMap().length, maxKeys, startKeys, scene);
		
		//ovladani
		
		scene.setOnKeyPressed(new EventHandler<KeyEvent>() {
			
			@Override
			public void handle(KeyEvent event) {
				switch (event.getCode()) {
					case M: mapCheck = true; break;
                	case UP:    player.setUp(true); break;
                	case LEFT:  player.setLeftMove(true); break;
                	case RIGHT: player.setRightMove(true); break;
                	case W:  player.setUp(true); break;
                    case A:  player.setLeftMove(true); break;
                    case D:  player.setRightMove(true); break;
                    case SPACE: start = false;
				}
			}
			
		});
		
		scene.setOnKeyReleased(new EventHandler<KeyEvent>() {
            @Override
            public void handle(KeyEvent event) {
                switch (event.getCode()) {
                	case M: mapCheck = false; break;
                	case UP:    player.setUp(false); break;
                	case LEFT:  player.setLeftMove(false); break;
                	case RIGHT: player.setRightMove(false); break;
                	case W:  player.setUp(false); break;
                	case A:  player.setLeftMove(false); break;
                	case D:  player.setRightMove(false); break;
                }
            }
        });
	}
	
	
	//vykresluje uvodni obrazovku
	public void starting(double deltaT) {
		GraphicsContext gc = canvas.getGraphicsContext2D();
		firstScreen.draw(gc, true);
		gc.restore();
	}
	
	
	//vykresluje svet na kterem je prave hrac a prepina svety podle pohybu hrace, take vykresluje mapu
	public void initiate(double deltaT) {
		GraphicsContext gc = canvas.getGraphicsContext2D();
		if(mapCheck == false) {
			boolean breakUsed = false;
			if(player.getResetPosition()) {
				res: for (World[] worlds : world) {
					for(int i = 0; i < worlds.length; i++) {
						if(worlds[i] == player.getResetWorld()) {
							currentWorld = worlds[i];
							player.setWorld(currentWorld);
							player.resetPos();
							break res;
						}
					}
				}
			}
			else if(key.getNumber() >= stageKeys[startingX]){
				if(player.getSide() == 1) {
					side1: for (World[] worlds : world) {
						for(int i = stage; i < worlds.length - stage; i++) {
							if(worlds[i] == currentWorld) {
								if(i==stage) {
									currentWorld = worlds[worlds.length-1-stage];
								}
								else {
									currentWorld = worlds[i-1];
								}
								player.setWorld(currentWorld);
								break side1;
							}
						}
					}
				}
				else if (player.getSide() == 3) {
					side3: for (World[] worlds : world) {
						for(int i = stage; i < worlds.length - stage; i++) {
							if(worlds[i] == currentWorld) {
								if(i== worlds.length - stage-1) {
									currentWorld = worlds[stage];
								}
								else {
									currentWorld = worlds[i+1];
								}
								player.setWorld(currentWorld);
								break side3;
							}
						}
					}
				}
				else if (player.getSide() == 4) {
					side4: for (int k = stage ; k < (world.size() - stage);  k++) {
						for(int i = stage; i < world.get(k).length; i++) {
							if(world.get(k)[i] == currentWorld) {
								if((k+1) >= world.size()-stage) {
									currentWorld = world.get(stage)[i];
								}
								else {
									currentWorld = world.get(k+1)[i];
								}
							player.setWorld(currentWorld);
							break side4;
							}
						}
					}
				}
				else if (player.getSide() == 2) {
					side4: for (int k = stage ; k < (world.size() - stage);  k++) {
						for(int i = stage; i < world.get(k).length; i++) {
							if(world.get(k)[i] == currentWorld) {
								if((k-1) <= (0 + stage)) {
									currentWorld = world.get(world.size() - stage - 1)[i];
								}
								else {
									currentWorld = world.get(k-1)[i];
								}
							player.setWorld(currentWorld);
							break side4;
							}
						}
					}
				}
			}
			
			breakUsed = false;
			if(key.getNumber() == key.getMax() && currentWorld == startingWorld) {
				startingWorld.drawMad(gc);
			}
			else {
				currentWorld.draw(gc);
			}
			
			currentWorld.simulate(deltaT);
			
			player.draw(gc);
			player.simulate(deltaT);
			
			key.draw(gc);
			key.simulate(deltaT);
			
			if(player.getKeyBrick() != null) {
				int p = 0;
				for (World[] worlds : world) {
					if(breakUsed == true) {
						break;
					}
					for(int i = 0; i < worlds.length; i++) {
						if(worlds[i] == currentWorld) {
							currentWorld.removeBrick(player.getKeyBrick());
							key.increaseNumber();
							breakUsed = true;
							break;
						}
					}
					p++;
				}
			}
			
			if(key.getNumber() >= key.getMaxShow()) {
				if(key.getMaxShow() == key.getMax()) {
					key.setMaxShow(key.getMax() + 1);
				}
				else {
					key.setMaxShow(stageKeys[stage-1] + key.getNumber());
					stage--;
				}
				
			}
		}
		else {
			int o = 0;
			for (World[] worlds : world) {
				if(o >= stage && ((world.size() - stage-1) >= o)) {
					for (int x = 0; x < worlds.length; x++) {
						if(key.getNumber() < startingWorld.getMapKeys()) {
							if(x >= stage && ((world.size() - stage-1) >= x)) {
									worlds[x].draw(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
									worlds[x].drawBlink(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
							}
							else {
								worlds[x].drawVoid(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
							}
						}
						else if((x >= stage && ((world.size() - stage-1) >= x)) && (key.getNumber() != key.getMax()) ) {
							worlds[x].draw(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
							if(worlds[x] == currentWorld) {
								worlds[x].drawBlink(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
							}
						}
						else if(key.getNumber() == key.getMax() && randomI > 10){
							if(worlds[x] == startingWorld) {
								worlds[x].drawMad(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
								if(worlds[x] == currentWorld) {
									worlds[x].drawBlink(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
								}
							}
							else{
								worlds[x].draw(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
								if(worlds[x] == currentWorld) {
									worlds[x].drawBlink(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
								}
							}
						}
						else {
							worlds[x].drawVoid(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
						}
					}
					
				}
				else {
					for (int x = 0; x < worlds.length; x++) {
						worlds[x].drawVoid(gc, canvas.getWidth() / worlds.length, canvas.getHeight()/ world.size(), x * (canvas.getWidth() / worlds.length), o * (canvas.getHeight()/ world.size()));
					}
					
				}
				
				o++;
			}
		}
		
		if(randomI > 15) {
			randomI= 0;
		}
		
		randomI++;
		
		gc.restore();
	}
	
	//get start pro rozeznani vykreslovani startovniho sveta
	
	public boolean getStart() {
		return start;
	}
	
	
	
}
