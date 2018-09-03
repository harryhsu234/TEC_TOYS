using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Configuration;

namespace INNOLUX_DB
{
    class Model
    {
        private static string tmplatePath = ConfigurationManager.AppSettings["templatePath"];
        private static string outputPath = ConfigurationManager.AppSettings["outputPath"];


        static string[] xlsx_col_names = new string[]
        {
            "TYPE", "EXPIMPNO", "TAX_CODE", "COMPANY_ID", "COMPANY_CODE",
            "HAWBNO", "INNO_CHARGE_CODE", "NOTAX_AMOUNT", "TAX_LOCAL_AMOUNT", "NOTAX_LOCAL_AMOUNT",
            "CURR_TYPE", "LOCAL_RATE", "GUI_NO", "GUI_DATE", "PAY_SEQ"
        };

        static string[] access_col_names = new string[]
        {
            "Type", "進出口號碼", "稅碼", "發票廠商統一編號", "請款廠商代碼",
            "大小提單號碼", "費用類別", "原幣金額", "台幣稅金", "單價",
            "幣別", "匯率", "發票號碼", "發票日期", "廠商請款編號"
        };

        public static DataTable getInnoInvoice(string sWhere)
        {
          

            //progBar.Value += 2;
            string sql_core = @"select b.booking_no, c.invoice_no, c.job_type, 
                                  case when c.job_type = 'AE' or c.job_type = 'SE' then 'EXP' else 'IMP' end as type, 
                                  case nvl(a.tax_local_amount,0) when 0 then 'P0' else 'P1' end as tax_code,
                                  b.hawbno, a.charge_code, e.code as inno_charge_code, 
                                  nvl(a.notax_local_amount,0) notax_amount, nvl(a.tax_local_amount,0) tax_local_amount, nvl(a.notax_local_amount,0) notax_local_amount,
                                  nvl(a.SUPPER_INVOICE_NO, a.RECEIPT_NO) as GUI_NO, d.SUPPER_INVOICE_DATE as GUI_DATE, a.accountno
                            from t_acc_charge a
                            left outer join t_job_head b on a.booking_no = b.booking_no
                            left outer join t_acc_invoice c on a.accountno = c.invoice_no
                            left outer join t_gui_n_inv d on d.SUPPER_INVOICE_NO = nvl(a.SUPPER_INVOICE_NO, a.RECEIPT_NO)
                            left outer join t_r_charge_map e on e.flag = 'INNOLUX' and a.charge_code = e.fno
                            left outer join t_d_accounts f on a.accounts_code = f.accounts_code
                            where 1=1 " + sWhere;
            string sql = @"select core.booking_no, nvl(aebk.inv_no, ' ') as expimpno, case when core.job_type = 'AE' or core.job_type = 'SE' then 'EXP' else 'IMP' end as type,
                                    tax_code, '86865094' as company_id, 'A86865094' as company_code,
                                    hawbno, nvl(inno_charge_code, 'E1640') as inno_charge_code,
                                    sum(notax_amount) as notax_amount, 
                                    sum(tax_local_amount) as tax_local_amount,
                                    sum(notax_local_amount) as notax_local_amount,
                                    'TWD' as curr_type, 1 as local_rate, GUI_NO, to_char(GUI_DATE, 'yyyy/mm/dd') as GUI_DATE, '' as pay_seq
                            from (" + sql_core + @") core 
                            left outer join t_ae_booking aebk on aebk.booking_no = core.booking_no
                            group by core.booking_no, nvl(aebk.inv_no, ' '), core.job_type, core.tax_code, core.inno_charge_code, core.hawbno, inno_charge_code, GUI_NO, GUI_DATE 
                            order by core.hawbno ";


            DataTable dt = OracleWorker.SelectDataTable(sql);

            if (dt.Rows.Count == 0) throw new Exception("資料庫無相符合的資料");


            foreach (DataRow dr in dt.Rows)
            {
                dr["HAWBNO"] = dr["HAWBNO"].ToString().Replace("-", "").Trim();

                string gui_date = dr["GUI_DATE"].ToString();
                if(string.IsNullOrEmpty(gui_date))
                {
                    dr["PAY_SEQ"] = "";
                }
                else
                {
                    DateTime gui_datetime = DateTime.Parse(gui_date);
                    int y = gui_datetime.Year % 10;
                    int m = gui_datetime.Month;

                    string sMonth = "";
                    switch (m)
                    {
                        case 10: sMonth = "A"; break;
                        case 11: sMonth = "B"; break;
                        case 12: sMonth = "C"; break;
                        default: sMonth = "" + m; break;
                    }
                    dr["PAY_SEQ"] = "86865094C" + y + sMonth + "A";
                }
                


            }

            return dt;
        }

