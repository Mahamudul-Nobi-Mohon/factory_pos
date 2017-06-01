using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace FactoryModule
{
    public partial class AllReport : Form
    {
        public AllReport()
        {
            InitializeComponent();
        }

        string ttpath = FactoryMainBody.ReportName;
        ReportDocument cryRpt = new ReportDocument();
        private void AllReport_Load(object sender, EventArgs e)
        {
            cryRpt = ShowReport.RReport(ttpath);
            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.RefreshReport();
        }

        private void AllReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            cryRpt.Close();
            cryRpt.Dispose();
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.Dispose();
            crystalReportViewer1 = null;
        }
    }
}
