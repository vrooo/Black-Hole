using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Windows;

namespace BlackHole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const float START_CAM_DIST = 100.0f;
        const float MIN_CAM_DIST = 5.0f, MAX_CAM_DIST = 1000000.0f;
        float camDist = START_CAM_DIST;
        float M = 10;

        RenderManager renderManager;
        Camera camera;
        Quad quad;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            camera = new OrbitCamera(0.0f, 0.0f, -camDist);
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
                renderManager.BindFloat(M, "M");
                renderManager.BindFloat(camDist, "dist");

                renderManager.Render(quad);

                control.SwapBuffers();
            }
        }

        private void Refresh()
        {
            GLMain.Invalidate();
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
                //if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Right))
                //{
                //    camera.Translate(diffX * camera.MoveSpeed, -diffY * camera.MoveSpeed, 0);
                //}
                Refresh();
            }
            prevLocation = e.Location;
        }

        private void OnMouseScroll(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            camDist = -renderManager.Camera.Position.Z - e.Delta * camera.ZoomSpeed;
            if (camDist < MIN_CAM_DIST)
                camDist = MIN_CAM_DIST;
            else if (camDist > MAX_CAM_DIST)
                camDist = MAX_CAM_DIST;
            renderManager.Camera.Position = new Vector3(0.0f, 0.0f, -camDist);
            Refresh();
        }
        #endregion
    }
}
