

using System;
using GodsOfCalamity.Components;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GodsOfCalamity.Entity;
using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace GodsofCalamity.PauseMenu
{
    public class Pause
    {
        public bool playerController;
        public static bool GameIsPaused = false; //setting gamepuse to false 
        public event EventHandler<object> Resuming; //occurs when app resumes
        const string TitleString = " ";
        public object Time { get; private set; } //game time 
        public object ExitButton { get; private set; }
        //public bool gameObject { get; private set; }
        private bool active; //to see if game is active or not\
        Pause timescale;


        //Handles CoreApplication::Resuming. The game app restores the game
        // from a suspended state.
        public static class CoreApplication
        {

        }


        //Pauses the Game
        /*public void Pause()
        {
            TimeSpan.timeScale = 0;
            pauseGame.Play();
            playerController.enabled = false;

            while (true)
            {
                if (!GameIsPaused)
                {
                    UpdateGame();
                }
                Draw();
            }
        } */
        


        //starts the game
        /*void Start()
        {
            Canvas_PauseMenu = Canvas_PauseMenu.GetComponent<Canvas>();
            Button_Pause = Button_Pause.GetComponent<Button>();
            Button_Resume = Button_Resume.GetComponent<Button>();
            Canvas_PauseMenu.enabled = false;
            Button_Resume.enabled = false;
            Button_Resume.gameObject.SetActive(false);
        } */


        //Pauses the Game 
        //void SetPause(int level, float timeRemaining)
        void PauseGame ()
        {
            
            PauseButton.enabled = true;
            //ExitButton.enabled = true;
            PauseButton.enabled = false;
            PauseButton.gameObject(false);
            MenuResume.enabled = true;
            MenuResume.gameObject(true);
            active = true;
            GodsOfCalamity.
        }

        //Resumes the game
        public void ResumeGame()
        {
            
            /* PauseButton.enabled = false;
            //Button_Exit.enabled = false;
            PauseButton.enabled = true;
            PauseButton.gameObject.SetActive(true);
            MenuResume.enabled = false;
            MenuResume.gameObject.SetActive(false);
            active = false;
            //Time.timer = 1; */
        }


        // Update is called once per frame
        /* public void PauseTest()
        {

            if (!active)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }

        } */
    }
}
