﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GarmentsPro.Departments.Wet_Processing
{
    public partial class Order : System.Web.UI.Page
    {

        string Con = @"Data Source=.;Initial Catalog=GarmentsPro;Integrated Security=SSPI";

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            DataTable MyTable = new DataTable();

            using (SqlConnection Sqlconnection = new SqlConnection(Con))
            {
                string MyQ = "select a.OID, OrderID ,ClientName , OrderType , Qty ,ETA_Time  as Deadline, Created_Date, Status, Yarn_Formation from Orders a join Status b on a.OrderID = b.OID";

                SqlDataAdapter myada = new SqlDataAdapter(MyQ, Sqlconnection);
                myada.Fill(MyTable);
            }

            GridView1.DataSource = MyTable;
            GridView1.DataBind();
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {


        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void btnStart_Click(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        Button btnStart = (row.Cells[4].FindControl("btnStart") as Button);
            //        if (Convert.ToBoolean(btnStart.OnClientClick) == true)
            //        {
            //            using (SqlConnection Sqlconnection = new SqlConnection(Con))
            //            {
            //                Sqlconnection.Open();

            //                string MyQ = "Update Status set Yarn_Formation=@YP where StaID=@ID";
            //                SqlCommand MyCmd = new SqlCommand(MyQ, Sqlconnection);
            //                MyCmd.Parameters.AddWithValue("@YP", "In Progress") ;
            //                MyCmd.Parameters.AddWithValue("@ID", row.Cells[0].Text);
            //                //MyCmd.ExecuteNonQuery();

            //                Sqlconnection.Close();


            //            }
            //        }
            //    }
            //}
        }



        protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow gvRow = GridView1.Rows[index];
            SqlConnection Sqlconnection = new SqlConnection(Con);

            Button btnstart = (gvRow.Cells[5].FindControl("btnstart") as Button);
            Button btnhold = (gvRow.Cells[6].FindControl("btnhold") as Button);
            Button btnFinish = (gvRow.Cells[7].FindControl("btnfinish") as Button);


            //DataTable MyTable = new DataTable();


            //    string MyQ = "select a.OID, OrderID ,ClientName , OrderType , Qty , CONVERT(VARCHAR(20)," +
            //                         "ETA_Time, 110) as Deadline, Created_Date, Status, Yarn_Formation from Orders a join Status b on a.OrderID = b.OID";

            //    SqlDataAdapter myada = new SqlDataAdapter(MyQ, Sqlconnection);
            //    myada.Fill(MyTable);


            //GridView1.DataSource = MyTable;
            //GridView1.DataBind();



            //Label lblParentID = (Label)GridView1.Rows[].FindControl("lblParentID");


            switch (e.CommandName)
            {

                case "Start":

                    btnstart.Text = "In progress";
                    btnstart.CssClass = "text-success";
                    btnhold.Text = "Hold";
                    btnhold.CssClass = "btn btn-warning btn-sm";
                    btnFinish.Visible = true;

                    string MyQa = "update Status  set Yarn_Formation=@YP where OID=@OID";
                    SqlCommand MyCmd = new SqlCommand(MyQa, Sqlconnection);

                    Sqlconnection.Open();

                    MyCmd.Parameters.AddWithValue("@OID", gvRow.Cells[1].Text);
                    MyCmd.Parameters.AddWithValue("@YP", "In Progress");

                    MyCmd.ExecuteNonQuery();

                    Sqlconnection.Close();
                    //btns = (row.Cells[5].FindControl("lblStatus") as Label).Text = "In progress";
                    //btns = (row.Cells[5].FindControl("lblStatus") as Label).CssClass = ("Color" "Danger");
                    break;

                case "Hold":

                    btnhold.Text = "on Hold";
                    btnhold.CssClass = "text-danger";
                    btnstart.Text = "Start";
                    btnstart.CssClass = "none";
                    btnstart.CssClass = "btn btn-success btn-sm";

                    btnFinish.Visible = false;

                    string MysQ = "update Status  set Yarn_Formation=@YP where OID=@OID";
                    SqlCommand MyCmds = new SqlCommand(MysQ, Sqlconnection);

                    Sqlconnection.Open();

                    MyCmds.Parameters.AddWithValue("@OID", gvRow.Cells[1].Text);
                    MyCmds.Parameters.AddWithValue("@YP", "on Hold");

                    MyCmds.ExecuteNonQuery();
                    Sqlconnection.Close();

                    break;

                case "Finish":
                    gvRow.Visible = false;

                    string MysQ1 = "update Status  set Yarn_Formation=@YP where OID=@OID";
                    SqlCommand MyCmdss = new SqlCommand(MysQ1, Sqlconnection);

                    Sqlconnection.Open();


                    MyCmdss.Parameters.AddWithValue("@OID", gvRow.Cells[1].Text);
                    MyCmdss.Parameters.AddWithValue("@YP", "Completed");

                    MyCmdss.ExecuteNonQuery();
                    Sqlconnection.Close();

                    break;

            }
        }
    }
}