        internal static string createInnoMDB(DataTable dt)
        {
            string result = "OK";

            string templatePath = Environment.CurrentDirectory + tmplatePath + "INNOLUX.mdb";

            string filename = "INNOLUX_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mdb";
            string newFullPath = Environment.CurrentDirectory + outputPath + filename;//新文件绝对路径

            string fileDir = Environment.CurrentDirectory + outputPath;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            System.IO.File.Copy(templatePath, newFullPath, true);
            // FileInfo newFile = new FileInfo(newFullPath);

            string sql = "";

            sql = " select * from 資料明細表 ";
            foreach(DataColumn dc in AccessWorker.GetOleDbDataTable(newFullPath, sql).Columns)
                Console.WriteLine(dc.ColumnName);

            sql = " DELETE FROM 資料明細表 ";
            AccessWorker.OleDbInsertUpdateDelete(newFullPath, sql);

            foreach (DataRow dr in dt.Rows)
            {
                string[] inv_nos = dr["EXPIMPNO"].ToString().Split(new char[] { ',', '.', '/' });
                bool isFirstInvNo = true;
                foreach (string inv_no in inv_nos)
                {

                    #region 逐筆將RECORD 寫入MDB 
                    string values = ""; // insert sql 的values 組合
                    bool isFirstVal = true; // 如果是第一筆 就不要有 逗號

                    foreach (string col_name in xlsx_col_names)
                    {
                        if (!isFirstVal) values += ", ";
                        if (col_name == "COMPANY_ID" || col_name == "LOCAL_RATE")
                            values += dr[col_name];
                        else if (col_name == "EXPIMPNO") // 解析過的 T_AE_BOOKING.INV_NO
                            values += " '" + inv_no + "'";
                        else if (col_name == "NOTAX_AMOUNT" || col_name == "TAX_LOCAL_AMOUNT" 
                                    || col_name == "NOTAX_LOCAL_AMOUNT")
                        {
                            if (isFirstInvNo)
                                values += dr[col_name];
                            else
                                values += 0;
                        }
                        else if (col_name == "TAX_CODE")
                        {
                            if (isFirstInvNo)
                                values += " '" + dr[col_name] + "'";
                            else
                                values += " 'P0'";
                        }
                        else if (col_name == "GUI_DATE" && string.IsNullOrEmpty(dr["GUI_DATE"].ToString()))
                            values += " null ";
                        else
                            values += " '" + dr[col_name] + "'";

                        isFirstVal = false;
                    }

                    string cols = "";
                    isFirstVal = true;
                    foreach (string col_name in access_col_names)
                    {
                        if (!isFirstVal) cols += ", ";

                        cols += col_name;

                        isFirstVal = false;
                    }

                    sql = @" INSERT INTO 資料明細表(" + cols + ") VALUES (" + values + ") ";


                    // sql = @" INSERT INTO 資料明細表(Type, 進出口號碼, 稅碼, 發票廠商統一編號, 請款廠商代碼, 大小提單號碼, 費用類別, 原幣金額, 台幣稅金, 單價, 幣別, 匯率, 發票號碼, 發票日期, 廠商請款編號) VALUES ( 'EXP',  '80407521', 'P1', '86865094',  'A86865094',  'TEC1718536',  'E0140', 87990, 4400, 87990,  'TWD', '1',  'PN55631727',  '2017/07/31',  '86865094C77A') ";

                    AccessWorker.OleDbInsertUpdateDelete(newFullPath, sql);
                    #endregion

                    isFirstInvNo = false;
                }
            }


            //DataTable dtHAWB = dt.AsDataView().ToTable(true, "EXPIMPNO", "HAWBNO");
            //foreach (DataRow drHAWB in dtHAWB.Rows)
            //{
            //    string filter = "HAWBNO = '" + drHAWB["HAWBNO"].ToString().Trim() + "'";
            //    string[] inv_nos = drHAWB["EXPIMPNO"].ToString().Split(new char[] { ',', '.', '/' });
            //    bool isFirstInvNo = true;
            //    foreach (string inv_no in inv_nos)
            //    {
            //        string _inv_no = inv_no.Trim();
            //        foreach (DataRow dr in dt.Select(filter))
            //        {
            //            #region 逐筆將RECORD 寫入MDB 
            //            string values = ""; // insert sql 的values 組合
            //            bool isFirstVal = true; // 如果是第一筆 就不要有 逗號

            //            foreach (string col_name in xlsx_col_names)
            //            {
            //                if (!isFirstVal) values += ", ";
            //                if (col_name == "COMPANY_ID" || col_name == "LOCAL_RATE")
            //                    values += dr[col_name];
            //                else if (col_name == "EXPIMPNO") // 解析過的 T_AE_BOOKING.INV_NO
            //                    values += " '" + _inv_no + "'";
            //                else if (col_name == "NOTAX_AMOUNT" || col_name == "TAX_LOCAL_AMOUNT" || col_name == "NOTAX_LOCAL_AMOUNT")
            //                {
            //                    if (isFirstInvNo)
            //                        values += dr[col_name];
            //                    else
            //                        values += 0;
            //                }
            //                else if (col_name == "GUI_DATE" && string.IsNullOrEmpty(dr["GUI_DATE"].ToString()))
            //                    values += " null ";
            //                else
            //                    values += " '" + dr[col_name] + "'";

            //                isFirstVal = false;
            //            }

            //            string cols = "";
            //            isFirstVal = true;
            //            foreach (string col_name in access_col_names)
            //            {
            //                if (!isFirstVal) cols += ", ";

            //                cols += col_name;

            //                isFirstVal = false;
            //            }

            //            sql = @" INSERT INTO 資料明細表(" + cols + ") VALUES (" + values + ") ";

            //            AccessWorker.OleDbInsertUpdateDelete(newFullPath, sql);
            //            #endregion
            //        }
            //        isFirstInvNo = false;
            //    }

            //}
            
            return result;
        }

