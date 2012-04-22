package xna.android;

import java.util.HashMap;
import java.util.Map;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.util.Log;

public class SpriteFont
{
	//para debug
	private static final String TAG = SpriteFont.class.getSimpleName();
	
	//para desenhar
	private Bitmap bitmap;	

	//um hashmap para mapear o caracter com a letra
	private Map<Character, Bitmap> glypho = new HashMap<Character, Bitmap>(62);

	//largura e altura
	private int width;
	private int height;
	public int x;
	public int y;
	
	//conjunto de letras minusculas
	private char[] charactersL = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g',
			'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
			'u', 'v', 'w', 'x', 'y', 'z' };
	
	//conjunto de letras maiusculas
	private char[] charactersU = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G',
			'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z' };
	
	//conjunto de numeros
	private char[] numbers = new char[] { '1', '2', '3', '4', '5', '6', '7',
			'8', '9', '0' };
	
	//conjunto de caracteres especiais
	private char[] specialChar = new char[] { '!', '@', '£', '$', '%', '^', '&',
			'(', ')', '-', '-', '+', '=', '"', '.', ',' , ':', ';'};
	
	//construtor da classe
	public SpriteFont(Bitmap bitmap) {
		super();
		this.bitmap = bitmap;
		this.width = 8;
		this.height = 12;
		this.x = 0;
		this.y = 0;
	
		for (int i = 0; i < 26; i++) {
			glypho.put(charactersL[i], Bitmap.createBitmap(bitmap,
					0 + (i * width), 0, width, height));
		}
		Log.d(TAG, "lowercase");
		
		for (int i = 0; i < 26; i++) {
			glypho.put(charactersU[i], Bitmap.createBitmap(bitmap,
					0 + (i * width), 15, width, height));
		}
		Log.d(TAG, "uppercase");
		
		for (int i = 0; i < 10; i++) {
			glypho.put(numbers[i], Bitmap.createBitmap(bitmap,
					0 + (i * width), 30, width, height));
		}
		Log.d(TAG, "numbers");
		
		for (int i = 0; i < 18; i++) {
			glypho.put(specialChar[i], Bitmap.createBitmap(bitmap,
					0 + (i * width), 45, width, height));
		}
		Log.d(TAG, "specialChars");
		
		// TODO - pontuação
	}
	
	public Bitmap getBitmap() {
		return bitmap;
	}
	
	
	public void drawString(Canvas canvas, String text, int x, int y) {
		if (canvas == null) {
			Log.d(TAG, "canvas null");
		}
		for (int i = 0; i < text.length(); i++) {
			Character ch = text.charAt(i);
			if (glypho.get(ch) != null) {
				canvas.drawBitmap(glypho.get(ch), x + (i * width), y, null);
			}
		}
	}
}
