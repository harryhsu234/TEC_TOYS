using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INNOLUX_DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 改予部分初始值
            #region 調整日期預設值，帳單日期設定為上月26日至本月底
            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month - 1;
            if (month == 0)
            {
                year--;
                month = 12;
            }
            int day = 26;
            this.ivdt_from.Value = new DateTime(year, month, day);

            now = DateTime.Now;
            year = now.Year;
            month = now.Month + 1; // 抓下個月份
            if (month == 13)
            {
                year++;
                month = 1;
            }
            day = 1;
            // 抓下個月的第一天 -1 天 就會是這個月的最後一天
            this.ivdt_to.Value = (new DateTime(year, month, day)).AddDays(-1);
            #endregion

            #region 填入快速選擇
            ArrayList data = new ArrayList();
            data.Add(new DictionaryEntry("== 請選擇 ==", new string[] { "", "" }));

            data.Add(new DictionaryEntry("台空-群創竹南", new string[] { "65012228", "12800225E1" })); // 65012228	12800225E1
            data.Add(new DictionaryEntry("台空-群創台南", new string[] { "65004382", "12800225E4" }));
            data.Add(new DictionaryEntry("台驊-群創", new string[] { "34804749", "018066" }));

            this.cbFastSelection.DisplayMember = "Key";
            this.cbFastSelection.ValueMember = "Value";
            this.cbFastSelection.DataSource = data;
            #endregion
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                taProg.Clear();
                

                #region 分析UI條件
                taProg.AppendText("整理條件..");
                taProg.AppendText("\n");
                string sWhere = getUICondition();
                #endregion

                #region 撈取T_ACC_CHARGE 資料
                taProg.AppendText("查找資料庫..");
                taProg.AppendText("\n");
                DataTable dt = Model.getInnoInvoice(sWhere);
                #endregion


                #region 寫入EXCEL FILE
                taProg.AppendText("EXCEL 產生..");
                taProg.AppendText(Model.createInnoExcel(dt));
                taProg.AppendText("\n");
                #endregion

                #region 寫入 ACCESS MDB FILE
                taProg.AppendText("ACCESS MDB 產生..");
                taProg.AppendText(Model.createInnoMDB(dt));
                taProg.AppendText("\n");
                
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
                taProg.AppendText("ERROR" + ex.Message);
            }
        }

        private string getUICondition()
        {
            string sWhere = "";

            if(!string.IsNullOrEmpty(tbAccountsCode.Text))
            {
                sWhere += " and a.accounts_code = '" + tbAccountsCode.Text + "' ";
            }
            if (!string.IsNullOrEmpty(tbOldCode.Text))
            {
                sWhere += " and f.old_acct_code = '" + tbOldCode.Text + "' ";
            }


            if (ivdt_from.Enabled && ivdt_to.Enabled)
            {
                sWhere += " and to_char(c.invoice_date, 'YYYY/MM/DD') between '" + ivdt_from.Text + "' and '" + ivdt_to.Text + "' ";
            }

            string GUIorREV = "";
            if (!string.IsNullOrEmpty(tbGUI_NO.Text))
            {
                GUIorREV = " A.SUPPER_INVOICE_NO = '" + tbGUI_NO.Text + "' ";
            }
            if (!string.IsNullOrEmpty(tbREV_NO.Text))
            {
                if (!string.IsNullOrEmpty(GUIorREV)) GUIorREV += " or ";

                GUIorREV += " A.RECEIPT_NO = '" + tbREV_NO.Text + "' ";
            }
            if (!string.IsNullOrEmpty(GUIorREV))
                sWhere += " and (" + GUIorREV + ") ";


            // 隱藏區間內完全正負REVERSED 的單據
            if (cbHideRCW.Checked)
            {
                string sTrnsDateWhere = "";
                if(ivdt_from.Enabled && ivdt_to.Enabled)
                    sTrnsDateWhere += " and to_char(c.invoice_date, 'YYYY/MM/DD') between '" + ivdt_from.Text + "' and '" + ivdt_to.Text + "' ";

                sWhere += @"
                and not exists (select * from EBS_ERP.T_ACC_INVOICE R where c.INVOICE_NO = R.RCW_INVOICE_NO " + sTrnsDateWhere + @") 
                and not exists (select * from EBS_ERP.T_ACC_INVOICE R where c.RCW_INVOICE_NO = R.INVOICE_NO " + sTrnsDateWhere + @") ";
            }
            // 隱藏未開發票或是收據的費用項目
            if (cbHideNoGUI.Checked)
            {
                sWhere += @" and nvl(a.SUPPER_INVOICE_NO, a.RECEIPT_NO) is not null ";
            }

            return sWhere;
        }

        private void cbFastSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            taProg.AppendText("公司快選: " + cbFastSelection.Text);
            taProg.AppendText("\n");
                       
            string[] selectedValue = (string[])cbFastSelection.SelectedValue;
            tbAccountsCode.Text = selectedValue[0];
            tbOldCode.Text = selectedValue[1];
        }

        /// <summary>
        /// 檢核發票號碼與收據號碼兩個欄位是否有值，如果都沒值就開放，不然就DISABLE 掉
        /// </summary>
        private void ivdt_enabler()
        {
            if (string.IsNullOrWhiteSpace(tbGUI_NO.Text) && string.IsNullOrWhiteSpace(tbREV_NO.Text))
            {
                if(this.ivdt_from.Enabled != true)
                {
                    taProg.AppendText("日期條件: 開放");
                    taProg.AppendText("\n");
                }
                
                this.ivdt_from.Enabled = true;
                this.ivdt_to.Enabled = true;
            }
            else
            {
                if(this.ivdt_from.Enabled != false)
                {
                    taProg.AppendText("日期條件: 關閉");
                    taProg.AppendText("\n");
                }
                
                this.ivdt_from.Enabled = false;
                this.ivdt_to.Enabled = false;
            }
        }

        private void tbGUI_NO_TextChanged(object sender, EventArgs e)
        {
            ivdt_enabler();
        }

        private void tbREV_NO_TextChanged(object sender, EventArgs e)
        {
            ivdt_enabler();
        }
    }
}
