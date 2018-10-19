using System;
using System.Threading;
using System.Windows.Forms;

namespace PicturesSoft
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            // Subscribe to thread (unhandled) exception events
            ThreadExceptionHandler handler =
                new ThreadExceptionHandler();

            Application.ThreadException +=
                new ThreadExceptionEventHandler(
                    handler.Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        /*
        //all the exception will be catched and handled in this delegated method.
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //log you error
            string path = @"D:\Work\ErrorLog.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                using(var tw = new StreamWriter(path, true))
                    tw.WriteLine(e.Exception.Message);
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                    tw.WriteLine(e.Exception.Message);
            }

            Console.WriteLine(e.Exception.Message);
        }
        */
    }

    /// 
    /// Handles a thread (unhandled) exception.
    /// 
    internal class ThreadExceptionHandler
    {
        /// 
        /// Handles the thread exception.
        /// 
        public void Application_ThreadException(
            object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                // Exit the program if the user clicks Abort.
                DialogResult result = ShowThreadExceptionDialog(
                    e.Exception);

                if (result == DialogResult.Abort)
                    Application.Exit();
            }
            catch
            {
                // Fatal error, terminate program
                try
                {
                    MessageBox.Show("Fatal Error",
                        "Fatal Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        /// 
        /// Creates and displays the error message.
        /// 
        private DialogResult ShowThreadExceptionDialog(Exception ex)
        {
            string errorMessage =
                "Unhandled Exception:\n\n" +
                ex.Message + "\n\n" +
                ex.GetType() +
                "\n\nStack Trace:\n" +
                ex.StackTrace;

            return MessageBox.Show(errorMessage,
                "Application Error",
                MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    } // End ThreadExceptionHandler
}
