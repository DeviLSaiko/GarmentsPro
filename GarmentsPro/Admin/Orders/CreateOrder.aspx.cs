﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GarmentsPro.Admin.Orders
{
    public partial class CreateOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrder();
            }
        }


        private string MyConnection()
        {
            return @"Data Source =.; Initial Catalog = GarmentsPro; Integrated Security = SSPI ";
        }
        private bool CheckExist()
        {
            DataTable MyTableee = new DataTable();
            SqlConnection Sqlconnection = new SqlConnection(MyConnection());
            SqlDataAdapter myada = new SqlDataAdapter("select * from Orders where OrderID=@OID", Sqlconnection);
            myada.SelectCommand.Parameters.AddWithValue("@OID", txtOrderID.Text);
            myada.Fill(MyTableee);


            if (MyTableee != null && MyTableee.Rows.Count > 0)
                return true;
            else
                return false;
        }
        private void LoadOrder()
        {
            DataTable MT = new DataTable();

            SqlConnection MyCon = new SqlConnection(MyConnection());
            SqlDataAdapter MA = new SqlDataAdapter("Select * from Orders", MyCon);
            MA.Fill(MT);

            GridView1.DataSource = MT;
            GridView1.DataBind();

            if (MT.Rows.Count == 0)
            {
                txtError.Text = "No Record Found";
            }

        }

        protected void BtnCreate_Click1(object sender, EventArgs e)
        {
           //trim date  select replace(convert(varchar, getdate(), 101), '/', '') +replace(convert(varchar, getdate(), 108), ':', '')

            bool CheakExist1 = CheckExist();

            if (CheakExist1 == false)
            {
                SqlConnection MyCon = new SqlConnection(MyConnection());
                SqlCommand MyCmd = new SqlCommand("Insert into  OrderID,ClientName,OrderType,Qty,ETA_Time,Status) values (@OID, @CN,@OType,@Qty,@ETA,@Stat)", MyCon);
                MyCon.Open();
                MyCmd.Parameters.AddWithValue("@OID", "(convert(varchar, txtOrderID.Text , 101), '/', '') + replace(convert(varchar, txtOrderID.Text, 108), ':', '') ");
                MyCmd.Parameters.AddWithValue("@CN", txtClinet.Text);
                MyCmd.Parameters.AddWithValue("@OType", ddType.SelectedValue);
                MyCmd.Parameters.AddWithValue("@Qty", txtQty.Text);
                MyCmd.Parameters.AddWithValue("@ETA", txtETA.Text);
                MyCmd.Parameters.AddWithValue("@Stat", DdStatus.SelectedValue);

                MyCmd.ExecuteNonQuery();

                string mYq = "Insert into Status (OID ,Yarn_Formation , Fabric_Formation , Wet_Processing ,  Fabrication  , Finished_Goods  ) values (@OID , @YF ,@FF ,@WP,@F,@FG)" ;
                SqlCommand MyCmd1 = new SqlCommand(mYq, MyCon);

                MyCmd1.Parameters.AddWithValue("@OID", txtOrderID.Text);
                MyCmd1.Parameters.AddWithValue("@YF", "Yet To Start");
                MyCmd1.Parameters.AddWithValue("@FF", "Yet To Start");
                MyCmd1.Parameters.AddWithValue("@WP", "Yet To Start");
                MyCmd1.Parameters.AddWithValue("@F", "Yet To Start");
                MyCmd1.Parameters.AddWithValue("@FG", "Yet To Start");
                MyCmd1.ExecuteNonQuery();

                MyCon.Close();


                txtOrderID.Text = "";
                txtClinet.Text = "";
                ddType.SelectedIndex = 0;
                DdStatus.SelectedIndex = 0;
                txtQty.Text = "";
                txtETA.Text = "";
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "swal('Error' , 'Order ID  Must be Unique ' , 'info')", true);
            }

            LoadOrder();
        }

    }
}