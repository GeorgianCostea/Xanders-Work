using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video.DirectShow;

namespace MadProject
{
    /// <summary>
    /// Class to control the cameras of the system.
    /// </summary>
    class CamControl
    {
        private int selectedFrameSize;
        private int selectedFps;
        private VideoCaptureDevice selectedDevice;
        public FilterInfoCollection Devices { get; private set; }
        public Dictionary<int, string> FrameSizes { get; private set; }
        public List<int> Fps;

        /// Get the selected device.
       
        public VideoCaptureDevice SelectedDevice
        {
            get
            {
                return selectedDevice;
            }
            private set
            {
                selectedDevice = value;
                RefreshFrameSize();
            }
        }

       
        /// Initializes a new instance of the CamControl
        
        public CamControl()
        {
            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // by default select the first one
            SetCamera(0);
        }





        /// Sets the active camera.
        public void SetCamera(int index)
        {
            if (Devices.Count < index)
            {
                throw new IndexOutOfRangeException("There is no device with index " + index);
            }

            SelectedDevice = new VideoCaptureDevice(Devices[index].MonikerString);
        }

        /// Sets the size of the frame.
        public void SetFrameSize(int index)
        {
            if (FrameSizes.Count < index)
            {
                throw new IndexOutOfRangeException("There is no framesize with index " + index);
            }

            selectedFrameSize = index;
            RefreshFps();
            ConfigureCamera();
        }

        /// Sets the FPS of the active camera.
        public void SetFps(int fps)
        {
            if (!Fps.Contains(fps))
            {
                throw new IndexOutOfRangeException("There is no fps like " + fps);
            }

            selectedFps = fps;
            ConfigureCamera();
        }

        /// Refreshes the size of the frame.
        private void RefreshFrameSize()
        {
            this.FrameSizes = new Dictionary<int, string>();
            int i = 0;
            foreach (VideoCapabilities set in SelectedDevice.VideoCapabilities)
            {
                this.FrameSizes.Add(i, String.Format("{0} x {1}", set.FrameSize.Width, set.FrameSize.Height));
                i++;
            }

            selectedFrameSize = 0;
            RefreshFps();
        }

        /// Refreshes the FPS.
        private void RefreshFps()
        {
            int MaxFramerate = selectedDevice.VideoCapabilities[selectedFrameSize].FrameRate;
            Fps = new List<int>();
            for (int i = 1; i < MaxFramerate; i++)
            {
                if (i % 5 == 0)
                {
                    Fps.Add(i);
                }
            }

            selectedFps = Fps.Min();
        }

        /// Configures the camera.
        private void ConfigureCamera()
        {
            SelectedDevice.DesiredFrameSize = SelectedDevice.VideoCapabilities[selectedFrameSize].FrameSize;
            SelectedDevice.DesiredFrameRate = selectedFps;
        }
    }
}
