<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewGraphs.aspx.cs" Inherits="USADataRoundabout.Contact" %>

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
            <p>Display Type</p>
            <select>
                <option value="Pie">Pie Chart</option>
                <option value="Bar">Bar Chart</option>
            </select>
        </div>
        <div class="small-flex">
            <p>Date Range</p>
            <p>
                <input type="date" id="dteStart" runat="server" />
                <input type="date" id="dteEnd" runat="server" />
            </p>
        </div>
        <div class="small-flex">
            <p>Sort By:</p>
            <select>
                <option value="School">School</option>
                <option value="ProtGroup">Protected Groups</option>
                <option value="Facility">Facility</option>
            </select>
        </div>
    </div>

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var data = google.visualization.arrayToDataTable([
                ['School', 'Complaints'],
                ['Computing', 11],
                ['Engineering', 2],
                ['Athletics', 2],
                ['Medical', 2],
                ['Arts and Sciences', 7]
            ]);

            var options = {
                title: 'Complaint Distribution'
            };

            var chart = new google.visualization.PieChart(document.getElementById('piechart'));

            chart.draw(data, options);
        }
    </script>
    <div id="piechart" style="width: 900px; height: 500px;" class="big-flex"></div>
    </asp:Content>
