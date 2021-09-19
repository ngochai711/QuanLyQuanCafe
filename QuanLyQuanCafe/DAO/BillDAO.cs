using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCafe.DTO;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BillDAO();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }
        private BillDAO() { }


        //Succesed: return ID
        //Fail: return -1
        public int GetUncheckBillIdByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM	 dbo.Bill WHERE idTable= "+ id +" AND status = 0");
            if(data.Rows.Count> 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        public DataTable GetBillListByDate(DateTime checkin, DateTime checkout)
        {
           return DataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDate @checkin , @checkout", new object[] { checkin, checkout });
        }

        public void CheckOut(int id, int  discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), status = 1, "+" discount = "+ discount +", totalPrice = "+ totalPrice +" WHERE id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }


        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @idTable",new object[] { id});
        }

        public int GetMaxID()
        {
            try { 
            return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM	 dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }

    }
}
