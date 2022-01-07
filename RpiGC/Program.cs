using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.ConfigFlags;
using static Raylib_cs.TextureFilter;
using static Raylib_cs.MouseButton;

namespace RpiGC
{
    class ButtonMenu{
        private Texture2D texture2d;
        public int btnState = 0;
        public float frameHeight;
        public float height;
        public float width;

        public int screenWidth, screenHeight;

        public bool btnAction = false;         // Button action should be activated

        public Rectangle sourceRec;
        public Rectangle btnBounds;

        public ButtonMenu(string pathToTexture, int sw, int sh){
            texture2d = LoadTexture(pathToTexture);
            screenWidth = sw;
            screenHeight = sh;
            
            height = texture2d.height;
            width = texture2d.width;
            frameHeight = height/3;
            sourceRec = new Rectangle(0, 0, width, frameHeight);
            btnBounds = new Rectangle(screenWidth/2.0f - texture2d.width/2.0f,  screenHeight/2.0f - texture2d.height/3/2.0f, (float)texture2d.width, frameHeight);
        }

        public ButtonMenu(Texture2D t, int sw, int sh){
            texture2d = t;
            screenWidth = sw;
            screenHeight = sh;

            height = texture2d.height;
            width = texture2d.width;
            frameHeight = height/3;
            sourceRec = new Rectangle(0, 0, width, frameHeight);
            btnBounds = new Rectangle(screenWidth/2.0f - texture2d.width/2.0f,  screenHeight/2.0f - texture2d.height/3/2.0f, (float)texture2d.width, frameHeight);
        }

        public void SetPosition(int x, int y){
            btnBounds = new Rectangle(x,y, (float)texture2d.width, frameHeight);
        }

        public void Draw(){
            sourceRec.y = frameHeight * btnState;
            DrawTextureRec(texture2d, sourceRec, new Vector2 (btnBounds.x, btnBounds.y), WHITE);
        }
        
    }

    class Program
    {
        public static void Main()
        {
            const int screenWidth = 480;
            const int screenHeight = 320;
            string label = "PotatoGames";

            int currentBtn = 0;

            Raylib.InitWindow(screenWidth, screenHeight, "PotatoGames");
            
            Texture2D button = LoadTexture("resources/Buttons/button.png"); 

            List<ButtonMenu> buttons = new List<ButtonMenu>();
            buttons.Add(new ButtonMenu(button, screenWidth, screenHeight));
            buttons.Add(new ButtonMenu(button, screenWidth, screenHeight));
            buttons.Add(new ButtonMenu(button, screenWidth, screenHeight));

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetPosition((screenWidth/2) - (int)buttons[i].width/2, (int)buttons[i].height/2 * i + 100);
            }

            Vector2 mousePoint = new Vector2(0.0f, 0.0f);

            SetTargetFPS(60);
    

            while (!Raylib.WindowShouldClose())
                {

                for (int i = 0; i < buttons.Count; i++)
                {   
                    if(i == currentBtn){
                        buttons[i].btnState = 1;
                    } else {
                        buttons[i].btnState = 0;
                    }
                }



                if (IsKeyPressed(KEY_UP)){
                    currentBtn = currentBtn == -1 ? currentBtn : currentBtn-1;
                }
                if (IsKeyPressed(KEY_DOWN)){
                    currentBtn = currentBtn == buttons.Count-1 ? currentBtn : currentBtn+1;
                }
                if(IsKeyPressed(KEY_ENTER)){
                    label += currentBtn.ToString();
                }
                
                Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);

                    Raylib.DrawText(label, 12, 12, 20, Color.BLACK);

                    Console.WriteLine(currentBtn);

                    for (int i = 0; i < buttons.Count; i++)
                    {   
                        buttons[i].Draw();
                    }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

    }
}
