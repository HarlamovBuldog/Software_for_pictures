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

            // Add handler to handle the exception raised by additional threads
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.ThreadException +=
                new ThreadExceptionEventHandler(
                    handler.Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Logger.Info("Приложение успешно завершено!");
        }

        static void CurrentDomain_UnhandledException
        (object sender, UnhandledExceptionEventArgs e)
        {// All exceptions thrown by additional threads are handled in this method

            ShowExceptionDetails(e.ExceptionObject as Exception);

            // Suspend the current thread for now to stop the exception from throwing.
            //Thread.CurrentThread.Suspend();
        }

        static void ShowExceptionDetails(Exception Ex)
        {
            Logger.Fatal(Ex);
            Logger.Info("Приложение завершено c ошибкой!");
            // Do logging of exception details
            MessageBox.Show(Ex.Message, Ex.TargetSite.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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

            Logger.Fatal(errorMessage);
            Logger.Info("Приложение завершено c ошибкой!");

            return MessageBox.Show(errorMessage,
                "Application Error",
                MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    } // End ThreadExceptionHandler
}