        public static string createInnoExcel(DataTable dt)
        {
            string result = "OK";

            string templatePath = Environment.CurrentDirectory + tmplatePath + "INNOLUX_FEE_DETAIL.xlsx";

            string filename = "INNOLUX_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            string newFullPath = Environment.CurrentDirectory + outputPath + filename;//新文件绝对路径

            string fileDir = Environment.CurrentDirectory + outputPath;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            System.IO.File.Copy(templatePath, newFullPath, true);
            FileInfo newFile = new FileInfo(newFullPath);

            using (ExcelPackage ep = new ExcelPackage(newFile))
            {
                ExcelWorksheet ws = ep.Workbook.Worksheets["INNO"];
                
                int row_pos = 2; // 從第2行開始印 A2
                foreach(DataRow dr in dt.Rows)
                {
                    string[] inv_nos = dr["EXPIMPNO"].ToString().Split(new char[] { ',', '.', '/' });
                    bool isFirstInvNo = true;
                    foreach (string inv_no in inv_nos)
                    {
                        int col_pos = 1;
                        foreach (string col_name in xlsx_col_names)
                        {
                            if (col_name == "NOTAX_AMOUNT" || col_name == "TAX_LOCAL_AMOUNT" || col_name == "NOTAX_LOCAL_AMOUNT")
                            {
                                if (isFirstInvNo)
                                    ws.Cells[row_pos, col_pos++].Value = dr[col_name];
                                else
                                    ws.Cells[row_pos, col_pos++].Value = 0;
                            }
                            else if (col_name == "TAX_CODE" )
                            {
                                if (isFirstInvNo)
                                    ws.Cells[row_pos, col_pos++].Value = dr[col_name];
                                else
                                    ws.Cells[row_pos, col_pos++].Value = "P0";
                            }
                            else if (col_name == "EXPIMPNO")
                            {
                                ws.Cells[row_pos, col_pos++].Value = inv_no;
                            }
                            else
                                ws.Cells[row_pos, col_pos++].Value = dr[col_name];
                        }
                       
                        row_pos++;
                        isFirstInvNo = false;
                    }
                }



                ep.Save();
            }


            return result;
        }
    }
}
