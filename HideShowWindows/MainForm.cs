using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;               // For prcesss related information
using System.Runtime.InteropServices;   // For DLL importing 
using System.IO;

namespace HideShowWindows
{
    public partial class HideShow : Form
    {
        public HideShow()
        {
            InitializeComponent();
            ProgListHandle = new System.Collections.ArrayList();
            ProgListHiddenHandle = new System.Collections.ArrayList();
        }

        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);


        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            int _left;
            int _top;
            int _right;
            int _bottom;

            public RECT(global::System.Drawing.Rectangle rectangle)
                : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
            {
            }
            public RECT(int left, int top, int right, int bottom)
            {
                _left = left;
                _top = top;
                _right = right;
                _bottom = bottom;
            }

            public int X
            {
                get { return Left; }
                set { Left = value; }
            }
            public int Y
            {
                get { return Top; }
                set { Top = value; }
            }
            public int Left
            {
                get { return _left; }
                set { _left = value; }
            }
            public int Top
            {
                get { return _top; }
                set { _top = value; }
            }
            public int Right
            {
                get { return _right; }
                set { _right = value; }
            }
            public int Bottom
            {
                get { return _bottom; }
                set { _bottom = value; }
            }
            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = value - Top; }
            }
            public int Width
            {
                get { return Right - Left; }
                set { Right = value + Left; }
            }
            public global::System.Drawing.Point Location
            {
                get { return new global::System.Drawing.Point(Left, Top); }
                set
                {
                    Left = value.X;
                    Top = value.Y;
                }
            }
            public global::System.Drawing.Size Size
            {
                get { return new global::System.Drawing.Size(Width, Height); }
                set
                {
                    Right = value.Width + Left;
                    Bottom = value.Height + Top;
                }
            }

            public global::System.Drawing.Rectangle ToRectangle()
            {
                return new global::System.Drawing.Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
            public static global::System.Drawing.Rectangle ToRectangle(RECT Rectangle)
            {
                return Rectangle.ToRectangle();
            }
            public static RECT FromRectangle(global::System.Drawing.Rectangle Rectangle)
            {
                return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
            }

            public static implicit operator global::System.Drawing.Rectangle(RECT Rectangle)
            {
                return Rectangle.ToRectangle();
            }
            public static implicit operator RECT(global::System.Drawing.Rectangle Rectangle)
            {
                return new RECT(Rectangle);
            }
            public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
            {
                return Rectangle1.Equals(Rectangle2);
            }
            public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
            {
                return !Rectangle1.Equals(Rectangle2);
            }

            public override string ToString()
            {
                return "{Left: " + Left + "; " + "Top: " + Top + "; Right: " + Right + "; Bottom: " + Bottom + "}";
            }

            public bool Equals(RECT Rectangle)
            {
                return Rectangle.Left == Left && Rectangle.Top == Top && Rectangle.Right == Right && Rectangle.Bottom == Bottom;
            }
            public override bool Equals(object Object)
            {
                if (Object is RECT)
                {
                    return Equals((RECT)Object);
                }
                else if (Object is Rectangle)
                {
                    return Equals(new RECT((global::System.Drawing.Rectangle)Object));
                }

                return false;
            }

            public override int GetHashCode()
            {
                return Left.GetHashCode() ^ Right.GetHashCode() ^ Top.GetHashCode() ^ Bottom.GetHashCode();
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;

            public WINDOWINFO(Boolean? filler)
                : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            {
                cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
            }

        }



        private void GetProgs_Click(object sender, EventArgs e)
        {
            FillProgsList();
        }

        private void FillProgsList()
        {
            int hWnd;
            Process[] processRunning = Process.GetProcesses();
            ProgList.Items.Clear();
            ProgListHandle.Clear();
            foreach (Process pr in processRunning)
            {
                hWnd = pr.MainWindowHandle.ToInt32();
                if (pr.HandleCount != 0 && hWnd != 0)
                {
                    ProgList.Items.Add(pr.ProcessName + "    ----    " + pr.MainWindowTitle);
                    ProgListHandle.Add(hWnd);

                    //                     WINDOWINFO info = new WINDOWINFO();
                    //                     info.cbSize = (uint)Marshal.SizeOf(info);
                    //                     GetWindowInfo(Handle, ref info);
                }


                //                 if (pr.ProcessName == "notepad")
                //                 {
                //                     hWnd = pr.MainWindowHandle.ToInt32();
                //                     ShowWindow(hWnd, SW_HIDE);
                //                 }
            }
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            if (ProgList.SelectedItem == null)
            {
                MessageBox.Show("Not process selected.");
                return;
            }

//            DialogResult result1 = MessageBox.Show("Hide: " + ProgList.SelectedItem, "Important Question", MessageBoxButtons.YesNo);
//            if (result1==DialogResult.Yes)
            {
                int hWnd = (int)ProgListHandle[ProgList.SelectedIndex];
                ShowWindow(hWnd, SW_HIDE);

                ProgListHidden.Items.Add(ProgList.SelectedItem);
                ProgListHiddenHandle.Add(hWnd);
                ProgListHidden.SelectedIndex = ProgListHidden.Items.Count - 1;

                ProgListHandle.RemoveAt(ProgList.SelectedIndex);
                ProgList.Items.Remove(ProgList.SelectedItem);

                SerializeHiddenDialogs();

            }
        }

        private void SerializeHiddenDialogs()
        {
            List<HiddenDialog> hiddenDialogs = new List<HiddenDialog>();
            for (int i = 0; i < ProgListHidden.Items.Count; i++)
            {
                hiddenDialogs.Add(new HiddenDialog((string)ProgListHidden.Items[i], (int)ProgListHiddenHandle[i]));
            }
            ObjectToSerialize objectToSerialize = new ObjectToSerialize();
            objectToSerialize.HiddenDialogs = hiddenDialogs;
            Serializer serializer = new Serializer();
            serializer.SerializeObject(tempFilename, objectToSerialize);
        }
        private void Show_Click(object sender, EventArgs e)
        {
            if (ProgListHidden.SelectedItem == null)
            {
                MessageBox.Show("Not process selected.");
                return;
            }

//            DialogResult result1 = MessageBox.Show("Show: " + ProgList.SelectedItem, "Important Question", MessageBoxButtons.YesNo);
//            if (result1 == DialogResult.Yes)
            {
                int hWnd = (int)ProgListHiddenHandle[ProgListHidden.SelectedIndex];
                ShowWindow(hWnd, SW_SHOWNORMAL);

                ProgList.Items.Add(ProgListHidden.SelectedItem);
                ProgListHandle.Add(hWnd);
                ProgList.SelectedIndex = ProgList.Items.Count - 1;

                ProgListHiddenHandle.RemoveAt(ProgListHidden.SelectedIndex);
                ProgListHidden.Items.Remove(ProgListHidden.SelectedItem);

                SerializeHiddenDialogs();
            }
        }

        private void HideShow_Load(object sender, EventArgs e)
        {
            FillProgsList();

            string tempPath = Environment.GetEnvironmentVariable("Temp");
            tempFilename = System.IO.Path.Combine(tempPath, "tempOutPutSave");

            if (System.IO.File.Exists(tempFilename))
            {
                List<HiddenDialog> hiddenDialogs = new List<HiddenDialog>();

                ObjectToSerialize objectToSerialize = new ObjectToSerialize();
                objectToSerialize.HiddenDialogs = hiddenDialogs;
                Serializer serializer = new Serializer();
                objectToSerialize = serializer.DeSerializeObject(tempFilename);
                hiddenDialogs = objectToSerialize.HiddenDialogs;
                foreach (HiddenDialog h in hiddenDialogs)
                {
                    ProgListHidden.Items.Add(h.getTitle());
                    ProgListHiddenHandle.Add(h.gethWnd());
                }
            }
        }

        
        private void HideShow_Resize(object sender, EventArgs e)
        {
           if (FormWindowState.Minimized == WindowState)
           {
              this.notifyIcon.Visible = true;
              Hide();
           }
        }

        private void HideShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {           
           Show();
           WindowState = FormWindowState.Normal;
           this.notifyIcon.Visible = false;
        }

        private void ProgListHidden_DoubleClick(object sender, EventArgs e)
        {
            if (ProgListHidden.SelectedItem != null)

                if (ProgListHidden.SelectedItem.ToString().Length != 0)
                {
                    Show_Click(sender, e);
                }                    
        }

        private void ProgList_DoubleClick(object sender, EventArgs e)
        {
            if (ProgList.SelectedItem != null)

                if (ProgList.SelectedItem.ToString().Length != 0)
                {
                    Hide_Click(sender, e);
                }   
        }
    }
}
