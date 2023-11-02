<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewGraphs.aspx.cs" Inherits="USADataRoundabout.Contact" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .small-flex {
            display: flex;
            flex-direction: column;
            justify-content: space-evenly;
        }
        .big-flex {
            display: flex;
            flex-direction: row;
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
    </asp:Content>
