using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonFileEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BonFile bon;
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "Bon File";
            ofd.Filter = "Bon Files|*.bon|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bon = new BonFile(ofd.FileName);
                this.Text = ofd.FileName + "   -   " + bon.Version + "    -    " + "Bon Editor";
                FirstStageCount.Text = bon.count.ToString();
                SecondStageCount.Text = bon.count2.ToString();
                ThirdsStageCount.Text = bon.count3.ToString();
                HeaderUnk0.Text = bon.unknown0.ToString();
                HeaderUnk1.Text = bon.unknown.ToString();
                HeaderSpaceControl.Text = bon.spacecontrol.ToString();
                FirstStagesGrid.Rows.Clear();
                SecondStagesGrid.Rows.Clear();
                ThirdStagesGrid.Rows.Clear();
                for (int i = 0; i < bon.FirstStages.Count; i++)
                {
                    FirstStagesGrid.Rows.Add(bon.FirstStages[i].BoneName);
                }
                for (int i = 0; i < bon.SecondStagesAlt.Count; i++)
                {
                    SecondStagesAltGrid.Rows.Add(bon.SecondStagesAlt[i].BoneName);
                }
                for (int i = 0; i < bon.GfxAnimations.Count; i++)
                {
                    ThirdStagesGrid.Rows.Add(bon.GfxAnimations[i].BoneName);
                }
            }
        }
        private void SaveFile(object sender, System.EventArgs e)
        {
            if (bon != null)
            {
                SaveFileDialog ofd = new SaveFileDialog();
                ofd.FileName = "Bon File";
                ofd.Filter = "Bon Files|*.bon|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    bon.Save(ofd.FileName, bon.Version);
                }
            }
        }
        private void FirstStagesGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (FirstStagesGrid.CurrentRow == null) return;
            if (FirstStagesGrid.CurrentRow.Index == -1) return;
            int i = FirstStagesGrid.CurrentRow.Index;
            FirstStageBoneName.Text = bon.FirstStages[i].BoneName;
            FirstStageID.Text = bon.FirstStages[i].ID[0].ToString();
            FirstStageID1.Text = bon.FirstStages[i].ID[1].ToString();
            FirstStageExtras.Text = bon.FirstStages[i].Extras.Count.ToString();
            FirstStageExtras2.Text = bon.FirstStages[i].Extras2.ToString();
            FirstStagePoint0X.Text = bon.FirstStages[i].Points[0].X.ToString();
            FirstStagePoint1X.Text = bon.FirstStages[i].Points[1].X.ToString();
            FirstStagePoint2X.Text = bon.FirstStages[i].Points[2].X.ToString();
            FirstStagePoint3X.Text = bon.FirstStages[i].Points[3].X.ToString();
            FirstStagePoint4X.Text = bon.FirstStages[i].Points[4].X.ToString();
            FirstStagePoint5X.Text = bon.FirstStages[i].Points[5].X.ToString();
            FirstStagePoint6X.Text = bon.FirstStages[i].Points[6].X.ToString();
            FirstStagePoint7X.Text = bon.FirstStages[i].Points[7].X.ToString();
            FirstStagePoint0Y.Text = bon.FirstStages[i].Points[0].Y.ToString();
            FirstStagePoint1Y.Text = bon.FirstStages[i].Points[1].Y.ToString();
            FirstStagePoint2Y.Text = bon.FirstStages[i].Points[2].Y.ToString();
            FirstStagePoint3Y.Text = bon.FirstStages[i].Points[3].Y.ToString();
            FirstStagePoint4Y.Text = bon.FirstStages[i].Points[4].Y.ToString();
            FirstStagePoint5Y.Text = bon.FirstStages[i].Points[5].Y.ToString();
            FirstStagePoint6Y.Text = bon.FirstStages[i].Points[6].Y.ToString();
            FirstStagePoint7Y.Text = bon.FirstStages[i].Points[7].Y.ToString();
            FirstStagePoint0Z.Text = bon.FirstStages[i].Points[0].Z.ToString();
            FirstStagePoint1Z.Text = bon.FirstStages[i].Points[1].Z.ToString();
            FirstStagePoint2Z.Text = bon.FirstStages[i].Points[2].Z.ToString();
            FirstStagePoint3Z.Text = bon.FirstStages[i].Points[3].Z.ToString();
            FirstStagePoint4Z.Text = bon.FirstStages[i].Points[4].Z.ToString();
            FirstStagePoint5Z.Text = bon.FirstStages[i].Points[5].Z.ToString();
            FirstStagePoint6Z.Text = bon.FirstStages[i].Points[6].Z.ToString();
            FirstStagePoint7Z.Text = bon.FirstStages[i].Points[7].Z.ToString();
            FirstStagePointUnk0.Text = bon.FirstStages[i].unknown0.ToString();
            FirstStagePointUnk1.Text = bon.FirstStages[i].unknown1.ToString();
            FirstStagePointUnk2.Text = bon.FirstStages[i].unknown2.ToString();
            FirstStagePointUnk3.Text = bon.FirstStages[i].unknown3.ToString();
            FirstStagePointUnk4.Text = bon.FirstStages[i].unknown4.ToString();
            FirstStagePointUnk5.Text = bon.FirstStages[i].unknown5.ToString();
            FirstStagePointUnk6.Text = bon.FirstStages[i].unknown6.ToString();
            FirstStagePointUnk7.Text = bon.FirstStages[i].unknown7.ToString();
        }

        private void FirstStagePoint0X_Leave(object sender, EventArgs e)
        {
            if (FirstStagesGrid.CurrentRow == null) return;
            if (FirstStagesGrid.CurrentRow.Index == -1) return;
            int tag = Convert.ToInt32((sender as Control).Tag.ToString());
            var indexes = FirstStagesGrid.SelectedRows.Cast<DataGridViewRow>().Select(t => t.Index).OrderByDescending(y => y);
            foreach (var i in indexes)
            {
                switch (tag)
                {
                    case 1:
                        bon.FirstStages[i].Points[0].X = Convert.ToDecimal(FirstStagePoint0X.Text);
                        break;
                    case 2:
                        bon.FirstStages[i].Points[0].Y = Convert.ToDecimal(FirstStagePoint0Y.Text);
                        break;
                    case 3:
                        bon.FirstStages[i].Points[0].Z = Convert.ToDecimal(FirstStagePoint0Z.Text);
                        break;
                    case 4:
                        bon.FirstStages[i].unknown0 = Convert.ToInt32(FirstStagePointUnk0.Text);
                        break;
                    case 5:
                        bon.FirstStages[i].Points[1].X = Convert.ToDecimal(FirstStagePoint1X.Text);
                        break;
                    case 6:
                        bon.FirstStages[i].Points[1].Y = Convert.ToDecimal(FirstStagePoint1Y.Text);
                        break;
                    case 7:
                        bon.FirstStages[i].Points[1].Z = Convert.ToDecimal(FirstStagePoint1Z.Text);
                        break;
                    case 8:
                        bon.FirstStages[i].unknown1 = Convert.ToInt32(FirstStagePointUnk1.Text);
                        break;
                    case 9:
                        bon.FirstStages[i].Points[2].X = Convert.ToDecimal(FirstStagePoint2X.Text);
                        break;
                    case 10:
                        bon.FirstStages[i].Points[2].Y = Convert.ToDecimal(FirstStagePoint2Y.Text);
                        break;
                    case 11:
                        bon.FirstStages[i].Points[2].Z = Convert.ToDecimal(FirstStagePoint2Z.Text);
                        break;
                    case 12:
                        bon.FirstStages[i].unknown2 = Convert.ToInt32(FirstStagePointUnk2.Text);
                        break;
                    case 13:
                        bon.FirstStages[i].Points[3].X = Convert.ToDecimal(FirstStagePoint3X.Text);
                        break;
                    case 14:
                        bon.FirstStages[i].Points[3].Y = Convert.ToDecimal(FirstStagePoint3Y.Text);
                        break;
                    case 15:
                        bon.FirstStages[i].Points[3].Z = Convert.ToDecimal(FirstStagePoint3Z.Text);
                        break;
                    case 16:
                        bon.FirstStages[i].unknown3 = Convert.ToDecimal(FirstStagePointUnk3.Text);
                        break;
                    case 17:
                        bon.FirstStages[i].Points[4].X = Convert.ToDecimal(FirstStagePoint4X.Text);
                        break;
                    case 18:
                        bon.FirstStages[i].Points[4].Y = Convert.ToDecimal(FirstStagePoint4Y.Text);
                        break;
                    case 19:
                        bon.FirstStages[i].Points[4].Z = Convert.ToDecimal(FirstStagePoint4Z.Text);
                        break;
                    case 20:
                        bon.FirstStages[i].unknown4 = Convert.ToDecimal(FirstStagePointUnk4.Text);
                        break;
                    case 21:
                        bon.FirstStages[i].Points[5].X = Convert.ToDecimal(FirstStagePoint5X.Text);
                        break;
                    case 22:
                        bon.FirstStages[i].Points[5].Y = Convert.ToDecimal(FirstStagePoint5Y.Text);
                        break;
                    case 23:
                        bon.FirstStages[i].Points[5].Z = Convert.ToDecimal(FirstStagePoint5Z.Text);
                        break;
                    case 24:
                        bon.FirstStages[i].unknown5 = Convert.ToDecimal(FirstStagePointUnk5.Text);
                        break;
                    case 25:
                        bon.FirstStages[i].Points[6].X = Convert.ToDecimal(FirstStagePoint6X.Text);
                        break;
                    case 26:
                        bon.FirstStages[i].Points[6].Y = Convert.ToDecimal(FirstStagePoint6Y.Text);
                        break;
                    case 27:
                        bon.FirstStages[i].Points[6].Z = Convert.ToDecimal(FirstStagePoint6Z.Text);
                        break;
                    case 28:
                        bon.FirstStages[i].unknown6 = Convert.ToDecimal(FirstStagePointUnk6.Text);
                        break;
                    case 29:
                        bon.FirstStages[i].Points[7].X = Convert.ToDecimal(FirstStagePoint7X.Text);
                        break;
                    case 30:
                        bon.FirstStages[i].Points[7].Y = Convert.ToDecimal(FirstStagePoint7Y.Text);
                        break;
                    case 31:
                        bon.FirstStages[i].Points[7].Z = Convert.ToDecimal(FirstStagePoint7Z.Text);
                        break;
                    case 32:
                        bon.FirstStages[i].unknown7 = Convert.ToDecimal(FirstStagePointUnk7.Text);
                        break;
                }
            }
        }

        private void SecondStagesAltGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (SecondStagesAltGrid.CurrentRow == null) return;
            if (SecondStagesAltGrid.CurrentRow.Index == -1) return;
            int i = SecondStagesAltGrid.CurrentRow.Index;
            SecondStageAltBoneName.Text = bon.SecondStagesAlt[i].BoneName.ToString();
            SecondStageAltID.Text = bon.SecondStagesAlt[i].ID[0].ToString();
            SecondStageAltID1.Text = bon.SecondStagesAlt[i].ID[1].ToString();
            SecondStageAltUnk.Text = bon.SecondStagesAlt[i].Unknown1.ToString();
        }

        private void SecondStageAltBoneName_Leave(object sender, EventArgs e)
        {
            if (SecondStagesAltGrid.CurrentRow == null) return;
            if (SecondStagesAltGrid.CurrentRow.Index == -1) return;
            int tag = Convert.ToInt32((sender as Control).Tag.ToString());
            var indexes = SecondStagesAltGrid.SelectedRows.Cast<DataGridViewRow>().Select(t => t.Index).OrderByDescending(y => y);
            foreach (var i in indexes)
            {
                switch (tag)
                {
                    case 33:
                        bon.SecondStagesAlt[i].BoneName = SecondStageAltBoneName.Text;
                        break;
                    case 34:
                        bon.SecondStagesAlt[i].ID[0] = Convert.ToInt32(SecondStageAltID.Text);
                        break;
                    case 35:
                        bon.SecondStagesAlt[i].ID[1] = Convert.ToInt32(SecondStageAltID1.Text);
                        break;
                    case 36:
                        bon.SecondStagesAlt[i].Unknown1 = Convert.ToInt32(SecondStageAltUnk.Text);
                        break;
                }
            }
        }

        private void FinalStageCords0X_Leave(object sender, EventArgs e)
        {
            if (ThirdStagesGrid.CurrentRow == null) return;
            if (ThirdStagesGrid.CurrentRow.Index == -1) return;
            int tag = Convert.ToInt32((sender as Control).Tag.ToString());
            var indexes = ThirdStagesGrid.SelectedRows.Cast<DataGridViewRow>().Select(t => t.Index).OrderByDescending(y => y);
            foreach (var i in indexes)
            {
                switch (tag)
                {
                    case 37:
                        bon.GfxAnimations[i].Cords[0].X =Convert.ToDecimal(FinalStageCords0X.Text);
                        break;
                    case 38:
                        bon.GfxAnimations[i].Cords[0].Y = Convert.ToDecimal(FinalStageCords0Y.Text);
                        break;
                    case 39:
                        bon.GfxAnimations[i].Cords[0].Z = Convert.ToDecimal(FinalStageCords0Z.Text);
                        break;
                    case 40:
                        bon.GfxAnimations[i].CordsInts[0]= Convert.ToInt32(FinalStageCords0Unk.Text);
                        break;

                    case 41:
                        bon.GfxAnimations[i].Cords[1].X = Convert.ToDecimal(FinalStageCords1X.Text);
                        break;
                    case 42:
                        bon.GfxAnimations[i].Cords[1].Y = Convert.ToDecimal(FinalStageCords1Y.Text);
                        break;
                    case 43:
                        bon.GfxAnimations[i].Cords[1].Z = Convert.ToDecimal(FinalStageCords1Z.Text);
                        break;
                    case 44:
                        bon.GfxAnimations[i].CordsInts[1] = Convert.ToInt32(FinalStageCords1Unk.Text);
                        break;

                    case 45:
                        bon.GfxAnimations[i].Cords[2].X = Convert.ToDecimal(FinalStageCords2X.Text);
                        break;
                    case 46:
                        bon.GfxAnimations[i].Cords[2].Y = Convert.ToDecimal(FinalStageCords2Y.Text);
                        break;
                    case 47:
                        bon.GfxAnimations[i].Cords[2].Z = Convert.ToDecimal(FinalStageCords2Z.Text);
                        break;
                    case 48:
                        bon.GfxAnimations[i].CordsInts[2] = Convert.ToInt32(FinalStageCords2Unk.Text);
                        break;

                    case 49:
                        bon.GfxAnimations[i].Cords[3].X = Convert.ToDecimal(FinalStageCords3X.Text);
                        break;
                    case 50:
                        bon.GfxAnimations[i].Cords[3].Y = Convert.ToDecimal(FinalStageCords3Y.Text);
                        break;
                    case 51:
                        bon.GfxAnimations[i].Cords[3].Z = Convert.ToDecimal(FinalStageCords3Z.Text);
                        break;
                    case 52:
                        bon.GfxAnimations[i].unknown1 = (float)Convert.ToDecimal(FinalStageCords3Unk.Text);
                        break;
                }
            }
        }

        private void ThirsStagesGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (ThirdStagesGrid.CurrentRow == null) return;
            if (ThirdStagesGrid.CurrentRow.Index == -1) return;
            int i = ThirdStagesGrid.CurrentRow.Index;
            FinalStageBoneName.Text = bon.GfxAnimations[i].BoneName;
            FinalStageID.Text = bon.GfxAnimations[i].ID.ToString();
            FinalStageUnk.Text = bon.GfxAnimations[i].unknown0.ToString();
            FinalStageCords0X.Text = bon.GfxAnimations[i].Cords[0].X.ToString();
            FinalStageCords0Y.Text = bon.GfxAnimations[i].Cords[0].Y.ToString();
            FinalStageCords0Z.Text = bon.GfxAnimations[i].Cords[0].Z.ToString();
            FinalStageCords0Unk.Text = bon.GfxAnimations[i].CordsInts[0].ToString();

            FinalStageCords1X.Text = bon.GfxAnimations[i].Cords[1].X.ToString();
            FinalStageCords1Y.Text = bon.GfxAnimations[i].Cords[1].Y.ToString();
            FinalStageCords1Z.Text = bon.GfxAnimations[i].Cords[1].Z.ToString();
            FinalStageCords1Unk.Text = bon.GfxAnimations[i].CordsInts[1].ToString();

            FinalStageCords2X.Text = bon.GfxAnimations[i].Cords[2].X.ToString();
            FinalStageCords2Y.Text = bon.GfxAnimations[i].Cords[2].Y.ToString();
            FinalStageCords2Z.Text = bon.GfxAnimations[i].Cords[2].Z.ToString();
            FinalStageCords2Unk.Text = bon.GfxAnimations[i].CordsInts[2].ToString();

            FinalStageCords3X.Text = bon.GfxAnimations[i].Cords[3].X.ToString();
            FinalStageCords3Y.Text = bon.GfxAnimations[i].Cords[3].Y.ToString();
            FinalStageCords3Z.Text = bon.GfxAnimations[i].Cords[3].Z.ToString();
            FinalStageCords3Unk.Text = bon.GfxAnimations[i].unknown1.ToString();
        }
    }
    public class Convert
    {
        public static decimal ToDecimal(string text)
        {
            try
            {
                return System.Convert.ToDecimal(text);
            }
            catch
            {
                return 0;
            }
        }
        public static int ToInt32(string text)
        {
            try
            {
                return System.Convert.ToInt32(text);
            }
            catch
            {
                return 0;
            }
        }
    }
}
