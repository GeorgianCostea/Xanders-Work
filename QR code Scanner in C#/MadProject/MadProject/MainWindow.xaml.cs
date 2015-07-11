using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge.Video.DirectShow;
using System.Windows.Interop;
using System.Windows.Threading;
using AForge.Video;
using AForge.Imaging;
using System.Drawing;
using com.google.zxing.common;
using com.google.zxing;
using com.google.zxing.qrcode;



namespace MadProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CamControl camControl;
        QRProcess process;

        public MainWindow()
        {
            camControl = new CamControl();
            InitializeComponent();
            RefreshCombo();
            process = new QRProcess();
            process.ResultFound += new EventHandler<ProcessArgs>(process_ResultFound);
            process.Start();
        }

        

        void process_ResultFound(object sender, ProcessArgs e)
        {
            txtCode.Dispatcher.Invoke(new Action(delegate()
            {
                txtCode.Text = e.Result;
                // camControl.SelectedDevice.SignalToStop();
            }));
        }

        public void RefreshCombo()
        {
            lstDevices.Items.Clear();
            for (int i = 0; i < camControl.Devices.Count; i++)
            {
                lstDevices.Items.Add(camControl.Devices[i].Name);
            }
        }

        void processor_NewTargetPosition(System.Drawing.Bitmap image)
        {
            IntPtr hBitMap = image.GetHbitmap();
            BitmapSource bmaps = Imaging.CreateBitmapSourceFromHBitmap(hBitMap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            bmaps.Freeze();

            Dispatcher.Invoke((Action)(() =>
            {
                pictureBoxMain.Source = bmaps;
            }), DispatcherPriority.Render, null);



        }

        //Reference the GDI DeleteObject method.
        [System.Runtime.InteropServices.DllImport("GDI32.dll")]
        public static extern bool DeleteObject(IntPtr objectHandle);

        void captureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            using (UnmanagedImage uimage = UnmanagedImage.FromManagedImage(eventArgs.Frame))
            {
                try
                {
                    using (Bitmap image = uimage.ToManagedImage())
                    {
                        IntPtr hBitMap = image.GetHbitmap();
                        try
                        {
                            BitmapSource bmaps = Imaging.CreateBitmapSourceFromHBitmap(hBitMap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            bmaps.Freeze();
                            Dispatcher.Invoke((Action)(() =>
                            {
                                pictureBoxMain.Source = bmaps;
                            }), DispatcherPriority.Render, null);

                            process.NewBitmap(image);
                        }
                        finally
                        {
                            DeleteObject(hBitMap);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (camControl.SelectedDevice.IsRunning)
            {
                camControl.SelectedDevice.SignalToStop();
            }

            process.Stop();
        }

        private void lstDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (camControl.SelectedDevice.IsRunning)
            {
                camControl.SelectedDevice.SignalToStop();
            }

            camControl.SetCamera(lstDevices.SelectedIndex);
            camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(captureDevice_NewFrame);

            camControl.SelectedDevice.Start();
        }
    }
}
