package application;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Scanner;

//importuje kostky ze souboru do hry
public class MapImporter {
	
	private File myFile;
	private ArrayList<int[]> maps;

	public MapImporter(String filename) {
		try {
			maps = new ArrayList<>();
			
			BufferedReader buffer = new BufferedReader(new FileReader (filename));
	        Scanner in = new Scanner(buffer);
	        
			while (in.hasNextLine()) {
			    Scanner lineIn = new Scanner(in.nextLine());
			    if(lineIn.hasNext()) {
			    	String[] s = lineIn.nextLine().split(" ");
			        int[] row = new int[s.length];
			        for (int i = 0; i < s.length; i++) {
			            row[i] = Integer.parseInt(s[i]);
			            
			        }
			        maps.add(row);
			    }
			}
			
			
		} catch (Exception e) {
			System.out.println("Couldnt load file");
		}
		
	}
	
	//get,set
	
	public int[][] getMap(){
		int[][] map = new int[maps.size()][];
		int x=0;
		for (int[] m : maps) {
			map[x] = new int[m.length];
			for(int i = 0; i < m.length; i++) {
				map[x][i] = m[i];
			}
			x++;
		}
		
		return map;
	}
	
	
	public int[][] getMapWithoutKey(){
		int[][] map = new int[maps.size()][];
		int x=0;
		for (int[] m : maps) {
			map[x] = new int[m.length];
			for(int i = 0; i < m.length; i++) {
				if(m[i] == 4) {
					map[x][i] = 0;
				}
				else {
					map[x][i] = m[i];
				}
				
			}
			x++;
		}
		
		return map;
	}
	
}



