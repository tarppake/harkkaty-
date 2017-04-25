using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace harkkatyo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage1 : Page
    {
        //liikuteltava hahmo
        private Pelihahmo pelihahmo;

        //seinät
        private Palikka palikka;
        //lattia
        private lattia lattia;

        // näppäinten arvot ylös/alas
        private bool LeftPressed;
        private bool RightPressed;
        private bool Uppressed;
        private bool Downpressed;

        //seinälista
        private List<Point> builder;
        private Point point;

        //pelilloopi
        private DispatcherTimer timer;

        public object SpriteSheetOffset { get; private set; }
        public double LocationY { get; private set; }
        public double LocationX { get; private set; }

        public GamePage1()
        {
            this.InitializeComponent();

            //pelihahmo taustaan
            pelihahmo = new Pelihahmo
            {
                LocationX = Tausta.Width / 2,
                LocationY = Tausta.Height / 2
            };
            Tausta.Children.Add(pelihahmo);

            //ylaseinä taustaan
            palikka = new Palikka
            {
                LocationX = 1,
                LocationY = 1
            };
            Tausta.Children.Add(palikka);

            //näppäimet alas/ylös
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            //pelilooppi
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Tick += Timer_Tick;
            timer.Start();

            //builder
            builder = new List<Point>();
            point = new Point();
            
            for (int j = 1; j<= 13; j++) {

                    for (int i = 1; i <= 17; i++)
                    {

                        point = new Point
                        {
                            X = 46 * i,
                            Y = 46 * j
                        };

                    builder.Add(point);

                    }
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            if (LeftPressed) pelihahmo.MoveLeft();
            if (RightPressed) pelihahmo.MoveRight();
            if (Uppressed) pelihahmo.MoveUp();
            if (Downpressed) pelihahmo.MoveDown();

            //päivitä sijainti
            pelihahmo.SetLocation();
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    LeftPressed = false;
                    break;
                case VirtualKey.Right:
                    RightPressed = false;
                    break;
                case VirtualKey.Up:
                    Uppressed = false;
                    break;
                case VirtualKey.Down:
                    Downpressed = false;
                    break;
            }
        }
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    LeftPressed = true;
                    break;
                case VirtualKey.Right:
                    RightPressed = true;
                    break;
                case VirtualKey.Up:
                    Uppressed = true;
                    break;
                case VirtualKey.Down:
                    Downpressed = true;
                    break;
            }
        }

        //takaisin näppäin
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null) return;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }

        }



        //seinä colliding
        private void CheckCollicion()
        {
            //gets rects
            Rect HRect = new Rect(
                pelihahmo.LocationX, pelihahmo.LocationY, pelihahmo.ActualHeight, pelihahmo.ActualWidth
                );
            Rect SRect = new Rect(
               palikka.LocationX, palikka.LocationY, palikka.ActualHeight, palikka.ActualWidth);

            //meneekö päällekkäi?
            HRect.Intersect(SRect);
            if (!HRect.IsEmpty)
            {
                pelihahmo.hit = true;
            }
        }



        //KARTAN LUKEMINEN TIEDOSTOSTA
        //ei varmana tuu näin..
        private async void Loadmap()
        {
            StorageFolder folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            StorageFile file =
                await folder.GetFileAsync("maze1.txt");
            var stream = await file.OpenAsync(FileAccessMode.Read);

            string[] lines = System.IO.File.ReadAllLines(@"\maze1.txt");
            foreach (string line in lines)
            {
                if 

            }

        }

         
    }
}
    

   

