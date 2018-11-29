using System.Threading;
using System.Windows.Forms;

namespace PicturesSoft
{
    public class DialogInvoker
    {
        public OpenFileDialog InvokeDialog;
        private Thread InvokeThread;
        private DialogResult InvokeResult;

        public DialogInvoker()
        {
            InvokeDialog = new OpenFileDialog();
            InvokeThread = new Thread(new ThreadStart(InvokeMethod));
            InvokeThread.SetApartmentState(ApartmentState.STA);
            InvokeResult = DialogResult.None;
        }

        public DialogInvoker(string openFileDialogFilter)
        {
            InvokeDialog = new OpenFileDialog();
            InvokeDialog.Filter = openFileDialogFilter;
            InvokeThread = new Thread(new ThreadStart(InvokeMethod));
            InvokeThread.SetApartmentState(ApartmentState.STA);
            InvokeResult = DialogResult.None;
        }      

        public DialogResult Invoke()
        {
            InvokeThread.Start();
            InvokeThread.Join();
            return InvokeResult;
        }

        private void InvokeMethod()
        {
            InvokeResult = InvokeDialog.ShowDialog();
        }
    }
}
