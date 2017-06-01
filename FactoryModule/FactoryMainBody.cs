using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryModule
{
    public partial class FactoryMainBody : Form
    {
        public FactoryMainBody()
        {
            InitializeComponent();
        }

        public static string ReportName = "";
        private void createUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnitType aunittype = new UnitType();
            aunittype.ShowDialog();
        }

        private void createCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRawCategory aaddrawcategory = new AddRawCategory();
            aaddrawcategory.ShowDialog();
        }

        private void addRawMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRawMaterial aaddrawmaterial = new AddRawMaterial();
            aaddrawmaterial.ShowDialog();
        }

        private void createFloorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFloor aaddfloor = new AddFloor();
            aaddfloor.ShowDialog();
        }
        private void rawMaterialListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportName = "RawMaterialListReport.rpt";
            AllReport aallreport = new AllReport();
            aallreport.Text = "Raw Material List";
            aallreport.ShowDialog();
        }

        private void addSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSupplier aaddsupplier = new AddSupplier();
            aaddsupplier.ShowDialog();
        }

        private void createSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSection aaddsection = new AddSection();
            aaddsection.ShowDialog();
        }

        private void purchaseRawMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchaseRawMaterial apurrawmat = new PurchaseRawMaterial();
            apurrawmat.ShowDialog();
        }

        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchaseReport apurchasereport = new PurchaseReport();
            apurchasereport.ShowDialog();
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportName = "RawMaterialStockReport.rpt";
            AllReport aallreport = new AllReport();
            aallreport.Text = "Raw Material Stock Report";
            aallreport.ShowDialog();
        }

        private void consumptionRawMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawMaterialConsumption arawmaterialconsumption = new RawMaterialConsumption();
            arawmaterialconsumption.ShowDialog();
        }

        private void consumptionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsumptionReport aconsumptionreport = new ConsumptionReport();
            aconsumptionreport.ShowDialog();
        }

        private void createFinnishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFinishedGoods aaddfinishedgoods = new AddFinishedGoods();
            aaddfinishedgoods.ShowDialog();
        }

        private void receiveFinishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiveFinishedGoods areceivefinishedgoods = new ReceiveFinishedGoods();
            areceivefinishedgoods.ShowDialog();
        }

        private void receivedGoodsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiveFinishedGoodsReport areceivefinishedgoodsreport = new ReceiveFinishedGoodsReport();
            areceivefinishedgoodsreport.ShowDialog();
        }

        private void finishedGoodsStockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportName = "FinishedGoodsStockReport.rpt";
            AllReport aallreport = new AllReport();
            aallreport.Text = "Finished Goods Stock Report";
            aallreport.ShowDialog();
        }

        private void finishedGoodsDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishedGoodsDistribution afinishedgoodsdistribution = new FinishedGoodsDistribution();
            afinishedgoodsdistribution.ShowDialog();
        }

        private void createDistributionAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDistributionArea aadddistributionarea = new AddDistributionArea();
            aadddistributionarea.ShowDialog();
        }

        private void finishedGoodsDistributionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FinishedGoodsDistributionReport afingooddistreport = new FinishedGoodsDistributionReport();
            afingooddistreport.ShowDialog();
        }
    }
}
