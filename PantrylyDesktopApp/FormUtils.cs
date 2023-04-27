using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PantrylyDesktopApp
{
    public static class FormUtils
    {
        // Variables to use for dragging around window
        private static bool isDragging = false;
        private static Point lastCursorPos;

        public static void MakeWindowFormRounded(Form form)
        {
            GraphicsPath path = new GraphicsPath();
            int arcSize = 20;
            Rectangle rect = new Rectangle(0, 0, form.Width, form.Height);
            path.AddArc(rect.X, rect.Y, arcSize, arcSize, 180, 90);
            path.AddArc(rect.X + rect.Width - arcSize, rect.Y, arcSize, arcSize, 270, 90);
            path.AddArc(rect.X + rect.Width - arcSize, rect.Y + rect.Height - arcSize, arcSize, arcSize, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - arcSize, arcSize, arcSize, 90, 90);
            form.Region = new Region(path);
            form.FormBorderStyle = FormBorderStyle.None;
        }

        public static void MakeButtonRounded(Button button)
        {
            // make button 1 circular.
            GraphicsPath buttonPath = new GraphicsPath();
            buttonPath.AddEllipse(0, 0, button.Width, button.Height);
            button.Region = new Region(buttonPath);
        }

        public static void AddDraggableWindowTitle(Panel panel)
        {
            panel.MouseDown += new MouseEventHandler(WindowTitle_MouseDown);
            panel.MouseMove += new MouseEventHandler(WindowTitle_MouseMove);
            panel.MouseUp += new MouseEventHandler(WindowTitle_MouseUp);
        }

        private static void WindowTitle_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursorPos = Cursor.Position;
        }

        private static void WindowTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point deltaCursorPos = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                Form form = Form.ActiveForm;
                form.Location = new Point(form.Location.X + deltaCursorPos.X, form.Location.Y + deltaCursorPos.Y);
                lastCursorPos = Cursor.Position;
            }
        }

        private static void WindowTitle_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        public static void AddCloseButton(Control closeButtonControl)
        {
            closeButtonControl.Click += new EventHandler(CloseButton_Click);
        }

        private static void CloseButton_Click(object sender, EventArgs e)
        {
            Form form = Form.ActiveForm;
            form.Close();
        }

        public static void AddMinimizeButton(Control minimizeButtonControl)
        {
            minimizeButtonControl.Click += new EventHandler(MinimizeButton_Click);
        }

        private static void MinimizeButton_Click(object sender, EventArgs e)
        {
            Form form = Form.ActiveForm;
            form.WindowState = FormWindowState.Minimized;
        }
    }
}
