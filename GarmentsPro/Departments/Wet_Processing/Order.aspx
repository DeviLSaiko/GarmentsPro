﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Departments/Wet_Processing/WetProcessing.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="GarmentsPro.Departments.Wet_Processing.Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="container" style="padding-top:250px">
        <div class="row">
            <center>
            <div class="col-md-12 align-content-center">
                <asp:GridView ID="GridView1" CssClass=" table table-responsive"  HeaderStyle-CssClass="align-content-center" OnRowDataBound="GridView1_RowDataBound" OnRowCommand= "GridView1_RowCommand1" AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:BoundField DataField="OID" HeaderText="ID" />
                        <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                        <asp:BoundField DataField="ClientName" HeaderText="Client " />
                        <asp:BoundField DataField="OrderType" HeaderText="Order Type" />
                         <asp:BoundField DataField="DeadLine" HeaderText="DeadLine" />
                         <asp:BoundField DataField="Yarn_Formation" HeaderText="Order Status" />

                        <asp:TemplateField HeaderText="Order Status" HeaderStyle-BackColor="White">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Set Start" HeaderStyle-BackColor="White">
                            <ItemTemplate>
                                <asp:Button ID="btnstart" CommandName="Start"   CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  CssClass="btn btn-success btn-sm " Width="100px" runat="server" Text="Start" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Put on Hold"   HeaderStyle-BackColor="White">
                            <ItemTemplate>
                                <asp:Button ID="btnhold"  CommandName="Hold"   CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  CssClass="btn btn-warning btn-sm" Width="100px" runat="server"  Text="Hold" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Completed" HeaderStyle-BackColor="White">
                            <ItemTemplate>
                                <asp:Button ID="btnfinish"  CommandName="Finish"   CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  CssClass="btn btn-primary btn-sm" Width="100px" runat="server"  Text="Finish" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                <asp:Label runat="server" ID="txtError" Text=""></asp:Label>
            </div>
        </div>
    </div>



</asp:Content>