package xna.android;

import java.util.ArrayList;
import java.util.List;

import android.R.string;
import android.graphics.Bitmap;
import android.graphics.Canvas;

public class Button extends MySprite
{
	public static List<Button> buttonList = new ArrayList<Button>();
	public static int selectedButton = 0; 
	
	public static int getSelectedButton() {
		return selectedButton;
	}

	public static void setSelectedButton(int selectedButton) {
		Button.selectedButton = selectedButton;
	}

	public string getCurrentColor() {
		return currentColor;
	}

	public void setCurrentColor(string currentColor) {
		this.currentColor = currentColor;
	}

	public string currentColor;
	
	public static void createButton(Bitmap texture, int x, int y, SoundManager sm)
	{
		Button button = new Button(texture, x, y, sm);
		buttonList.add(button);
	}
	
	public Button(Bitmap texture, int x, int y, SoundManager sm)
	{
		super(texture, x, y, sm);
	}
	public void Update(long gameTime)
	{
		super.update(320, 480);
	}
	
	public void Draw(Canvas spriteBatch)
	{
		super.draw(spriteBatch);
	}
	
	
	
}
