using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryModule
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FactoryMainBody());
            //FH.Closed += (s, args) => this.Close();
            //FH.userToolStripMenuItem.Visible = false;
            //FH.Show();
            //Application.Run(new UnitType());
            //Application.Run(new PurchaseRawMaterial());
            //Application.Run(new PurchaseReport());
            //Application.Run(new ReceiveFinishedGoods());
            //Application.Run(new FinishedGoodsDistribution());
        }
    }
}
