<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="USADataRoundabout.Contact" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .big-flex {
        display: flex;
        flex-direction: row;
        justify-content: space-evenly;
        padding-top: 50px;
    }
    .small-flex {
        display: flex;
        flex-direction: column;
        align-items: start;
    }
</style>
    <div class="big-flex">
        <div class="small-flex">
            <dl>
                <dt>Display Type</dt>
                <dd><select id="ddlDisplay" runat="server">
                    <option value="Pie">Pie Chart</option>
                    <option value="Bar">Bar Chart</option>
                </select></dd>
            </dl>
        </div>
        <div class="small-flex">
            <dl>
                <dt>Date Range</dt>
                <dd>
                    <input type="date" id="dteStart" runat="server" />
                    <input type="date" id="dteEnd" runat="server" />
                </dd>
            </dl>
        </div>
        <div class="small-flex">
            <dl>
                <dt>Sort By:</dt>
                <dd><select id ="ddlSortBy" runat="server">
                    <option value="School">School</option>
                    <option value="ProtGroup">Protected Groups</option>
                    <option value="Facility">Facility</option>
                </select></dd>
            </dl>
        </div>
        <div class="small-flex">
            <button type="submit" runat="server" id="btnGenerate" onserverclick="btnGenerate_Click">Submit</button>
            <!--<var id="output" runat="server">imhere</var>-->
        </div>
    </div>
    <div class="big-flex">
        <div class="small-flex">
            <asp:Chart ID="chrtGraph" runat="server">
                <Series>
                    <asp:Series ChartType="Pie" Name="Series1" CustomProperties="PieLabelStyle = Outside">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
        <div class="small-flex" id="SchoolTotals" runat="server">
            <dl>
                <dt>Totals:</dt>
                <dd><ul id="ulSchool" runat="server"></ul></dd>
            </dl>
        </div>
        <div class="small-flex" id="ProtGroupsTotals" runat="server">
            <dl>
                <dt>Totals:</dt>
                <dd><ul id="ulProtGroup" runat="server"></ul></dd>
            </dl>
        </div>
        <div class="small-flex" id="FacilityTotals" runat="server">
            <dl>
                <dt>Totals:</dt>
                <dd><ul id="ulFacility" runat="server"></ul></dd>
            </dl>
        </div>
    </div>
</asp:Content>
