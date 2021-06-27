using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BlackHole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        const int SKYBOX_MILKYWAY1 = 1, SKYBOX_MILKYWAY2 = 2;
        const float ZOOM_SPEED = 1e8f;
        const double MASS_MULT = 0.742e-27;

        public const double START_CAM_DIST = 2e11;
        public const double MIN_CAM_DIST = 1, MAX_CAM_DIST = 1e12;
        public const double START_M = 5.984e9;
        public const double MIN_M = 1, MAX_M = 2e10;
        public const double START_MASS = START_M / MASS_MULT;
        public const double MIN_MASS = MIN_M / MASS_MULT, MAX_MASS = MAX_M / MASS_MULT;

        double camDist = START_CAM_DIST;
        public double CameraDistance
        {
            get => camDist;
            set
            {
                camDist = value;
                if (camDist < MIN_CAM_DIST)
                    camDist = MIN_CAM_DIST;
                else if (camDist > MAX_CAM_DIST)
                    camDist = MAX_CAM_DIST;
                if (renderManager != null)
                {
                    renderManager.Camera.Position = new Vector3(0.0f, 0.0f, (float)(-camDist));
                    Refresh();
                }
                OnPropertyChanged();
            }
        }

        public double m = START_M;
        public double M
        {
            get => m;
            set
            {
                m = value;
                mass = m / MASS_MULT;
                Refresh();
                OnPropertyChanged();
                OnPropertyChanged("Mass");
            }
        }
        public double mass = START_MASS;
        public double Mass
        {
            get => mass;
            set
            {
                mass = value;
                m = mass * MASS_MULT;
                Refresh();
                OnPropertyChanged();
                OnPropertyChanged("M");
            }
        }

        RenderManager renderManager;
        Camera camera;
        Quad quad;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            camera = new OrbitCamera(0.0f, 0.0f, (float)(-camDist))
            {
                ZoomSpeed = ZOOM_SPEED
            };
            renderManager = new RenderManager(camera);
            quad = new Quad();
        }

        private void OnRender(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (renderManager != null)
            {
                var control = sender as GLControl;
                control.MakeCurrent();
                renderManager.SetupRender(control.Width, control.Height);
                renderManager.BindFloat((float)M, "M");
                renderManager.BindFloat((float)camDist, "dist");

                switch (ComboSkybox.SelectedIndex)
                {
                    case SKYBOX_MILKYWAY1:
                        renderManager.UseCubeMilkyWay1();
                        break;
                    case SKYBOX_MILKYWAY2:
                        renderManager.UseCubeMilkyWay2();
                        break;
                    default:
                        renderManager.UseCubeColors();
                        break;
                }

                renderManager.Render(quad);

                control.SwapBuffers();
            }
        }

        private void Refresh()
        {
            GLMain.Invalidate();
        }

        private void OnSkyboxSelectionChaged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Refresh();
        }

        #region Mouse
        private System.Drawing.Point prevLocation;
        private void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            (sender as GLControl).Capture = false;
        }

        private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            (sender as GLControl).Capture = true;
        }

        private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((sender as GLControl).Capture)
            {
                float diffX = e.X - prevLocation.X;
                float diffY = e.Y - prevLocation.Y;
                if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Left))
                {
                    camera.Rotate(diffY * camera.RotateSpeed, diffX * camera.RotateSpeed, 0);
                }
                Refresh();
            }
            prevLocation = e.Location;
        }

        private void OnMouseScroll(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CameraDistance = -renderManager.Camera.Position.Z - e.Delta * camera.ZoomSpeed;
        }
        #endregion

        #region Notify property changed
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            var args = new PropertyChangedEventArgs(name);
            if (Application.Current != null)
            {
                foreach (Delegate d in PropertyChanged.GetInvocationList())
                {
                    Application.Current.Dispatcher.Invoke(d, this, args);
                }
            }
        }
        #endregion
    }
}
